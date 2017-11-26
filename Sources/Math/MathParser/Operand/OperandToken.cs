// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperandToken.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <inheritdoc />
    /// <summary>
    /// Defines the OperandToken type
    /// </summary>
    internal class OperandToken : IOperandToken
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
        /// Gets or sets the type of the operand
        /// </summary>
        public Operand Type { get; set; }
    }
}
