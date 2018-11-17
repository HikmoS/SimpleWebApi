using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }

    }
}
