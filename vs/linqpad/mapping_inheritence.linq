<Query Kind="Program">
  <NuGetReference>AutoMapper</NuGetReference>
  <Namespace>AutoMapper</Namespace>
</Query>

void Main()
{
	Mapper.Reset();
	Mapper.Initialize(m =>
	{
		m.CreateMap<SourcePerson, Person>(MemberList.Destination)
			.ConstructUsing(p => this.Factory(p))
			.BeforeMap((s, d, ctx) => ctx.Mapper.Map(s,d));
			
		m.CreateMap<SourcePerson, Employee>(MemberList.Destination)
			.ForMember(p => p.Type, o => o.MapFrom (p => p.SourceType))
			.ForMember(p => p.Name, o => o.MapFrom (p => p.SourceName))
			.ForMember(p => p.EmployeeNumber, o => o.MapFrom(p => p.SourceEmployeeNumber));
			
		m.CreateMap<SourcePerson, Manager>(MemberList.Destination)
			.ForMember(p => p.Type, o => o.MapFrom (p => p.SourceType))
			.ForMember(p => p.Name, o => o.MapFrom (p => p.SourceName))
			.ForMember(p => p.Manages, o => o.MapFrom(p => p.SourceManages));
	});

	Mapper.Map<SourcePerson, Person>(new SourcePerson { SourceType = PersonType.Manager, SourceName = "Blah", SourceEmployeeNumber = 3, SourceManages = 10 }).Dump();
}

// Define other methods and classes here
enum PersonType
{
	Unknown = 0,
	Employee = 1,
	Manager = 2
}

class SourcePerson
{
	public PersonType SourceType { get; set; }
	public string SourceName { get; set; }
	public int SourceEmployeeNumber { get; set; } = 2;
	public int SourceManages { get; set; } = 3;
}

class Person
{
	public Person()
	{
		this.Type = PersonType.Unknown;
	}

	protected Person(PersonType type)
	{
		this.Type = type;
	}

	private PersonType type;
	public PersonType Type
	{
		get
		{
			return this.type;
		}

		set
		{
			value.Dump();
			this.type = value;
		}
	}
	
	public string Name { get; set; }
}

class Employee : Person
{
	public Employee()
	: base(PersonType.Employee)
	{
	}

	public Employee(PersonType type)
		: base(type)
	{
	}

	public int EmployeeNumber { get; set; }
}

class Manager : Person 
{
	public Manager()
		: base(PersonType.Manager)
	{

	}
	
	public int Manages { get; set; } = 3;
}

Person Factory(SourcePerson source)
{
	switch (source.SourceType)
	{
		case PersonType.Manager:
			return new Manager();
		case PersonType.Employee:
			return new Employee();
		case PersonType.Unknown:
		default:
			return new Person();
	}
}
