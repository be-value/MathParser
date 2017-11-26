// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Operand.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <summary>
    /// Defines the type of the operand
    /// </summary>
    internal enum Operand
    {
        /// <summary>
        /// Represents a numerical operand
        /// </summary>
        Numeric,

        /// <summary>
        /// Represents a variable as operand
        /// </summary>
        Variable
    }
}
