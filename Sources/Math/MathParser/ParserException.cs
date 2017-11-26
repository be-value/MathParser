// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserException.cs" company="RDW">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    using System;
    using System.Text;

    /// <inheritdoc />
    /// <summary>
    /// Defines the parser exceptions thrown by this mathematical parser
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Not being able to Serialize is OK for now, Reviewed")]
    public class ParserException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MathParser.ParserException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        /// <param name="lexeme">The lexeme of the token where the errors was first discovered</param>
        /// <param name="postion">The position in the string of the lexeme where the error was first discovered</param>
        public ParserException(string message, string lexeme, int postion)
            : base(message)
        {
            Lexeme = lexeme;
            Position = postion;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the actual Message of this exception
        /// </summary>
        public override string Message
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("Pos: ");
                sb.Append(Position);
                sb.Append(", ");
                sb.Append("Token '");
                sb.Append(Lexeme);
                sb.Append("' : ");
                sb.Append(base.Message);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets the lexeme of the token where the error was first discovered
        /// </summary>
        public string Lexeme { get; }

        /// <summary>
        /// Gets the position of the lexeme where the error was first discovered
        /// </summary>
        public int Position { get; }
    }
}
