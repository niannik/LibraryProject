using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.CategoryModels
{
    public class GetCategoriesModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
