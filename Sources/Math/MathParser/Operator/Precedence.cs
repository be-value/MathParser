// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Precedence.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <summary>
    /// Defines the precedence of arithmetic operators
    /// </summary>
    internal enum Precedence
    {
        /// <summary>
        /// Precedence of the assignment operator '='
        /// </summary>
        Assignment = 0,

        /// <summary>
        /// Precedence of the additive operators '+' and '-'
        /// </summary>
        Additive = 1,

        /// <summary>
        /// Precedence of the multiplicative operators '*' and'/'
        /// </summary>
        Multiplicative = 2,

        /// <summary>
        /// Precedence of the unary operator '+' and '-'
        /// </summary>
        Unary = 3,

        /// <summary>
        /// Precedence of the function operators 'f(..,..)'
        /// </summary>
        Primary = 4
    }
}
