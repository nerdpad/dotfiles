<Query Kind="Program">
  <NuGetReference>AutoMapper</NuGetReference>
  <Namespace>AutoMapper</Namespace>
</Query>

void Main()
{
	Mapper.Reset();
	Mapper.Initialize(m =>
	{
		m.CreateMap<BaseOption, BaseElement>()
			.ConstructUsing(s => CreateElement(s));
			
		m.CreateMap<BaseOption, BaseElementPresentation>()
			.ConstructUsing(s => CreateElementPresentation(s));
			
		m.CreateMap<DecimalOption, DecimalElement>()
			.IncludeBase<BaseOption, BaseElement>()
			.ForMember(de => de.DefaultChecked, opt => opt.MapFrom(o => o.DefaultChecked));
			
		m.CreateMap<DecimalOption, DecimalElementPresentation>()
			.IncludeBase<BaseOption, BaseElementPresentation>()
			.ForMember(de => de.Label, opt => opt.MapFrom(o => o.Label));
		
		m.CreateMap<BaseOption, Split>()
			.ConstructUsing(s => new Split())
			.AfterMap((source, destination, ctx) =>
			{
				destination.Element = ctx.Mapper.Map<BaseElement>(source);
				destination.Presentation = ctx.Mapper.Map<BaseElementPresentation>(source);
			});
		m.CreateMap<DecimalOption, Split>()
			.IncludeBase<BaseOption, Split>();
	});

	var option = new DecimalOption()
	{
		DefaultChecked = true,
		Label = "Enter a number:"
	};
	
	Mapper.Map<BaseOption, Split>(option).Dump();
}

private BaseElement CreateElement(BaseOption option)
{
	switch (option.Type)
	{
		case ElementType.Decimal:
			return new DecimalElement();
		default:
		case ElementType.CheckBox:
			throw new InvalidCastException();
	}
}

private BaseElementPresentation CreateElementPresentation(BaseOption option)
{
	switch (option.Type)
	{
		case ElementType.Decimal:
			return new DecimalElementPresentation();
		default:
		case ElementType.CheckBox:
			throw new InvalidCastException();
	}
}

// Define other methods and classes here
enum ElementType 
{
	Decimal = 1,
	CheckBox = 2
}

class Split
{
	public BaseElement Element { get; set; }
	public BaseElementPresentation Presentation { get; set; }
}

// options
class BaseOption
{
	public BaseOption(ElementType type)
	{
		this.Type = type;
	}
	
	public ElementType Type { get; set; }
}

class DecimalOption : BaseOption
{
	public DecimalOption()
		: base(ElementType.Decimal)
	{
	}
	
	public string Label { get; set; }
	public bool DefaultChecked { get; set; }
}

class BaseElement
{
	public BaseElement(ElementType type)
	{
		this.Type = type;
	}

	public ElementType Type { get; set; }
}

class DecimalElement : BaseElement
{
	public DecimalElement()
		: base(ElementType.Decimal)
	{
	}
	
	public bool DefaultChecked { get; set; }
}

class BaseElementPresentation
{
	public BaseElementPresentation(ElementType type)
	{
		this.Type = type;
	}

	public ElementType Type { get; set; }
}

class DecimalElementPresentation : BaseElementPresentation
{
	public DecimalElementPresentation()
		: base(ElementType.Decimal)
	{
	}

	public string Label { get; set; }
}