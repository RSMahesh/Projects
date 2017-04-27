// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogInterceptor.cs" company="">
//   
// </copyright>
// <summary>
//   The log interceptor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.Plumbing
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Castle.Core.Logging;
    using Castle.DynamicProxy;

  

    using Newtonsoft.Json;

    using Test.Api.Core;

    /// <summary>
    /// The log interceptor.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class LogInterceptor : IInterceptor
    {
        /// <summary>
        /// The parameter separator.
        /// </summary>
        private static readonly string ParameterSeparator = string.Format(",{0}", Environment.NewLine);

        /// <summary>
        /// The _logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInterceptor"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public LogInterceptor(ILogger logger)
        {
            Ensure.Argument.IsNotNull(logger, "logger");

            this._logger = logger;
        }

        /// <summary>
        /// The intercept.
        /// </summary>
        /// <param name="invocation">
        /// The invocation.
        /// </param>
        public void Intercept(IInvocation invocation)
        {
            // No need wasting time creating the call string if it not going to be used.
            var callString = this.CreateCallString(invocation);

            this._logger.Debug(callString);

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                // Log.Write(EventKind.Error, ex.ToString());
                throw;
            }

            this._logger.DebugFormat(
                DetermineDebugFormatString(invocation.Method.ReturnType), 
                callString, 
                this.DumpObject(invocation.ReturnValue));
        }

        /// <summary>
        /// The determine debug format string.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string DetermineDebugFormatString(Type type)
        {
            return type == typeof(void) ? "{0} returned" : "{0} returned {1}";
        }

        /// <summary>
        /// The data contract serialize.
        /// </summary>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string DataContractSerialize(object argument)
        {
            try
            {
                return JsonConvert.SerializeObject(argument);
            }
            catch
            {
                return argument.ToString();
            }
        }

        /// <summary>
        /// The create call string.
        /// </summary>
        /// <param name="invocation">
        /// The invocation.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateCallString(IInvocation invocation)
        {
            if (!this._logger.IsDebugEnabled)
            {
                return string.Empty;
            }

            return string.Format(
                "{0}.{1}({2})", 
                invocation.TargetType.Name, 
                invocation.Method.Name, 
                this.CreateArgumentList(invocation));
        }

        /// <summary>
        /// The create argument list.
        /// </summary>
        /// <param name="invocation">
        /// The invocation.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateArgumentList(IInvocation invocation)
        {
            var arguments = invocation.Arguments.Select(this.DumpObject).ToList();

            return string.Join(ParameterSeparator, arguments);
        }

        /// <summary>
        /// The dump object.
        /// </summary>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string DumpObject(object argument)
        {
            if (!this._logger.IsDebugEnabled)
            {
                return string.Empty;
            }

            if (argument == null)
            {
                return "<<NULL>>";
            }

            var objtype = argument.GetType();
            if (objtype.IsPrimitive || !objtype.IsClass)
            {
                return argument.ToString();
            }

            return string.Format("({0}){1}", objtype, DataContractSerialize(argument));
        }
    }
}