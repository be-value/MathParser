// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FunctionRepository.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    using System;
    using System.Linq;

    /// <summary>
    /// Repository containing function information to be used by calling clients
    /// </summary>
    public class FunctionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionRepository"/> class.
        /// </summary>
        /// <param name="functionLoader">
        /// The function loader.
        /// </param>
        public FunctionRepository(IFunctionLoader functionLoader)
        {
            FunctionLoader = functionLoader ?? throw new ArgumentNullException("functionLoader", "parameter cannot bu null");
        }

        /// <summary>
        /// Gets or sets FunctionLoader.
        /// </summary>
        private IFunctionLoader FunctionLoader { get; }

        /// <summary>
        /// Test if the repository contains a specific function
        /// </summary>
        /// <param name="functionName">The function name to find.</param>
        /// <returns>true if the function is present, false otherwise.</returns>
        public bool Contains(string functionName)
        {
            return FunctionLoader.GetFunctionInfo().Any(fInfo => fInfo.Name == functionName);
        }
    }
}
