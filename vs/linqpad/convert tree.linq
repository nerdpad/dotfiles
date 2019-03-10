<Query Kind="Program" />

void Main()
{
	var source = new List<Category>
	{
		new Category
		{
			ID = 1,
			Name = "Category 1",
			Policies = new List<Policy>
			{
				new Policy { ID = 1, Name ="Policy 1" }
			},
			Categories = new List<Category>
			{
				new Category
				{
					ID = 2,
					Name = "Category 2",
					Policies = new List<Policy>
					{
						new Policy { ID = 2, Name ="Policy 2" }
					},
					Categories = new List<Category>
					{
						new Category
						{
							ID = 1,
							Name = "Category 1",
							Policies = new List<Policy>
							{
								new Policy { ID = 1, Name ="Policy 1" }
							},
							Categories = new List<Category>
							{
								new Category
								{
									ID = 2,
									Name = "Category 2",
									Policies = new List<Policy>
									{
										new Policy { ID = 2, Name ="Policy 2" }
									},
									Categories = new List<Category>
									{
				
									}
								}
							}
						},
					}
				}
			}
		},
		new Category
		{
			ID = 3,
			Name = "Category 3",
			Policies = new List<Policy>
			{
				new Policy { ID = 3, Name ="Policy 3" }
			},
			Categories = new List<Category>
			{
				new Category
				{
					ID = 4,
					Name = "Category 4",
					Policies = new List<Policy>
					{
						new Policy { ID = 4, Name ="Policy 4" }
					},
					Categories = new List<Category>
					{
		
					}
				}
			}
		}
	};
	
	source.Dump();

	var target = Convert(source);
	
	target.Dump();
}

// Define other methods and classes here

List<Category> Convert(List<Category> categories)
{
	var result = new List<Category>();
	var queue = new Queue<Tuple<Category, Category>>();
	
	foreach (var rootCategory in categories)
	{
		queue.Enqueue(new Tuple<Category, Category>(null, rootCategory));
		
		while (queue.Count > 0)
		{
			var c = queue.Dequeue();
			
			var category = new Category
			{
				ID = c.Item2.ID,
				Name = c.Item2.Name,
				Policies = ConvertPolicies(c.Item2.Policies)
			};
			
			// queue the child items
			foreach (var childCategory in c.Item2.Categories)
			{
				queue.Enqueue(new Tuple<Category, Category>(category, childCategory));
			}
			
			if (c.Item1 != null)
			{
				c.Item1.Categories.Add(category);
				continue;
			}
			
			result.Add(category);
		}
	}
	
	return result;
}

List<Policy> ConvertPolicies(List<Policy> policies)
{
	return policies;	
}

class Policy
{
	public int ID { get; set; }
	public string Name { get; set; }
}

class Category
{
	public Category()
	{
		this.Categories = new List<Category>();
	}

	public int ID { get; set; }
	public string Name { get; set; }
	public List<Category> Categories { get; set; }
	public List<Policy> Policies { get; set; }
}
