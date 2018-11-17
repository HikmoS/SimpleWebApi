using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApi.Model
{
    public class MyContext: DbContext
    {
        public DbSet<Product> Products { get; set; } 

        public MyContext(DbContextOptions<MyContext> options) : base(options) { } 
    }
}
