using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ResponseModels
{
    public class GetListOfUsers
    {
        public required List<User> ListOfUsers { get; set; }
    }
}
