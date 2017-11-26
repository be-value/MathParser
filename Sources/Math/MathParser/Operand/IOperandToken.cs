// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperandToken.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <inheritdoc />
    /// <summary>
    /// Defines the Operand token interface
    /// </summary>
    internal interface IOperandToken : IToken
    {
        /// <summary>
        /// Gets the type of this operand
        /// </summary>
        Operand Type { get; }
    }
}
