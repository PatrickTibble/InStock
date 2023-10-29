namespace InStock.Common.Abstraction.Repositories.Base
{
    public interface IIdentifiable : IIdentifiable<int>
	{

	}

	public interface IIdentifiable<out T> where T : IComparable
	{
		T Id { get; }
	}
}