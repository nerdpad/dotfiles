<Query Kind="Program" />

void Main()
{
	// can tour
	// int[] gas = new int[] {1, 2, 3, 4, 5};
	// int[] cost = new int[] {3, 4, 5, 1, 2};

	int[] gas = new int[] {1, 2, 3, 4, 5};
	int[] cost = new int[] {1, 3, 2, 4, 5};

	// can't tour
	// int[] gas = new int[] { 1, 2, 3, 4, 5 };
	// int[] cost = new int[] { 1, 3, 3, 4, 5 };

	GetTour(gas, cost).Dump();
	Tour(gas, cost).Dump();
	CanTour(gas, cost).Dump();
}

// Define other methods and classes here
int GetTour(int[] gas, int[] cost)
{
	int
	// sum of all the gas
	sum = 0,
	// gas I am going to need to get to the next station
	diff = 0,
	// starting gas station
	start = 0;
	
	for (int i = 0; i < gas.Length; i++)
	{
		sum = sum + gas[i] - cost[i];
		
		if (sum < 0)
		{
			// I do not have enough gas to travel to the next station
			start = i + 1; // change the starting point
			diff+=sum; // store the deficit
			sum = 0; // start over from the new station
			continue;
		}
	}
	
	// sum + diff < 0 means not gas in the entire route
	return sum + diff >= 0 ? start : -1;
}

int Tour(int[] gas, int[] cost)
{
	int
	totalGas = 0,
	totalLeftOverGas = 0,
	start = 0;
	
	for (int i = 0; i < gas.Length; i++)
	{
		// left over at the next station = available gas at the current
		int availableGas = gas[i];
		int costOfTravel = cost[i];
		int leftOverAtTheNextStation = availableGas - costOfTravel;
		totalLeftOverGas = totalLeftOverGas + leftOverAtTheNextStation;
		
		if (totalLeftOverGas < 0)
		{
			// we have no left over gas to travel to the next stop
			totalLeftOverGas = 0;
			start = i + 1; // start from the next station
			
		}
		
		totalGas = totalGas + leftOverAtTheNextStation;
	}
	
	return totalGas < 0 ? -1 : start;
}

int CanTour(int[] gas, int[] cost)
{
	int sumRemaining = 0; // track current remaining gas
	int total = 0; // tract total remaining
	int start = 0;
	
	for (int i = 0; i < gas.Length; i++)
	{
		int remaining = gas[i] - cost[i];
		
		// if sum remaining of (i-1) >= 0, continue
		if (sumRemaining >= 0)
		{
			sumRemaining += remaining;
		}
		else
		{
			// otherwise, reset start index to current
			sumRemaining = remaining;
			start = i;
		}
		
		total += remaining;
	}
	
	return total >= 0 ? start : -1;
}