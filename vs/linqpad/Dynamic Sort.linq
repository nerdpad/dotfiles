<Query Kind="Program" />

void Main()
{
	var unsorted = from m in Movies select m;
	
	var sorter = new Sorter<Movie>();
	sorter.Register("Name", m1 => m1.Name);
	sorter.Register("Year", m2 => m2.Year);

	var instructions = new List<SortInstruction>()
							   {
								   // new SortInstruction() {Name = "Name", Direction = SortDirection.Descending },
								   new SortInstruction() {Name = "Year", Direction = SortDirection.Descending }
							   };
							   
	unsorted.SortBy(sorter, instructions).Dump();
}

// Define other methods and classes here
private static IQueryable<Movie> Movies
{
	get { return CreateMovies().AsQueryable(); }
}

private static IEnumerable<Movie> CreateMovies()
{
	yield return new Movie { Name = "B", Year = 1990 };
	yield return new Movie { Name = "A", Year = 2001 };
	yield return new Movie { Name = "A", Year = 2000 };
}

internal class Movie
{
	public string Name { get; set; }
	public int Year { get; set; }
}

internal static class SorterExtension
{
	public static IOrderedQueryable<T> SortBy<T>(this IQueryable<T> source, Sorter<T> sorter, IEnumerable<SortInstruction> instructions)
	{
		return sorter.SortBy(source, instructions);
	}
}

/// <summary>
/// A dynamic sorter.
/// </summary>
/// <typeparam name="TSource">The type of the source.</typeparam>
internal class Sorter<TSource>
{
	/// <summary>
	/// The first order of sorting passes.
	/// </summary>
	private readonly FirstPasses _FirstPasses;

	/// <summary>
	/// The first order of sorting passes in descending order.
	/// </summary>
	private readonly FirstPasses _FirstDescendingPasses;

	/// <summary>
	/// The next order of sorting passes.
	/// </summary>
	private readonly NextPasses _NextPasses;

	/// <summary>
	/// The next order of sorting passes in descending order.
	/// </summary>
	private readonly NextPasses _NextDescendingPasses;

	public Sorter()
	{
		this._FirstPasses = new FirstPasses();
		this._FirstDescendingPasses = new FirstPasses();
		this._NextPasses = new NextPasses();
		this._NextDescendingPasses = new NextPasses();
	}

	/// <summary>
	/// Registers the specified name.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <param name="name">The name.</param>
	/// <param name="selector">The selector.</param>
	public void Register<TKey>(string name, Expression<Func<TSource, TKey>> selector)
	{
		this._FirstPasses.Add(name, s => s.OrderBy(selector));
		this._FirstDescendingPasses.Add(name, s => s.OrderByDescending(selector));
		this._NextPasses.Add(name, s => s.ThenBy(selector));
		this._NextDescendingPasses.Add(name, s => s.ThenByDescending(selector));
	}

	/// <summary>
	/// Sorts the collection as per the sorting instructions.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="instructions">The sorting instructions.</param>
	/// <returns></returns>
	public IOrderedQueryable<TSource> SortBy(IQueryable<TSource> source, IEnumerable<SortInstruction> instructions)
	{
		IOrderedQueryable<TSource> result = null;

		foreach (var instruction in instructions)
		{
			result = result == null ? this.SortFirst(instruction, source) : this.SortNext(instruction, result);
		}

		return result;
	}

	/// <summary>
	/// Sort the collection as per instruction.
	/// </summary>
	/// <param name="instruction">The sorting instruction.</param>
	/// <param name="source">The source collection.</param>
	/// <returns>The result of the sorting operation.</returns>
	private IOrderedQueryable<TSource> SortFirst(SortInstruction instruction, IQueryable<TSource> source)
	{
		if (instruction.Direction == SortDirection.Ascending)
		{
			return this._FirstPasses[instruction.Name].Invoke(source);
		}

		return this._FirstDescendingPasses[instruction.Name].Invoke(source);
	}

	/// <summary>
	/// Sort the collection as per instruction.
	/// </summary>
	/// <param name="instruction">The sorting instruction.</param>
	/// <param name="source">The source collection.</param>
	/// <returns>The result of the sorting operation.</returns>
	private IOrderedQueryable<TSource> SortNext(SortInstruction instruction, IOrderedQueryable<TSource> source)
	{
		if (instruction.Direction == SortDirection.Ascending)
		{
			return this._NextPasses[instruction.Name].Invoke(source);
		}

		return this._NextDescendingPasses[instruction.Name].Invoke(source);
	}

	/// <summary>
	/// The properties to sort the collection with.
	/// </summary>
	private class FirstPasses : Dictionary<string, Func<IQueryable<TSource>, IOrderedQueryable<TSource>>>
	{
	}

	/// <summary>
	/// The properties to sort the collection with.
	/// </summary>
	private class NextPasses : Dictionary<string, Func<IOrderedQueryable<TSource>, IOrderedQueryable<TSource>>>
	{
	}
}

/// <summary>
/// The sorting instructions.
/// </summary>
internal class SortInstruction
{
	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>
	/// The name.
	/// </value>
	public string Name { get; set; }

	/// <summary>
	/// Gets or sets the direction.
	/// </summary>
	/// <value>
	/// The direction.
	/// </value>
	public SortDirection Direction { get; set; }
}

internal enum SortDirection
{
	//Note I have created this enum because the one that exists in the .net 
	// framework is in the web namespace...
	Ascending,
	Descending
}