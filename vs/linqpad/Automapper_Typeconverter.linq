<Query Kind="Program">
  <NuGetReference>AutoMapper</NuGetReference>
  <Namespace>AutoMapper</Namespace>
</Query>

void Main()
{
	Mapper.Reset();
	Mapper.Initialize(m =>
	{
		m.CreateMap<Person, Person>()
			.Include<Person, Manager>()
			.ConstructUsing(p => this.CreatePerson(p));
			
		m.CreateMap<Person, Manager>();
	});

	Mapper.Map<Person, Manager>(new Person { Type = PersonType.Employee, Name = "Blah" }).Dump();
}

Person CreatePerson(Person source)
{
	switch (source.Type)
	{
		case PersonType.Employee:
			return new Person();
		case PersonType.Manager:
			return new Manager();
	}
	
	return null;
}

// Define other methods and classes here
enum PersonType
{
	Employee = 1,
	Manager = 2
}

class Person
{
	public PersonType Type { get; set; }
	public string Name { get; set; }
}

class Manager : Person
{
	public int Manages { get; set; } = 3;
}

class PersonConverter : ITypeConverter<Person, Person>
{
	public Person Convert(Person source, Person destination, ResolutionContext context)
	{
		switch (source.Type)
		{
			case PersonType.Employee:
				return context.Mapper.Map<Person, Person>(source);
			case PersonType.Manager:
				return context.Mapper.Map<Person, Manager>(source);
		}
		
		return destination;
	}
}