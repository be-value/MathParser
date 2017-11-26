// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenizerTests.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
namespace MathParser.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Rhino.Mocks;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="Tokenizer"/> class and its corresponding extension method(s)
    /// </summary>
    public class TokenizerTests
    {
        /// <summary>
        /// The <see cref="Tokenizer"/> used by all the tests
        /// </summary>
        private readonly Tokenizer tokenizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenizerTests"/> class.
        /// </summary>
        public TokenizerTests()
        {
            tokenizer = new Tokenizer();
        }

        #region null and empty expressions
        /// <summary>
        /// Test if calling the <see cref="Tokenizer"/> with null results in an <see cref="ArgumentException"/> 
        /// being thrown.
        /// </summary>
        [Fact]
        public void TokenizeCalledWithNullThrowsArgumentException()
        {
            // Act and Assert
            Assert.Throws<ArgumentException>(() => tokenizer.Tokenize(null).ToList());
        }
        #endregion

        #region various scenarios
        /// <summary>
        /// Test if unknown tokens are recognized at all
        /// </summary>
        [Fact]
        public void TokenizeUnknownTokerRetunsUnknowToken()
        {
            // Arrange
            const string expression = " @ADX# ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.Equal("@", list[0].Lexeme);
            Assert.IsType<UnknownToken>(list[0]);
            Assert.Equal("#", list[2].Lexeme);
            Assert.IsType<UnknownToken>(list[2]);
        }

        /// <summary>
        /// Test if in the process of tokenizing, the whitespace tokens are removed
        /// </summary>
        [Fact]
        public void TokenizeExpressionRemovesWhitespaceTokens()
        {
            // Arrange
            //                         0123456
            const string expression = " ( a + 3 ) ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.Equal(5, list.Count);
        }
        #endregion

        #region Operand group

        /// <summary>
        /// Test if tokenizing an integer value returns an operand token
        /// </summary>
        [Fact]
        public void TokenizeNumberReturnsOperandToken()
        {
            // Arrange
            const string expression = " 12 + 3 ";

            // Act
            var list = GetTokens(expression);

            // Arrange
            Assert.IsAssignableFrom<IOperandToken>(list[0]);
        }

        /// <summary>
        /// Test if tokenizing a decimal point value returns an operand token
        /// </summary>
        [Fact]
        public void TokenizeNumberWithFixedDecimalPointReturnsOperandToken()
        {
            // Arrange
            const string expression = " 12.3 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.Equal(1, list.Count);
            Assert.IsAssignableFrom<IOperandToken>(list[0]);
        }

        /// <summary>
        /// Test if tokenizing identifiers returns operand tokens
        /// </summary>
        [Fact]
        public void TokenizeIdentifierReturnsOperandToken()
        {
            // Arrange
            const string expression = " v _x a12";
            
            // Act
            var list = GetTokens(expression);

            // Arrange
            Assert.IsAssignableFrom<IOperandToken>(list[0]);
            Assert.IsAssignableFrom<IOperandToken>(list[1]);
            Assert.IsAssignableFrom<IOperandToken>(list[2]);
        }

        #endregion

        #region Operator group

        /// <summary>
        /// Test if tokenizing a plus character returns an operator token
        /// </summary>
        [Fact]
        public void TokenizePlusReturnsOperatorToken()
        {
            // Arrange
            const string expression = " +12 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.IsAssignableFrom<IOperatorToken>(list[0]);
        }

        /// <summary>
        /// Test if tokenizing a minus character returns an operator token
        /// </summary>
        [Fact]
        public void TokenizeMinusReturnsOperatorToken()
        {
            // Arrange
            const string expression = " -13 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.IsAssignableFrom<IOperatorToken>(list[0]);
        }

        /// <summary>
        /// Test if tokenizing a asterisk character returns an operator token
        /// </summary>
        [Fact]
        public void TokenizeAsteriskReturnsOperatorToken()
        {
            // Arrange
            const string expression = " *14 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.IsAssignableFrom<IOperatorToken>(list[0]);
        }

        /// <summary>
        /// Test if tokenizing a slash character returns an operator token
        /// </summary>
        [Fact]
        public void TokenizeSlashReturnsOperatorToken()
        {
            // Arrange
            const string expression = " /15 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.IsAssignableFrom<IOperatorToken>(list[0]);
        }

        /// <summary>
        /// Test if tokenizing a function identifier returns an operator token
        /// </summary>
        [Fact]
        public void TokenizeFunctionReturnsOperatorToken()
        {
            // Arrange mocking the function environment
            var mocks = new MockRepository();
            var loader = mocks.Stub<IFunctionLoader>();
            Functions.Repository = new FunctionRepository(loader);
            SetupResult.For(loader.GetFunctionInfo()).Return(new List<FunctionInfo> { new FunctionInfo { ArgumentCount = 1, Name = "sin" } });
            mocks.ReplayAll();

            // Actual function expression
            const string expression = " sin(90) ";
            
            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.IsAssignableFrom<IOperatorToken>(list[0]);
            mocks.VerifyAll();
        }
        #endregion

        #region Meta group

        /// <summary>
        /// Test if tokenizing a left parenthesis character returns an meta token
        /// </summary>
        [Fact]
        public void TokenizeLeftParenthesisReturnsMetaToken()
        {
            // Arrange
            const string expression = " (16 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.IsAssignableFrom<IMetaToken>(list[0]);
        }

        /// <summary>
        /// Test if tokenizing a right parenthesis character returns an meta token
        /// </summary>
        [Fact]
        public void TokenizeRightParenthesisReturnsMetaToken()
        {
            // Arrange
            const string expression = " )17 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.IsAssignableFrom<IMetaToken>(list[0]);
        }

        /// <summary>
        /// Test if tokenizing a comma character returns an meta token
        /// </summary>
        [Fact]
        public void TokenizeCommaReturnsMetaToken()
        {
            // Arrange
            const string expression = " ,18 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.IsAssignableFrom<IMetaToken>(list[0]);
        }

        #endregion

        #region specific tokens
        /// <summary>
        /// Test if tokenizing a number returns this specific operand token
        /// </summary>
        [Fact]
        public void TokenizeNumericReturnsNumberOperandToken()
        {
            // Arrange
            const string expression = " 12345 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operandToken = list[0] as IOperandToken;
            AssertTypeOfOperand(Operand.Numeric, operandToken);
        }

        /// <summary>
        /// Test if tokenizing a decimal number returns this specific operand token
        /// </summary>
        [Fact]
        public void TokenizeNumericWithDecimalReturnsNumberOperandToken()
        {
            // Arrange
            const string expression = " 123.45 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            Assert.Equal(1, list.Count);
            var operandToken = list[0] as IOperandToken;
            AssertTypeOfOperand(Operand.Numeric, operandToken);
        }

        /// <summary>
        /// Test is tokenizing an identifier returns this specific operand token
        /// </summary>
        [Fact]
        public void TokenizeIdentifierReturnsVariableOperandToken()
        {
            // Arrange
            const string expresson = " _variable_ ";

            // Act
            var list = GetTokens(expresson);

            // Assert
            var operandToken = list[0] as IOperandToken;
            AssertTypeOfOperand(Operand.Variable, operandToken);
        }

        /// <summary>
        /// Test if tokenizing a left parenthesis returns this specific meta token
        /// </summary>
        [Fact]
        public void TokenizeLeftParenthesisReturnsLeftParenthesisToken()
        {
            // Arrange
            const string expression = " (19 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var metaToken = list[0] as IMetaToken;
            AssertTypeOfMeta(Meta.LeftParenthesis, metaToken);
        }

        /// <summary>
        /// Test if tokenizing a right parenthesis returns this specific meta token
        /// </summary>
        [Fact]
        public void TokenizeRightParenthesisReturnsRightParenthesisToken()
        {
            // Arrange
            const string expression = " )+20 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var metaToken = list[0] as IMetaToken;
            AssertTypeOfMeta(Meta.RightParenthesis, metaToken);
        }

        /// <summary>
        /// Test if tokenizing a comma returns this specific meta token
        /// </summary>
        [Fact]
        public void TokenizeCommaReturnsCommaToken()
        {
            // Arrange
            const string expression = " ,21 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var metaToken = list[0] as IMetaToken;
            AssertTypeOfMeta(Meta.Comma, metaToken);
        }

        /// <summary>
        /// Test if tokenizing a asterisk character returns an multiplication operator token
        /// </summary>
        [Fact]
        public void TokenizeAsteriskReturnsMultiplicationOperatorToken()
        {
            // Arrange
            const string expression = " *22 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[0] as IOperatorToken;
            AssertTypeOfOperator(Operator.Multiplication, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a slash character returns an division operator token
        /// </summary>
        [Fact]
        public void TokenizeSlashReturnsDivisionOperatorToken()
        {
            // Arrange
            const string expression = " /23 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[0] as IOperatorToken;
            AssertTypeOfOperator(Operator.Division, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a plus character returns addition token
        /// </summary>
        [Fact]
        public void TokenizePlusAfterNumberReturnsAdditionOperatorToken()
        {
            // Arrange
            const string expression = " 24+ ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.Addition, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a plus character returns addition token
        /// </summary>
        [Fact]
        public void TokenizePlusAfterRightParenthesisReturnsAdditionOperatorToken()
        {
            // Arrange
            const string expression = " )+ ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.Addition, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a minus character returns subtraction token
        /// </summary>
        [Fact]
        public void TokenizeMinusAfterNumberReturnsSubtractionOperatorToken()
        {
            // Arrange
            const string expression = " 25- ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.Subtraction, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a minus character returns subtraction token
        /// </summary>
        [Fact]
        public void TokenizeMinusAfterRightParenthesisReturnsSubtractionOperatorToken()
        {
            // Arrange
            const string expression = " )- ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.Subtraction, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a plus character returns unary plus token
        /// </summary>
        [Fact]
        public void TokenizePlusAsFirstTokenReturnsUnaryPlusOperatorToken()
        {
            // Arrange
            const string expression = " +26 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[0] as IOperatorToken;
            AssertTypeOfOperator(Operator.UnaryPlus, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a plus character returns unary plus token
        /// </summary>
        [Fact]
        public void TokenizePlusAfterLeftParenthesisReturnsUnaryPlusOperatorToken()
        {
            // Arrange
            const string expression = " (+26 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.UnaryPlus, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a plus character returns unary plus token
        /// </summary>
        [Fact]
        public void TokenizePlusAfterCommaReturnsUnaryPlusOperatorToken()
        {
            // Arrange
            const string expression = " ,+26 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.UnaryPlus, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a plus character returns unary plus token
        /// </summary>
        [Fact]
        public void TokenizePlusAfterOperatorReturnsUnaryPlusOperatorToken()
        {
            // Arrange
            const string expression = " *+26 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.UnaryPlus, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a minus character returns unary minus token
        /// </summary>
        [Fact]
        public void TokenizeMinusAsFirstTokenReturnsUnaryMinusOperatorToken()
        {
            // Arrange
            const string expression = " -26 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[0] as IOperatorToken;
            AssertTypeOfOperator(Operator.UnaryMinus, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a minus character returns unary minus token
        /// </summary>
        [Fact]
        public void TokenizeMinusAfterLeftParenthesisReturnsUnaryMinusOperatorToken()
        {
            // Arrange
            const string expression = " (-26 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.UnaryMinus, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a minus character returns unary minus token
        /// </summary>
        [Fact]
        public void TokenizeMinusAfterCommaReturnsUnaryMinusOperatorToken()
        {
            // Arrange
            const string expression = " ,-26 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.UnaryMinus, operatorToken);
        }

        /// <summary>
        /// Test if tokenizing a minus character returns unary minus token
        /// </summary>
        [Fact]
        public void TokenizeMinusAfterOperatorReturnsUnaryMinusOperatorToken()
        {
            // Arrange
            const string expression = " *-26 ";

            // Act
            var list = GetTokens(expression);

            // Assert
            var operatorToken = list[1] as IOperatorToken;
            AssertTypeOfOperator(Operator.UnaryMinus, operatorToken);
        }
        #endregion

        #region helper methods
        /// <summary>
        /// Asserts if the type of the token is as expected
        /// </summary>
        /// <param name="expectedType">The expected operand type</param>
        /// <param name="operandToken">The operand token containing the actual type value</param>
        private static void AssertTypeOfOperand(Operand expectedType, IOperandToken operandToken)
        {
            if (operandToken == null)
            {
                throw new Exception("no operand token was provided");
            }

            Assert.Equal(expectedType, operandToken.Type);
        }

        /// <summary>
        /// Asserts if the type of the token is as expected
        /// </summary>
        /// <param name="expectedType">The expected meta type</param>
        /// <param name="metaToken">The meta token containing the actual type value</param>
        private static void AssertTypeOfMeta(Meta expectedType, IMetaToken metaToken)
        {
            if (null == metaToken)
            {
                throw new Exception("no meta token was provided");
            }

            Assert.Equal(expectedType, metaToken.Type);
        }

        /// <summary>
        /// Asserts if the type of the token is as expected
        /// </summary>
        /// <param name="expectedType">The expected operator type</param>
        /// <param name="operatorToken">The operator token containing the actual type value</param>
        private static void AssertTypeOfOperator(Operator expectedType, IOperatorToken operatorToken)
        {
            if (null == operatorToken)
            {
                throw new Exception("no operator token was provided");
            }

            Assert.Equal(expectedType, operatorToken.Type);
        }

        /// <summary>
        /// Action for many test to trigger the <see cref="Tokenizer"/>
        /// </summary>
        /// <param name="expression">expression to tokenize</param>
        /// <returns>The resulting list of tokens</returns>
        private static IList<IToken> GetTokens(string expression)
        {
            return expression.Tokenize().ToList();
        }
        #endregion
    }
}
