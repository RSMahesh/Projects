// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pagination.cs" company="">
//   
// </copyright>
// <summary>
//   Specifies the start index and number of records to return for a request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Test.Api.WebModels
{
    /// <summary>
    /// Specifies the start index and number of records to return for a request.
    /// </summary>
    public sealed class Pagination
    {
        /// <summary>
        /// The default count.
        /// </summary>
        private const int DefaultCount = 50;

        /// <summary>
        /// The default offset.
        /// </summary>
        private const int DefaultOffset = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pagination"/> class.
        /// </summary>
        public Pagination()
        {
            this.Offset = DefaultOffset;
            this.Count = DefaultCount;
        }

        /// <summary>
        /// Gets or sets the index of the first record to return.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the number of records to return.
        /// </summary>
        public int Count { get; set; }
    }
}