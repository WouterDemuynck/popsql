using System;
using System.Collections.Generic;
using System.Text;

namespace Popsql
{
	internal class SqlIdentifierParser
	{
		public static string[] Parse(string identifier)
		{
			if (identifier == null) throw new ArgumentNullException(nameof(identifier));
			if (identifier.Length == 0) throw new ArgumentException("The value of the identifier argument must be a non-empty string.", nameof(identifier));

			var tokens = new List<string>();
			StringBuilder currentToken = new StringBuilder();
			bool isQuoted = false;
			bool isDelimited = false;
			for (int currentIndex = 0; currentIndex < identifier.Length; currentIndex++)
			{
				char currentChar = identifier[currentIndex];

				switch (currentChar)
				{
					case '\"':
						if (isQuoted)
						{
							// Look forward to see if this quote is escaped.
							if ((currentIndex < (identifier.Length - 1) && identifier[currentIndex + 1]  == '\"'))
							{
								currentToken.Append(currentChar);
								currentIndex++;
								continue;
							}

							tokens.Add(currentToken.ToString());
							currentToken = new StringBuilder();
							isQuoted = false;
							continue;
						}
						
						if (!isDelimited)
						{
							isQuoted = true;
							continue;
						}

						currentToken.Append(currentChar);
						break;

					case '[':
						if (!isQuoted && !isDelimited)
						{
							isDelimited = true;
							continue;
						}

						currentToken.Append(currentChar);
						break;

					case ']':
						if (!isQuoted && isDelimited)
						{
							// Look forward to see if this end bracket is escaped.
							if (currentIndex < (identifier.Length - 1) && identifier[++currentIndex] == ']')
							{
								currentToken.Append(currentChar);
								continue;
							}

							tokens.Add(currentToken.ToString());
							currentToken = new StringBuilder();
							isDelimited = false;
							continue;
						}

						currentToken.Append(currentChar);
						break;

					case '.':
						if (!isQuoted && !isDelimited)
						{
							if (currentToken.Length > 0)
							{
								tokens.Add(currentToken.ToString());
								currentToken = new StringBuilder();
							}
							continue;
						}

						currentToken.Append(currentChar);
						break;

					default:
						currentToken.Append(currentChar);
						break;
				}
			}

			if (currentToken.Length > 0)
			{
				// Add parser left-overs too.
				tokens.Add(currentToken.ToString());
			}

			return tokens.ToArray();
		}
	}
}