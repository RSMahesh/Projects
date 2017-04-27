using System;

namespace MediaProcessor.ServiceLibrary.Common
{
    /// <summary>
    /// The exe exception.
    /// </summary>
    public class ExeException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExeException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ExeException(string message)
            : base(message)
        {
        }

        #endregion
    }
}