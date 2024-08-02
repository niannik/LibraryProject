using Services.Models.AuthorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ResponseModels
{
    public class GetListOfAuthors
    {
        public required List<GetAuthorModel> ListOfAuthors { get; set; }
    }
}
