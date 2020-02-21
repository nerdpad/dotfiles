<Query Kind="Program" />

void Main()
{
	var str = "abcbbbbcccbdddadacb"; // ans: "bcbbbbcccb" - 10
	var kstr = "abcadcacacaca"; // "cadcacacaca" - 11
	LengthOfLongestSubstringTwoDistinct(str).Dump();
	LengthOfLongestSubstringKDistinct(kstr, 2).Dump();
}

// Define other methods and classes here
public int LengthOfLongestSubstringTwoDistinct(string s)
{
	int max = 0;
	var map = new Dictionary<char, int>();
	int start = 0;
	
	for(int i = 0; i < s.Length; i++)
	{
		char c = s[i];
		
		// how many times have we seen this char
		if (map.ContainsKey(c))
		{
			map[c] = map[c] + 1;
		}
		else
		{
			map[c] = 1;
		}
		
		if (map.Count > 2)
		{
			max = Math.Max(max, i - start);
			
			while (map.Count > 2)
			{
				char t = s[start];
				int count = map[t];
				
				if (count > 1)
				{
					map[t] = count - 1;
				}
				else
				{
					map.Remove(t);
				}
				
				start++;
			}
		}
	}
	
	max = Math.Max(max, s.Length - start);
	
	return max;
}

public int LengthOfLongestSubstringKDistinct(string s, int k)
{
	int start = 0, end = 0, max = 0;
	var dic = new Dictionary<char, int>();
	
	while (end < s.Length)
	{
		char c = s[end];
		if (dic.ContainsKey(c))
		{
			dic[c]++;
		}
		else
		{
			dic[c] = 1;
		}
		
		while (dic.Count > k)
		{
			c = s[start];
			if (--dic[c] == 0)
			{
				dic.Remove(c);
			}
			
			start++;
		}
		
		max = Math.Max(max, end - start + 1);
		end++;
	}
	
	return max;
}