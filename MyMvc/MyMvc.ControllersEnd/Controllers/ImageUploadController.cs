using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyMvc.Controllers.Common;

namespace MyMvc.ControllersEnd.Controllers
{
    public class ImageUploadController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            try
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase postedFile = files[i];
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileExtension = Path.GetExtension(fileName);

                    postedFile.SaveAs(Request.MapPath("~/UpImgs/") + DateTime.Now.ToString("HHmmssfff") + fileExtension);
                }
            }
            catch (Exception)
            {
                return Content("请确认上传的是图片");
            }

            return Content("上传成功");
        }
    }
}
