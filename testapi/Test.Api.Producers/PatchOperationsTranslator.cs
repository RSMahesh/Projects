// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatchOperationsTranslator.cs" company="Test Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Test Inc.
// </copyright>
// <summary>
//   The patch operations translator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Test.Api.Business;

using Test.Api.Business;
using Test.Api.Core;
using Test.Api.WebModels;

namespace Test.Api.Producers
{
    /// <summary>
    /// The patch operations translator.
    /// </summary>
    public class PatchOperationsTranslator : IPatchOperationsTranslator
    {
        private static readonly IList<Validator<PatchOperation>> PathValueValidators = new List<Validator<PatchOperation>>
            {
                operation => !operation.Path.IsNullOrEmpty() ? (IRuleResult)RulePassed.Passed : new RuleCriticallyViolated(FormatMissingInformationErrorMessage(operation, "path")),
                operation => operation.Value != null ? (IRuleResult)RulePassed.Passed : new RuleCriticallyViolated(FormatMissingInformationErrorMessage(operation, "value"))
            };

        private static readonly IList<Validator<PatchOperation>> PathOnlyValidators = new List<Validator<PatchOperation>>
            {
                operation => !operation.Path.IsNullOrEmpty() ? (IRuleResult)RulePassed.Passed : new RuleCriticallyViolated(FormatMissingInformationErrorMessage(operation, "path"))
            };

        private static readonly IList<Validator<PatchOperation>> PathFromValidators = new List<Validator<PatchOperation>>
            {
                operation => !operation.Path.IsNullOrEmpty() ? (IRuleResult)RulePassed.Passed : new RuleCriticallyViolated(FormatMissingInformationErrorMessage(operation, "path")),
                operation => !operation.From.IsNullOrEmpty() ? (IRuleResult)RulePassed.Passed : new RuleCriticallyViolated(FormatMissingInformationErrorMessage(operation, "from"))
            };

        private readonly IObjectFactory _objectFactory;
        private readonly DataValidationService<PatchOperation> _pathValueValidationService;
        private readonly DataValidationService<PatchOperation> _pathOnlyValidationService;
        private readonly DataValidationService<PatchOperation> _pathFromValidationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatchOperationsTranslator"/> class.
        /// </summary>
        /// <param name="objectFactory">
        /// The object factory.
        /// </param>
        public PatchOperationsTranslator(IObjectFactory objectFactory)
        {
            Ensure.Argument.IsNotNull(objectFactory, "objectFactory");

            this._objectFactory = objectFactory;

            this._pathValueValidationService = new DataValidationService<PatchOperation>(PathValueValidators);
            this._pathOnlyValidationService = new DataValidationService<PatchOperation>(PathOnlyValidators);
            this._pathFromValidationService = new DataValidationService<PatchOperation>(PathFromValidators);
        }

        /// <summary>
        /// The translate.
        /// </summary>
        /// <param name="operations">
        /// The operations.
        /// </param>
        /// <param name="context">
        /// Context containing information about the request.
        /// </param>
        /// <returns>
        /// The patch operations in business object form.
        /// </returns>
        public IEnumerable<PatchOperationBase> Translate(IEnumerable<PatchOperation> operations, ITestApiContext context)
        {
            Ensure.That<PostRequestEntityDetailsNullException>(operations != null);

            var operationList = new List<PatchOperationBase>();
            foreach (var operation in operations)
            {
                var operationInstruction = operation.Operation ?? string.Empty;
                switch (operationInstruction.ToLowerInvariant())
                {
                    case "test":
                        var testOperation = this.CreatePathValueOperation<TestPatchOperation>(operation, context);
                        if (testOperation != null)
                        {
                            operationList.Add(testOperation);
                        }

                        break;
                    case "remove":
                        var removeOperation = this.CreatePathOnlyOperation<RemovePatchOperation>(operation, context);
                        if (removeOperation != null)
                        {
                            operationList.Add(removeOperation);
                        }

                        break;
                    case "add":
                        var addOperation = this.CreatePathValueOperation<AddPatchOperation>(operation, context);
                        if (addOperation != null)
                        {
                            operationList.Add(addOperation);
                        }

                        break;
                    case "replace":
                        var replaceOperation = this.CreatePathValueOperation<ReplacePatchOperation>(operation, context);
                        if (replaceOperation != null)
                        {
                            operationList.Add(replaceOperation);
                        }

                        break;
                    case "move":
                        var moveOperation = this.CreatePathFromOperation<MovePatchOperation>(operation, context);
                        if (moveOperation != null)
                        {
                            operationList.Add(moveOperation);
                        }

                        break;
                    case "copy":
                        var copyOperation = this.CreatePathFromOperation<CopyPatchOperation>(operation, context);
                        if (copyOperation != null)
                        {
                            operationList.Add(copyOperation);
                        }

                        break;
                    default:
                        context.AddError(string.Format(@"Invalid PATCH operation '{0}'.  The instruction was ignored.", operation.Operation ?? "null"));
                        break;
                }
            }

            return operationList;
        }

        private static string FormatMissingInformationErrorMessage(PatchOperation operation, string field)
        {
            return string.Format("Operation {0} must have a '{1}' specified.", operation, field);
        }

        private static string FormatTooMuchInformationWarningMessage(PatchOperation operation, string field)
        {
            return string.Format("Operation {0} does not need a '{1}' specified.  The argument has been ignored.", operation, field);
        }

        private T CreatePathValueOperation<T>(PatchOperation operation, ITestApiContext context) where T : PatchOperationBase
        {
            var validationResult = this._pathValueValidationService.Validate(operation);

            if (validationResult.HasErrors())
            {
                context.AddError(string.Format("{0}  The instruction was ignored.", validationResult));
                return null;
            }

            if (!operation.From.IsNullOrEmpty())
            {
                var warning = FormatTooMuchInformationWarningMessage(operation, "from");
                context.AddWarning(warning);
            }

            return this.CreateOperation<T>(operation.Path, operation.Value);                        
        }

        private T CreatePathOnlyOperation<T>(PatchOperation operation, ITestApiContext context) where T : PatchOperationBase
        {
            var validationResult = this._pathOnlyValidationService.Validate(operation);

            if (validationResult.HasErrors())
            {
                context.AddError(string.Format("{0}  The instruction was ignored.", validationResult));
                return null;
            }

            if (operation.Value != null)
            {
                context.AddWarning(FormatTooMuchInformationWarningMessage(operation, "value"));
            }

            if (!operation.From.IsNullOrEmpty())
            {
                var warning = FormatTooMuchInformationWarningMessage(operation, "from");
                context.AddWarning(warning);
            }

            return this.CreateOperation<T>(operation.Path);
        }

        private T CreatePathFromOperation<T>(PatchOperation operation, ITestApiContext context) where T : PatchOperationBase
        {
            var validationResult = this._pathFromValidationService.Validate(operation);

            if (validationResult.HasErrors())
            {
                context.AddError(string.Format("{0}  The instruction was ignored.", validationResult));
                return null;
            }

            if (operation.Value != null)
            {
                context.AddWarning(FormatTooMuchInformationWarningMessage(operation, "value"));
            }

            return this.CreateOperation<T>(operation.Path, operation.From);
        }

        private T CreateOperation<T>(params object[] args)
        {
            return this._objectFactory.Create<T>(args);
        }
    }
}