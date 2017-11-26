// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetaToken.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <inheritdoc />
    /// <summary>
    /// Defines the meta token
    /// This class must be replaced in a later stadium of the development process
    /// </summary>
    internal class MetaToken : IMetaToken
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the lexeme corresponding to this token
        /// </summary>
        public string Lexeme { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the position of this token in the original expression
        /// </summary>
        public int Position { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the type of the meta token
        /// </summary>
        public Meta Type { get; set; }
    }
}
