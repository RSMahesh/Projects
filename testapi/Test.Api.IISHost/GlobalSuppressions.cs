// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Media Valet Inc.">
//   The information described in this document is furnished 
//   as proprietary information and may not be copied or sold 
//   without the written permission of Media Valet Inc.
// </copyright>
// <summary>
//   GlobalSuppressions.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", 
        MessageId = "Api", Scope = "namespace", Target = "MediaValet.Api", 
        Justification = "We are following Microsoft's naming conventon of the term")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api", 
        Justification = "We are following Microsoft's naming conventon of the term")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", 
        Justification = "This is a TODO Item")]
[assembly: 
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api",
        Scope = "namespace", Target = "MediaValet.Api.Plumbing",
        Justification = "We are following Microsoft's naming conventon of the term")]
[assembly: 
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api",
        Scope = "namespace", Target = "MediaValet.Api.Models",
        Justification = "We are following Microsoft's naming conventon of the term")]
[assembly: 
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api",
        Scope = "namespace", Target = "MediaValet.Api.Controllers",
        Justification = "We are following Microsoft's naming conventon of the term")]
[assembly: 
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api",
        Scope = "type", Target = "MediaValet.Api.WebApiApplication",
        Justification = "We are following Microsoft's naming conventon of the term")]
