using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _ZEEN.Models.ViewModels
{
    public class UserViewModel
    {
        public RegularUser user { get; set; }
        public List<RegularUser> users { get; set; }
        public Sale sale { get; set; }
        public List<RegularUser> sales { get; set; }

    }
}
