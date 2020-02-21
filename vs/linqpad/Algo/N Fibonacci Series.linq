<Query Kind="Program" />

void Main()
{
	int n = 4;
	List<int> result = Result.fibonacci().Take(n).ToList();
	result.Dump();
}

// Define other methods and classes here
class Result
{

	/*
     * Complete the 'fibonacci' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts INTEGER n as parameter.
     */

	public static IEnumerable<int> fibonacci()
	{
		int n_2 = 1; // your rules; or start Fibonacci from 1st, not 0th item
		int n_1 = 1;

		yield return n_2;
		yield return n_1;

		while (true)
		{
			int n = n_2 + n_1;

			yield return n;

			n_2 = n_1;
			n_1 = n;
		}
	}

}