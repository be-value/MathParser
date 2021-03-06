﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperatorTokenTests.cs" company="RDW">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser.Tests
{
    using Xunit;

    /// <summary>
    /// Tests of derivable properties for all the operators
    /// </summary>
    public class OperatorTokenTests
    {
        /// <summary>
        /// Test if unary plus operator precedence is Unary
        /// </summary>
        [Fact]
        public void UnaryPlusOperatorPrecedenceIsUnary()
        {
            IOperatorToken unaryPlus = new OperatorToken { Type = Operator.UnaryPlus };
            Assert.Equal(Precedence.Unary, unaryPlus.Precedence());
        }

        /// <summary>
        /// Test if unary plus operator associativity is R2L
        /// </summary>
        [Fact]
        public void UnaryPlusOperatorAssociativityIsRight2Left()
        {
            IOperatorToken unaryPlus = new OperatorToken { Type = Operator.UnaryPlus };
            Assert.Equal(Associativity.Right2Left, unaryPlus.Associativity());
        }

        /// <summary>
        /// Test if unary minus operator precedence is Unary
        /// </summary>
        [Fact]
        public void UnaryMinusOperatorPrecedenceIsUnary()
        {
            IOperatorToken unaryMinus = new OperatorToken { Type = Operator.UnaryMinus };
            Assert.Equal(Precedence.Unary, unaryMinus.Precedence());
        }

        /// <summary>
        /// Test if unary minus operator associativity is R2L
        /// </summary>
        [Fact]
        public void UnaryMinusOperatorAssociativityIsRight2Left()
        {
            IOperatorToken unaryMinus = new OperatorToken { Type = Operator.UnaryMinus };
            Assert.Equal(Associativity.Right2Left, unaryMinus.Associativity());
        }

        /// <summary>
        /// Test if multiplication operator precedence is Multiplicative
        /// </summary>
        [Fact]
        public void MultiplicationOperatorPrecedenceIsMultiplicative()
        {
            IOperatorToken multiplication = new OperatorToken { Type = Operator.Multiplication };
            Assert.Equal(Precedence.Multiplicative, multiplication.Precedence());
        }

        /// <summary>
        /// Test is multiplication operator associativity is L2R
        /// </summary>
        [Fact]
        public void MultiplicationOperatorAssociativityIsLeft2Right()
        {
            IOperatorToken multiplication = new OperatorToken { Type = Operator.Multiplication };
            Assert.Equal(Associativity.Left2Right, multiplication.Associativity());
        }

        /// <summary>
        /// Test if division operator precedence is Multiplicative
        /// </summary>
        [Fact]
        public void DivisionOperatorPrecedenceIsMultiplicative()
        {
            IOperatorToken division = new OperatorToken { Type = Operator.Division };
            Assert.Equal(Precedence.Multiplicative, division.Precedence());
        }

        /// <summary>
        /// Test is division operator associativity is L2R
        /// </summary>
        [Fact]
        public void DivisionOperatorAssociativityIsLeft2Right()
        {
            IOperatorToken division = new OperatorToken { Type = Operator.Division };
            Assert.Equal(Associativity.Left2Right, division.Associativity());
        }

        /// <summary>
        /// Test if addition operator precedence is Additive
        /// </summary>
        [Fact]
        public void AdditionOperatorPrecedenceIsAdditive()
        {
            IOperatorToken addition = new OperatorToken { Type = Operator.Addition };
            Assert.Equal(Precedence.Additive, addition.Precedence());
        }

        /// <summary>
        /// Test is addition operator associativity is L2R
        /// </summary>
        [Fact]
        public void AdditionOperatorAssociativityIsLeft2Right()
        {
            IOperatorToken addition = new OperatorToken { Type = Operator.Addition };
            Assert.Equal(Associativity.Left2Right, addition.Associativity());
        }

        /// <summary>
        /// Test if subtraction operator precedence is Additive
        /// </summary>
        [Fact]
        public void SubtractionOperatorPrecedenceIsAdditive()
        {
            IOperatorToken subtraction = new OperatorToken { Type = Operator.Subtraction };
            Assert.Equal(Precedence.Additive, subtraction.Precedence());
        }

        /// <summary>
        /// Test is subtraction operator associativity is L2R
        /// </summary>
        [Fact]
        public void SubtractionOperatorAssociativityIsLeft2Right()
        {
            IOperatorToken subtraction = new OperatorToken { Type = Operator.Subtraction };
            Assert.Equal(Associativity.Left2Right, subtraction.Associativity());
        }
    }
}
