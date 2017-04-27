// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorLog.cs" company="">
//   
// </copyright>
// <summary>
//   The error log.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The error log.
    /// </summary>
    public class ErrorLog
    {
        /// <summary>
        /// Gets or sets the f_id.
        /// </summary>
        public decimal f_id { get; set; }

        /// <summary>
        /// Gets or sets the session fid.
        /// </summary>
        public Guid? SessionFID { get; set; }

        /// <summary>
        /// Gets or sets the user ip.
        /// </summary>
        public string UserIP { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the insert date.
        /// </summary>
        public DateTime InsertDate { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the additional info.
        /// </summary>
        public string AdditionalInfo { get; set; }
    }
}