using System;

namespace Popsql
{
	internal abstract class OwnedBy<TParent> 
	{
		protected OwnedBy(TParent parent)
		{
			if (parent == null) throw new ArgumentNullException(nameof(parent));
			Parent = parent;
		}
		
		protected TParent Parent
		{
			get;
			private set;
		}
	}
}