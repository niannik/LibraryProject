namespace Application.Categories.Queries.AdminGetCategories;

public class AdminGetCategoriesResponse
{
    public required List<AdminCategoryItem> Items { get; set; }
}

public class AdminCategoryItem
{
    public required int Id { get; set; }
    public required string Title { get; set; }
}
