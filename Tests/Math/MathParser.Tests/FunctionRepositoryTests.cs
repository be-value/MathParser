// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FunctionRepositoryTests.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser.Tests
{
    using System;
    using System.Collections.Generic;
    using Rhino.Mocks;
    using Xunit;

    /// <summary>
    /// Test class for the function repository functionality
    /// </summary>
    public class FunctionRepositoryTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionRepositoryTests"/> class.
        /// </summary>
        public FunctionRepositoryTests()
        {
            Mocks = new MockRepository();
            FunctionLoaderStub = Mocks.Stub<IFunctionLoader>();
            var functionList = new List<FunctionInfo> { new FunctionInfo { ArgumentCount = 1, Name = "sin" } };
            SetupResult.For(FunctionLoaderStub.GetFunctionInfo()).Return(functionList);
            Mocks.ReplayAll();            
        }

        /// <summary>
        /// Gets or sets Mocks.
        /// </summary>
        public MockRepository Mocks { get; set; }

        /// <summary>
        /// Gets or sets FunctionLoaderStub.
        /// </summary>
        public IFunctionLoader FunctionLoaderStub { get; set; }

        /// <summary>
        /// Test if passing a null value for the function loader throws an ArgumentNullException
        /// </summary>
        [Fact]
        public void ConstructorPassNullThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new FunctionRepository(null));
            Mocks.VerifyAll();
        }

        /// <summary>
        /// Test if the Contains method returns false on a non-existing function 
        /// </summary>
        [Fact]
        public void ContainsForNonExistingFunctionReturnsFalse()
        {
            // Arrange
            var repository = new FunctionRepository(FunctionLoaderStub);

            // Act
            var exists = repository.Contains("cos");

            // Assert
            Assert.Equal(false, exists);
            Mocks.VerifyAll();
        }

        /// <summary>
        /// Test if the Contains method returns true on an existing function
        /// </summary>
        [Fact]
        public void ContainsForExistingFunctionReturnsTrue()
        {
            // Arrange
            var repository = new FunctionRepository(FunctionLoaderStub);

            // Act
            var exists = repository.Contains("sin");

            // Assert
            Assert.Equal(true, exists);
            Mocks.VerifyAll();
        }
    }
}
