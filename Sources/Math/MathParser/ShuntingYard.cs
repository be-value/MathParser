// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShuntingYard.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the Shunting Yard Algorithm for converting infix to postfix
    /// </summary>
    internal class ShuntingYard
    {
        /// <summary>
        /// The method to convert infix to postfix
        /// </summary>
        /// <param name="infix">The mathematical expression as a set of tokens in infix notation.</param>
        /// <returns>The mathematical expression as a set of tokens in postfix notation</returns>
        /// <exception cref="ArgumentException">When infix is null.</exception>
        public IEnumerable<IToken> ToPostfix(IEnumerable<IToken> infix)
        {
            if (null == infix)
            {
                throw new ArgumentException("Parameter cannot be null", "infix");
            }

            var operatorStack = new Stack<IStackable>();
            return ToPostfix(infix, operatorStack);
        }

        /// <summary>
        /// Test if the token at the top of the stack is an operator token
        /// </summary>
        /// <param name="operatorStack">The stack to inspect</param>
        /// <returns>true if there is an operator at the top of the stack, false otherwise</returns>
        private static IOperatorToken OperatorOnTop(Stack<IStackable> operatorStack)
        {
            if (0 < operatorStack.Count && operatorStack.Peek() is IOperatorToken)
            {
                return operatorStack.Peek() as IOperatorToken;
            }

            return null;
        }

        /// <summary>
        /// Converts the input sequence of tokens in infix notation to a postfix notation
        /// </summary>
        /// <param name="infix">The sequence of tokens in infix notation.</param>
        /// <param name="operatorStack">The operator stack used during the process.</param>
        /// <returns>The sequence of tokens in postfix (Reverse Polish) notation.</returns>
        private IEnumerable<IToken> ToPostfix(IEnumerable<IToken> infix, Stack<IStackable> operatorStack)
        {
            var postfix = new List<IToken>();
            var tokenEnumerator = infix.GetEnumerator();

            // While there are tokens to be read:
            while (tokenEnumerator.MoveNext())
            {
                // Read a token
                var token = tokenEnumerator.Current;

                // If the token is a number (or variable), add it to the output queue
                if (token is IOperandToken)
                {
                    postfix.Add(token);
                }

                // If the token is an operator, o1, then
                if (token is IOperatorToken)
                {
                    var theOperator = token as IOperatorToken;

                    // If the token is a function token
                    if (theOperator.Type == Operator.Function)
                    {
                        // push it onto the stack.
                        operatorStack.Push(theOperator);
                    }
                    else
                    {
                        var stackOperator = OperatorOnTop(operatorStack);

                        // while there is an operator token, o2 at the top of the stack and
                        //      either o1 is left associative and its precedence is less than or equal to that of o2
                        //      or o1 is right associative and its precedence is less than that of o2
                        while (null != stackOperator && operatorStack.Count > 0 &&
                                ((theOperator.Associativity() == Associativity.Left2Right &&
                                 theOperator.Precedence() <= stackOperator.Precedence())
                                 ||
                                (theOperator.Associativity() == Associativity.Right2Left &&
                                 theOperator.Precedence() < stackOperator.Precedence())))
                        {
                            // pop o2 off the stack, onto the output queue
                            postfix.Add(operatorStack.Pop() as IToken);
                            stackOperator = OperatorOnTop(operatorStack);
                        }

                        // puch o1 onto the stack
                        operatorStack.Push(theOperator);                        
                    }
                }

                // Handling left- and right parenthesis
                if (token is IMetaToken)
                {
                    var meta = token as IMetaToken;

                    // If the token is a left parenthesis, then push it onto the stack
                    if (meta.Type == Meta.LeftParenthesis)
                    {
                        operatorStack.Push(meta);
                    }

                    // If the token is a function argument separator (e.g., a comma):
                    if (meta.Type == Meta.Comma)
                    {
                        var stackOperator = OperatorOnTop(operatorStack);

                        // Until the token at the top of the stack is a left parenthesis
                        while (null != stackOperator)
                        {
                            // pop operators off the stack into the output queue.
                            postfix.Add(operatorStack.Pop() as IToken);
                            stackOperator = OperatorOnTop(operatorStack);
                        }

                        // If no left parentheses are encountered, either the separator was 
                        // misplaced or parentheses were mismatched.
                        if (operatorStack.Count == 0)
                        {
                            const string Error = "Parameter separator was misplaced or parentheses were mismatched";
                            throw new ParserException(Error, token.Lexeme, token.Position);
                        }
                    }

                    // If the token is a richt parenthesis:
                    if (meta.Type == Meta.RightParenthesis)
                    {
                        var stackOperator = OperatorOnTop(operatorStack);

                        // Until the token at the top of the stack is a left parenthesis
                        while (null != stackOperator)
                        {
                            // pop operators off the stack onto the output queue
                            postfix.Add(operatorStack.Pop() as IToken);
                            stackOperator = OperatorOnTop(operatorStack);
                        }

                        if (operatorStack.Count > 0)
                        {
                            // This can only be a parenthesis! - if not, we have a problem
                            // Pop the left parenthesis from the stack, but not onto the output queue
                            operatorStack.Pop();
                        }
                        else
                        {
                            // If the stack runs out wihtout finding a left parenthesis, then there are mismatched parentheses.
                            throw new ParserException("No matching left parenthesis found", token.Lexeme, token.Position);
                        }

                        stackOperator = OperatorOnTop(operatorStack);

                        // If the token at the top of the stack is a function token
                        if (null != stackOperator && Operator.Function == stackOperator.Type)
                        {
                            // pop it onto the output queue
                            postfix.Add(operatorStack.Pop() as IToken);
                        }
                    }
                }
            }

            // When there are no more tokens to read:
            // While there are still operator tokens in the stack
            while (operatorStack.Count > 0)
            {
                var op = operatorStack.Pop() as IToken;

                // If the operator token on the top of the stack is a parenthesis, then there are mismatched parentheses.
                if (op is IMetaToken)
                {
                    throw new ParserException("No matching right parenthesis found", op.Lexeme, op.Position);
                }

                // Pop the operator onto the output queue
                postfix.Add(op);
            }

            // Exit
            return postfix;
        }
    }
}
