// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnknownToken.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <inheritdoc />
    /// <summary>
    /// The UnknownToken type is the class representing unexpected tokens
    /// </summary>
    internal class UnknownToken : IToken
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
    }
}
