using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetsendBackend.Models;

using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore;

namespace VetsendBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        
        }

        public DbSet<Product> Products { get; set; }
        

    }
}
