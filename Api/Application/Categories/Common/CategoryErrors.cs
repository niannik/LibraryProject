using Application.Common;

namespace Application.Categories.Common;

public class CategoryErrors
{
    public static Error CategoyIsAlreadyExist = new("Categoy Is Already Exist .", "Categoy_Is_Already_Exist");
    public static Error CategoyNotFound = new("Categoy Not Found", "Categoy_Not_Found");
}
