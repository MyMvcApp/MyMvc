using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyMvc.Models.Models;
namespace MyMvc.Context
{
    //前端的表
    public partial class MyMvcContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<PagedPeoPle> PagedPeoPle { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public MyMvcContext() 
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    }
}
