// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatorExtensions.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    /// <summary>
    /// Definition of extension methods for operator information
    /// </summary>
    internal static class OperatorExtensions
    {
        /// <summary>
        /// Determines the Precedence of a specific operator
        /// </summary>
        /// <param name="operatorToken">The operator token.</param>
        /// <returns>The precedence of the operator.</returns>
        public static Precedence Precedence(this IOperatorToken operatorToken)
        {
            return operatorToken.Properties().OperatorPrecedence;
        }

        /// <summary>
        /// Determines the Associativity of a specific operator
        /// </summary>
        /// <param name="operatorToken">The operator token.</param>
        /// <returns>The associativity of the operator.</returns>
        public static Associativity Associativity(this IOperatorToken operatorToken)
        {
            return operatorToken.Properties().OperatorAssociativity;
        }

        /// <summary>
        /// Retrieval of key properties regarding a specific operator
        /// </summary>
        /// <param name="operatorToken">The operator token.</param>
        /// <returns>A locally used object containing the operator properties.</returns>
        private static OperatorInfo Properties(this IOperatorToken operatorToken)
        {
            switch (operatorToken.Type)
            {
                case Operator.UnaryPlus:
                case Operator.UnaryMinus:
                    return new OperatorInfo
                        {
                            OperatorAssociativity = MathParser.Associativity.Right2Left,
                            OperatorPrecedence = MathParser.Precedence.Unary
                        };
                case Operator.Multiplication:
                case Operator.Division:
                    return new OperatorInfo
                    {
                        OperatorAssociativity = MathParser.Associativity.Left2Right,
                        OperatorPrecedence = MathParser.Precedence.Multiplicative
                    };
                default:
                    return new OperatorInfo
                    {
                        OperatorAssociativity = MathParser.Associativity.Left2Right,
                        OperatorPrecedence = MathParser.Precedence.Additive
                    };
            }            
        }

        /// <summary>
        /// Locally used class for provisioning operator information
        /// </summary>
        private class OperatorInfo
        {
            /// <summary>
            /// Gets or sets Associativity.
            /// </summary>
            public Associativity OperatorAssociativity { get; set; }

            /// <summary>
            /// Gets or sets Precedence.
            /// </summary>
            public Precedence OperatorPrecedence { get; set; }
        }
    }
}
