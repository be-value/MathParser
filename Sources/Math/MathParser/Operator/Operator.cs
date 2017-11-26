// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Operator.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <summary>
    /// Defines the operator types used by this math parser
    /// </summary>
    internal enum Operator
    {
        /// <summary>
        /// Represents the unary plus operator
        /// </summary>
        UnaryPlus,

        /// <summary>
        /// Represents the addition operator
        /// </summary>
        Addition,

        /// <summary>
        /// Represents the unary minus operator
        /// </summary>
        UnaryMinus,

        /// <summary>
        /// Represents the subtraction operator
        /// </summary>
        Subtraction,

        /// <summary>
        /// Represents the multiplication operator
        /// </summary>
        Multiplication,

        /// <summary>
        /// Represents the division operator
        /// </summary>
        Division,

        /// <summary>
        /// Represents a function operator
        /// </summary>
        Function
    }
}
