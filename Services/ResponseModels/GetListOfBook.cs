using Services.Models.BookModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ResponseModels
{
    public class GetListOfBook
    {
        public List<GetBookModel> ListOfBooks {  get; set; }
    }
}
