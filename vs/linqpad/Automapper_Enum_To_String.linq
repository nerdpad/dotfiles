<Query Kind="Program">
  <NuGetReference>AutoMapper</NuGetReference>
  <Namespace>AutoMapper</Namespace>
</Query>

void Main()
{
	Mapper.Reset();
	Mapper.Initialize(m =>
	{
	});

	List<PolicyState> states = new List<PolicyState> { PolicyState.ENABLED, PolicyState.NOTCONFIGURED };
	
	var stringStates = Mapper.Map<List<PolicyState>, List<string>>(states);
	stringStates.Dump();
	
	Mapper.Map<List<string>, List<PolicyState>>(stringStates).Dump();
	
	
}

enum PolicyState
{
	NOTCONFIGURED,
	ENABLED,
	DISABLED
}