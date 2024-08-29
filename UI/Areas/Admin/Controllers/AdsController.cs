using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class AdsController : BaseController
    {
        AdsBLL bll = new AdsBLL();

        public ActionResult AdsList()
        {
            List<AdsDTO> Adslist = new List<AdsDTO>();
            Adslist = bll.GetAds();
            return View(Adslist);
        }
        // GET: Admin/Ads
        public ActionResult AddAds()
        {
            AdsDTO dto = new AdsDTO();
            return View(dto); // Pass the model to the view
        }

        [HttpPost]
        public ActionResult AddAds(AdsDTO model)
        {
            if (model.AdsImage == null || model.AdsImage.ContentLength == 0)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
                return View(model);
            }

            
            // Generate unique filename
            string uniqueNumber = Guid.NewGuid().ToString();
            string ext = Path.GetExtension(model.AdsImage.FileName);
            string filename = uniqueNumber + ext;

            // Ensure the directory exists
            string directoryPath = Server.MapPath("~/Areas/Admin/Content/AdsImage/");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Resize image if needed
            using (Bitmap UserImage = new Bitmap(model.AdsImage.InputStream))
            {
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);

                // Save image to the server
                string imagePath = Path.Combine(directoryPath, filename);
                resizeImage.Save(imagePath);

                // Update model with image path
                model.ImagePath = filename;
            }

            bll.AddAds(model);
            ViewBag.ProcessState = General.Messages.AddSuccess;
            ModelState.Clear();
            return RedirectToAction("AddAds");    
        }

        public JsonResult DeleteAds(int ID)
        {
            string imagepath = bll.DeleteAds(ID);
            if(System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/AdsImage" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/AdsImage" + imagepath));
            }
            return Json("");
        }
    }
}
