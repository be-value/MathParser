// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShuntingYardTests.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    /// <summary>
    /// Defines the test cases for the shunting yard algorithm
    /// </summary>
    public class ShuntingYardTests
    {
        /// <summary>
        /// The shunting yard algorithm object used by all tests
        /// </summary>
        private readonly ShuntingYard shuntingYard;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShuntingYardTests"/> class.
        /// </summary>
        public ShuntingYardTests()
        {
            shuntingYard = new ShuntingYard();
        }

        /// <summary>
        /// Test if an argument exception is thrown when providing a null value as input.
        /// </summary>
        [Fact]
        public void ToPostfixCalledWithNullThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => shuntingYard.ToPostfix(null).ToList());
        }

        /// <summary>
        /// Test if numeric operands are returned as a single value expression in the postfix set.
        /// </summary>
        [Fact]
        public void ToPostfixCalledWithSingleNumberReturnsThatNumber()
        {
            // Arrange
            var number = new OperandToken { Position = 0, Lexeme = "24", Type = Operand.Numeric };
            var infix = new List<IToken> { number };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(1, postfix.Count);
            Assert.Same(number, postfix[0]);
        }

        /// <summary>
        /// Test if numeric operands are returned in the same order
        /// </summary>
        [Fact]
        public void ToPostfixWithTwoNumbersRetursThoseNumbers()
        {
            // Arrange
            var numberA = new OperandToken { Position = 0, Lexeme = "24", Type = Operand.Numeric };
            var numberB = new OperandToken { Position = 3, Lexeme = "12", Type = Operand.Numeric };
            var infix = new List<IToken> { numberA, numberB };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(2, postfix.Count);
            Assert.Same(numberA, postfix[0]);
            Assert.Same(numberB, postfix[1]);
        }

        /// <summary>
        /// Test if a unary operator and number will be reversed when converted to infix
        /// </summary>
        [Fact]
        public void ToPostfixWithUnaryOperatorReversesOrder()
        {
            // Arrange
            var unaryOp = new OperatorToken { Position = 0, Lexeme = "-", Type = Operator.UnaryMinus };
            var number = new OperandToken { Position = 1, Lexeme = "25", Type = Operand.Numeric };
            var infix = new List<IToken> { unaryOp, number };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(2, postfix.Count);
            Assert.Same(number, postfix[0]);
            Assert.Same(unaryOp, postfix[1]);
        }

        /// <summary>
        /// Test if a binary operator will be put at the end of the infix list
        /// </summary>
        [Fact]
        public void ToPostFixWithBinaryOperatorPutsOperatorAtTheEnd()
        {
            // Arrange
            var leftOperand = new OperandToken { Position = 0, Lexeme = "26", Type = Operand.Numeric };
            var binaryOperator = new OperatorToken { Position = 2, Lexeme = "*", Type = Operator.Multiplication };
            var rightOperand = new OperandToken { Position = 3, Lexeme = "4", Type = Operand.Numeric };
            var infix = new List<IToken> { leftOperand, binaryOperator, rightOperand };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(3, postfix.Count);
            Assert.Same(leftOperand, postfix[0]);
            Assert.Same(rightOperand, postfix[1]);
            Assert.Same(binaryOperator, postfix[2]);
        }

        /// <summary>
        /// Test if operators with the same precedence will be converted in the correct sequence
        /// </summary>
        [Fact]
        public void ToPostfixWithTwoBinaryOperatorsWithSamePrecedence()
        {
            var operand1 = new OperandToken { Position = 0, Lexeme = "26", Type = Operand.Numeric };
            var operatorA = new OperatorToken { Position = 2, Lexeme = "*", Type = Operator.Multiplication };
            var operand2 = new OperandToken { Position = 3, Lexeme = "4", Type = Operand.Numeric };
            var operatorB = new OperatorToken { Position = 4, Lexeme = "/", Type = Operator.Division };
            var operand3 = new OperandToken { Position = 5, Lexeme = "8", Type = Operand.Numeric };
            var infix = new List<IToken> { operand1, operatorA, operand2, operatorB, operand3 };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(5, postfix.Count);
            Assert.Same(operand1, postfix[0]);
            Assert.Same(operand2, postfix[1]);
            Assert.Same(operatorA, postfix[2]);            
            Assert.Same(operand3, postfix[3]);
            Assert.Same(operatorB, postfix[4]);
        }

        /// <summary>
        /// Test if operators with the same precedence will be converted in the correct sequence
        /// </summary>
        [Fact]
        public void ToPostfixWithTwoBinaryOperatorsWithDifferentPrecedence()
        {
            var operand1 = new OperandToken { Position = 0, Lexeme = "26", Type = Operand.Numeric };
            var operatorA = new OperatorToken { Position = 2, Lexeme = "+", Type = Operator.Addition };
            var operand2 = new OperandToken { Position = 3, Lexeme = "4", Type = Operand.Numeric };
            var operatorB = new OperatorToken { Position = 4, Lexeme = "*", Type = Operator.Multiplication };
            var operand3 = new OperandToken { Position = 5, Lexeme = "8", Type = Operand.Numeric };
            var infix = new List<IToken> { operand1, operatorA, operand2, operatorB, operand3 };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(5, postfix.Count);
            Assert.Same(operand1, postfix[0]);
            Assert.Same(operand2, postfix[1]);
            Assert.Same(operand3, postfix[2]);
            Assert.Same(operatorB, postfix[3]);
            Assert.Same(operatorA, postfix[4]);
        }

        /// <summary>
        /// Test if operators with the same precedence will be converted in the correct sequence
        /// </summary>
        [Fact]
        public void ToPostfixWithTwoBinaryOperatorsWithDifferentPrecedenceButCorrectedWithParenthesis()
        {
            var lpar = new MetaToken { Position = 0, Lexeme = "(", Type = Meta.LeftParenthesis };
            var operand1 = new OperandToken { Position = 1, Lexeme = "26", Type = Operand.Numeric };
            var operatorA = new OperatorToken { Position = 3, Lexeme = "+", Type = Operator.Addition };
            var operand2 = new OperandToken { Position = 4, Lexeme = "4", Type = Operand.Numeric };
            var rpar = new MetaToken { Position = 5, Lexeme = ")", Type = Meta.RightParenthesis };
            var operatorB = new OperatorToken { Position = 6, Lexeme = "*", Type = Operator.Multiplication };
            var operand3 = new OperandToken { Position = 7, Lexeme = "8", Type = Operand.Numeric };
            var infix = new List<IToken> { lpar, operand1, operatorA, operand2, rpar, operatorB, operand3 };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(5, postfix.Count);
            Assert.Same(operand1, postfix[0]);
            Assert.Same(operand2, postfix[1]);
            Assert.Same(operatorA, postfix[2]);
            Assert.Same(operand3, postfix[3]);
            Assert.Same(operatorB, postfix[4]);
        }

        /// <summary>
        /// Test if omitting a left parenthesis causes a parser exception
        /// </summary>
        [Fact]
        public void ToPostfixWithTwoBinaryOperatorsWithNoOpeningParenthesisThrowsParserException()
        {
            var operand1 = new OperandToken { Position = 0, Lexeme = "26", Type = Operand.Numeric };
            var operatorA = new OperatorToken { Position = 2, Lexeme = "+", Type = Operator.Addition };
            var operand2 = new OperandToken { Position = 3, Lexeme = "4", Type = Operand.Numeric };
            var rpar = new MetaToken { Position = 4, Lexeme = ")", Type = Meta.RightParenthesis };
            var operatorB = new OperatorToken { Position = 5, Lexeme = "*", Type = Operator.Multiplication };
            var operand3 = new OperandToken { Position = 6, Lexeme = "8", Type = Operand.Numeric };
            var infix = new List<IToken> { operand1, operatorA, operand2, rpar, operatorB, operand3 };

            // Act and assert
            try
            {
                shuntingYard.ToPostfix(infix);
                Assert.True(false, "No ParserException was thrown");
            }
            catch (ParserException e)
            {
                Console.WriteLine(e.Message);
                Assert.Equal(e.Position, rpar.Position);
                Assert.Equal(e.Lexeme, rpar.Lexeme);
            }
        }

        /// <summary>
        /// Test if omitting a right parenthesis causes a parser exception
        /// </summary>
        [Fact]
        public void ToPostfixWithTwoBinaryOperatorsWithNoClosingParenthesisThrowsParserException()
        {
            // Arrange
            var lpar = new MetaToken { Position = 0, Lexeme = "(", Type = Meta.LeftParenthesis };
            var operand1 = new OperandToken { Position = 1, Lexeme = "26", Type = Operand.Numeric };
            var operatorA = new OperatorToken { Position = 3, Lexeme = "+", Type = Operator.Addition };
            var operand2 = new OperandToken { Position = 4, Lexeme = "4", Type = Operand.Numeric };
            var operatorB = new OperatorToken { Position = 5, Lexeme = "*", Type = Operator.Multiplication };
            var operand3 = new OperandToken { Position = 6, Lexeme = "8", Type = Operand.Numeric };
            var infix = new List<IToken> { lpar, operand1, operatorA, operand2, operatorB, operand3 };

            // Act and assert
            try
            {
                shuntingYard.ToPostfix(infix);
                Assert.True(false, "No ParserException was thrown");
            }
            catch (ParserException e)
            {
                Console.WriteLine(e.Message);
                Assert.Equal(e.Position, lpar.Position);
                Assert.Equal(e.Lexeme, lpar.Lexeme);
            }
        }

        /// <summary>
        /// Test a combination of multiple unary operators with a single binary operator
        /// </summary>
        [Fact]
        public void ToPostfixWithTwoUnaryOperandsAndBinaryOperandConvertsCorrectly()
        {
            // Arrange
            var operatorA = new OperatorToken { Position = 0, Lexeme = "+", Type = Operator.UnaryPlus };
            var operand1 = new OperandToken { Position = 1, Lexeme = "26", Type = Operand.Numeric };
            var operatorB = new OperatorToken { Position = 3, Lexeme = "-", Type = Operator.Subtraction };
            var operatorC = new OperatorToken { Position = 4, Lexeme = "-", Type = Operator.UnaryMinus };
            var operand2 = new OperandToken { Position = 5, Lexeme = "8", Type = Operand.Numeric };
            var infix = new List<IToken> { operatorA, operand1, operatorB, operatorC, operand2 };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(5, postfix.Count);
            Assert.Same(operand1, postfix[0]);
            Assert.Same(operatorA, postfix[1]);
            Assert.Same(operand2, postfix[2]);
            Assert.Same(operatorC, postfix[3]);
            Assert.Same(operatorB, postfix[4]);
        }

        /// <summary>
        /// Test a function expression converts correctly
        /// </summary>
        [Fact]
        public void ToPostfixWithFunctionConvertsCorrectly()
        {
            // Arrange
            var sinus = new OperatorToken { Position = 0, Lexeme = "sin", Type = Operator.Function };
            var meta1 = new MetaToken { Position = 3, Lexeme = "(", Type = Meta.LeftParenthesis };
            var operand = new OperandToken { Position = 4, Lexeme = "8", Type = Operand.Numeric };
            var meta2 = new MetaToken { Position = 5, Lexeme = ")", Type = Meta.RightParenthesis };
            var infix = new List<IToken> { sinus, meta1, operand, meta2 };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(2, postfix.Count);
            Assert.Same(operand, postfix[0]);
            Assert.Same(sinus, postfix[1]);
        }

        /// <summary>
        /// Test a function expression with an expression as parameter converts correctly
        /// </summary>
        [Fact]
        public void ToPostfixWithFunctionAndExpressionParameterConvertsCorrectly()
        {
            // Arrange
            var sinus = new OperatorToken { Position = 0, Lexeme = "sin", Type = Operator.Function };
            var meta1 = new MetaToken { Position = 3, Lexeme = "(", Type = Meta.LeftParenthesis };
            var operandA = new OperandToken { Position = 4, Lexeme = "8", Type = Operand.Numeric };
            var operator1 = new OperatorToken { Position = 5, Lexeme = "+", Type = Operator.Addition };
            var operandB = new OperandToken { Position = 6, Lexeme = "56", Type = Operand.Numeric };
            var meta2 = new MetaToken { Position = 8, Lexeme = ")", Type = Meta.RightParenthesis };
            var infix = new List<IToken> { sinus, meta1, operandA, operator1, operandB, meta2 };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(4, postfix.Count);
            Assert.Same(operandA, postfix[0]);
            Assert.Same(operandB, postfix[1]);
            Assert.Same(operator1, postfix[2]);
            Assert.Same(sinus, postfix[3]);
        }

        /// <summary>
        /// Test if a function with two parameter converts correctly to postfix
        /// </summary>
        [Fact]
        public void ToPostfixWithFunctionRequiringTwoParametersConvertsCorrectly()
        {
            // Arrange
            var power = new OperatorToken { Position = 0, Lexeme = "pow", Type = Operator.Function };
            var meta1 = new MetaToken { Position = 3, Lexeme = "(", Type = Meta.LeftParenthesis };
            var operand1 = new OperandToken { Position = 4, Lexeme = "2", Type = Operand.Numeric };
            var meta2 = new MetaToken { Position = 5, Lexeme = ",", Type = Meta.Comma };
            var operand2 = new OperandToken { Position = 6, Lexeme = "3", Type = Operand.Numeric };
            var meta3 = new MetaToken { Position = 7, Lexeme = ")", Type = Meta.RightParenthesis };
            var infix = new List<IToken> { power, meta1, operand1, meta2, operand2, meta3 };

            // Act
            var postfix = shuntingYard.ToPostfix(infix).ToList();

            // Assert
            Assert.Equal(3, postfix.Count);
            Assert.Same(operand1, postfix[0]);
            Assert.Same(operand2, postfix[1]);
            Assert.Same(power, postfix[2]);
        }

        /// <summary>
        /// Test a combination of multiple unary operators with a single binary operator
        /// </summary>
        [Fact]
        public void ToPostfixExtensionMethodActsIdentically()
        {
            // Arrange
            const string expression = "+26--8";
            var operatorA = new OperatorToken { Position = 0, Lexeme = "+", Type = Operator.UnaryPlus };
            var operand1 = new OperandToken { Position = 1, Lexeme = "26", Type = Operand.Numeric };
            var operatorB = new OperatorToken { Position = 3, Lexeme = "-", Type = Operator.Subtraction };
            var operatorC = new OperatorToken { Position = 4, Lexeme = "-", Type = Operator.UnaryMinus };
            var operand2 = new OperandToken { Position = 5, Lexeme = "8", Type = Operand.Numeric };
            var infix = new List<IToken> { operatorA, operand1, operatorB, operatorC, operand2 };

            // Act
            var postfix1 = shuntingYard.ToPostfix(infix).ToList();
            var postfix2 = expression.ToPostfix().ToList();

            // Assert
            Assert.Equal(postfix1.Count, postfix2.Count);
            for (var i = 0; i < postfix1.Count; i++)
            {
                Assert.Equal(postfix1[i].Position, postfix2[i].Position);
                Assert.Equal(postfix1[i].Lexeme, postfix2[i].Lexeme);
            }
        }
    }
}
