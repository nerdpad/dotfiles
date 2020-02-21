<Query Kind="Program" />

void Main()
{
	var input = "arbcabcbb";
	LengthOfLongestSubstring(input).Dump();
}

// Define other methods and classes here

public int LengthOfLongestSubstring(string s)
{
	int a_pointer = 0, b_pointer = 0, max = 0;
	
	var hash = new HashSet<char>();
	
	while (s.Length > b_pointer)
	{
		char c = s[b_pointer];

		if (!hash.Contains(c))
		{
			hash.Add(c);
			max = Math.Max(hash.Count, max);
			b_pointer++;
		}
		else
		{
			hash.Remove(s[a_pointer]);
			a_pointer++;
		}
	}
	
	return max;
}