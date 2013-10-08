using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMvc.Context;
namespace MyMvc.Repository
{
    //EF中事务处理的机制,例子,只有涉及到同时操作多个数据库的时候才会需要额外的处理，一般DBContext的SaveChange()就是一个对事务的处理。
    public class Transaction
    {
        MyMvcContext context1;
        MyMvcContext context2;
        public Transaction(MyMvcContext c1, MyMvcContext c2)
        {
            context1 = c1;
            context2 = c2;
        }

        public void SaveChanges() 
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    //Do something with context1
            //    //Do something with context2

            //    //Save Changes but don't discard yet
            //    context1.SaveChanges(false);

            //    //Save Changes but don't discard yet
            //    context2.SaveChanges(false);

            //    //if we get here things are looking good.
            //    scope.Complete();
            //    context1.AcceptAllChanges();
            //    context2.AcceptAllChanges();

            //}
        }
    }
}
