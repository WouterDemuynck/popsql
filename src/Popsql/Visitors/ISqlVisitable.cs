using System.Security.Cryptography.X509Certificates;

namespace Popsql.Visitors
{
	public interface ISqlVisitable
	{
		void Accept(ISqlVisitor visitor);
	}
}