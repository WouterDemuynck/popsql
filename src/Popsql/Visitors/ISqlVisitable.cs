using System.Security.Cryptography.X509Certificates;

namespace Popsql.Visitors
{
	/// <summary>
	/// Provides the implementing class with the ability to accept visitors to traverse their object tree.
	/// </summary>
	public interface ISqlVisitable
	{
		/// <summary>
		/// Accepts the specified <paramref name="visitor"/> and dispatches calls to the specific visitor
		/// methods for this object.
		/// </summary>
		/// <param name="visitor">
		/// The <see cref="ISqlVisitor" /> to visit this object with.
		/// </param>
		void Accept(ISqlVisitor visitor);
	}
}