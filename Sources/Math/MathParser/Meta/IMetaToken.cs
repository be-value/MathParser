// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetaToken.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <summary>
    /// Defines the meta token interface
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    internal interface IMetaToken : IToken, IStackable
    {
        /// <summary>
        /// Gets the type of the meta token
        /// </summary>
        Meta Type { get; }
    }
}
