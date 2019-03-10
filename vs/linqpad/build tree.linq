<Query Kind="Program" />

void Main()
{
	var data = new List<Category>
	{
		new Category { ID = 1, Name = "Root 1" },
		new Category { ID = 2, Name = "Root 2" },
		new Category { ID = 3, Name = "Child 1", ParentID = 1 },
		new Category { ID = 4, Name = "Child 1", ParentID = 1 },
		new Category { ID = 5, Name = "Child 2", ParentID = 2 },
		new Category { ID = 5, Name = "Child 3", ParentID = 5 }
	};

	data.Dump();

	var root = data.Where(d => !d.ParentID.HasValue).ToList();

	var queue = new Queue<Category>();

	foreach (var rootCategory in root)
	{
		queue.Enqueue(rootCategory);

		while (queue.Count > 0)
		{
			var parentCategory = queue.Dequeue();

			// find all the child categories for this parent category
			var childCategories = data.Where(d => d.ParentID == parentCategory.ID).ToList();

			foreach (var childCategory in childCategories)
			{
				// add child category to it's parent category
				parentCategory.Categories.Add(childCategory);

				// remove the child category from the flat list of categories
				data.Remove(childCategory);

				// queue the child category to find it's child categories
				queue.Enqueue(childCategory);
			}
		}
	}
	
	root.Dump();
}

// Define other methods and classes here
class Category
{
	public Category()
	{
		this.Categories = new List<Category>();
	}
	
	public int ID { get; set; }
	public int? ParentID { get; set; }
	public string Name { get; set; }
	public List<Category> Categories { get; set; }
}