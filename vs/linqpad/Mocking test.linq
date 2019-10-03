<Query Kind="Program">
  <NuGetReference>JustMock</NuGetReference>
  <NuGetReference>xunit</NuGetReference>
  <NuGetReference>Xunit.Runner.LinqPad</NuGetReference>
  <Namespace>Telerik.JustMock</Namespace>
  <Namespace>Xunit</Namespace>
  <Namespace>Xunit.Runner.LinqPad</Namespace>
  <CopyLocal>true</CopyLocal>
</Query>

void Main()
{
	XunitRunner.Run(Assembly.GetExecutingAssembly());
	var subject = new Subject(new ConsoleEventManager());
	subject.DoSomething("Zuhaib");
}

// Define other methods and classes here
public class TestSubject
{
	[Fact]
	public void Test()
	{
		var sender = Mock.Create<IConsoleEventManager>();
		var subject = Mock.Create<Subject>(sender);
		
		string actual = null;

		Mock.Arrange(() => sender.Send(Arg.IsAny<string>()))
			.DoInstead<string>((v) => { actual = v; });
		
		subject.DoSomething("Zuhaib");
		
		Assert.Equal("Zuhaib has changed", actual);
	}
}
public interface ISubject
{
	void DoSomething(string value);
}
public class Subject : ISubject
{
	private readonly IConsoleEventManager sender;
	
	public Subject(IConsoleEventManager sender)
	{
		this.sender = sender;
	}
	
	public void DoSomething(string value)
	{
		var v = Util.Change(value);
		
		this.sender.Send(v);
	}
}

public interface IConsoleEventManager
{
	void Send(string value);
}
public class ConsoleEventManager : IConsoleEventManager
{
	public void Send(string value)
	{
		Sender<string>.Instance.Send(value);
	}
}

public interface ISender<T>
{
	void Send(T value);
}
public class Sender<T> : ISender<T>
{
	private static readonly ISender<T> _instance = new Sender<T>();
	
	public static ISender<T> Instance
	{
		get {return _instance;}
	}
	
	public void Send(T value)
	{
		value.Dump();
	}
}

static class Util
{
	public static string Change(string value)
	{
		return $"{value} has changed";
	}
}