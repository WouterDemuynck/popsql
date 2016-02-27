namespace Popsql.Grammar
{
	/// <summary>
	/// Provides the <see cref="Go"/> method to construct a <see cref="SqlStatement"/> tree.
	/// </summary>
	/// <typeparam name="T">
	/// The type of <see cref="SqlStatement"/> to construct.
	/// </typeparam>
	public interface ISqlGo<out T>
		where T : SqlStatement
	{
		/// <summary>
		/// Returns the <see cref="SqlStatement"/> constructed by previous builder methods.
		/// </summary>
		/// <returns>
		/// The <see cref="SqlStatement"/> constructed by previous builder methods.
		/// </returns>
		T Go();
	}
}