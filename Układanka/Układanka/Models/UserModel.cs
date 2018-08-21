using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Układanka.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int clickNumber { get; set; }
        public DateTime time { get; set; }
    }
}
