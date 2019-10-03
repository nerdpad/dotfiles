<Query Kind="Program" />

void Main()
{
	MyList source = new BMWList();
	(source is MyList).Dump();
	(source is CarList<Car>).Dump();
	(source is CarList<BMW>).Dump();
	(source is BMWList).Dump();
}

// Define other methods and classes here
class Car
{
	public string Name { get; set; }
}

class BMW : Car
{
	public BMW()
	{
		this.Name = "BMW";
	}
}

class VW : Car
{
	public VW()
	{
		this.Name = "VW";
	}
}

class MyList
{
	
}
class CarList<T> : MyList
	where T : Car
{
	public List<T> Cars { get; set; }
}

class BMWList : CarList<BMW>
{
	
}

class VMWList : CarList<VW>
{

}