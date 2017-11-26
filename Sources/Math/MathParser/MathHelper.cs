// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathHelper.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the MathHelper class - a static class for defining extension methods
    /// for the MathParser system
    /// </summary>
    internal static class MathHelper
    {
        /// <summary>
        /// Transforms a string expression into a list of lexemes
        /// </summary>
        /// <param name="expression">The expression to split into lexemes</param>
        /// <returns>The resulting list of lexemes</returns>
        public static IEnumerable<string> LexicalSplit(this string expression)
        {
            return new Lexer().Split(expression);           
        }

        /// <summary>
        /// Transforms a string expression into a list of tokens
        /// </summary>
        /// <param name="expression">The expression to tokenize</param>
        /// <returns>The list of tokens</returns>
        public static IEnumerable<IToken> Tokenize(this string expression)
        {
            return new Tokenizer().Tokenize(expression.LexicalSplit());
        }

        /// <summary>
        /// Transforms a mathematical string expression into a list of postfix tokens
        /// </summary>
        /// <param name="expression">The mathematical expression to be transferred to a postfix notation</param>
        /// <returns>The list of tokens in postfix notation</returns>
        public static IEnumerable<IToken> ToPostfix(this string expression)
        {
            return new ShuntingYard().ToPostfix(expression.Tokenize());
        }
    }
}
