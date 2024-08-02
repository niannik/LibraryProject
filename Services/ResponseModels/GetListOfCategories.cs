using Services.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ResponseModels
{
    public class GetListOfCategories
    {
        public List<GetCategoriesModel> CategoriesList { get; set; }
    }
}
