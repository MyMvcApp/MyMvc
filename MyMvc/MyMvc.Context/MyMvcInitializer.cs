using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MyMvc.Context
{
    public class MyMvcInitializer : DropCreateDatabaseIfModelChanges<MyMvcContext>
    {
        protected override void Seed(MyMvcContext context)
        {
            context.SaveChanges();
        }
    }
}
