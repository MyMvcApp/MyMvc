using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MyMvc.Models.Models;
using MyMvc.Repository.Repository;
using MyMvc.Context;
using MyMvc.ControllerTemplate;
using MyMvc.IRepository.Repository;
namespace MyMvc.Controllers.Common
{
   public class StudentController : BaseEndCRUDController<Student,MyMvcContext>,IControlerTemplate<Student>
   {
       private IStudentRepository studentRepository;
       public StudentController() 
       {
           studentRepository = new StudentRepository(new MyMvcContext());
           BaseReposity = studentRepository;
       }
       
       public ActionResult Index()
       {
           return View(studentRepository.GetData());
       }

       //
       // GET: /Student/Details/5

       public ActionResult Details(int id = 0)
       {
           Student student = studentRepository.GetByID(id);
           if (student == null)
           {
               return HttpNotFound();
           }
           return View(student);
       }

       //
       // GET: /Student/Create

       public ActionResult Create()
       {
           return View();
       }

       //
       // POST: /Student/Create

       [HttpPost]
       public ActionResult Create(Student student)
       {
           if (ModelState.IsValid)
           {
               studentRepository.Create(student);
               return RedirectToAction("Index");
           }
           return View(student);
       }

       //
       // GET: /Student/Edit/5

       public ActionResult Edit(int id = 0)
       {
           Student student = studentRepository.GetByID(id);
           if (student == null)
           {
               return HttpNotFound();
           }
           return View(student);
       }

       //
       // POST: /Student/Edit/5

       [HttpPost]
       public ActionResult Edit(Student student)
       {
           if (ModelState.IsValid)
           {
               studentRepository.Update(student);
               return RedirectToAction("Index");
           }
           return View(student);
       }

       //
       // GET: /Student/Delete/5

       public ActionResult Delete(int id = 0)
       {
           Student student = studentRepository.GetByID(id);
           if (student == null)
           {
               return HttpNotFound();
           }
           return View(student);
       }

       //
       // POST: /Student/Delete/5

       [HttpPost, ActionName("Delete")]
       public ActionResult DeleteConfirmed(int id)
       {
           Student student = studentRepository.GetByID(id);
           studentRepository.Delete(id);
           return RedirectToAction("Index");
       }

       protected override void Dispose(bool disposing)
       {
           studentRepository.Dispose();
           base.Dispose(disposing);
       }
    }
}
