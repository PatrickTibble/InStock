namespace InStock.Frontend.Abstraction.Models
{
    public interface IIdentifiable : IIdentifiable<int>
	{

	}

	public interface IIdentifiable<out T> where T : IComparable
	{
		T Id { get; }
	}
}