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
    public class UserController : BaseController
    {
        UserBLL bll = new UserBLL();

        // GET: Admin/User
        public ActionResult UserList()
        {
            List<UserDTO> model = new List<UserDTO>();
            model = bll.GetUsers();
            return View(model);
        }

        public ActionResult AddUser()
        {
            UserDTO model = new UserDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(UserDTO model)
        {
            if (model.UserImage == null)
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
            string ext = Path.GetExtension(model.UserImage.FileName);
            string filename = uniqueNumber + ext;

            // Resize image if needed
            using (Bitmap UserImage = new Bitmap(model.UserImage.InputStream))
            {
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);

                // Save image to the server
                string imagePath = Server.MapPath("~/Areas/Admin/Content/UserImage/" + filename);
                resizeImage.Save(imagePath);

                // Update model with image path
                model.Imagepath = filename;
            }

            // Add user using BLL
            bll.AddUser(model);

            ViewBag.ProcessState = General.Messages.AddSuccess;
            ModelState.Clear();
            return View(new UserDTO());
        }

        public ActionResult UpdateUser(int ID)
        {
            UserDTO dto = new UserDTO();
            dto = bll.GetUserWithID(ID);
            return View(dto);   
        }

        [HttpPost]
        public ActionResult UpdateUser(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserImage != null)
                {
                    string uniqueNumber = Guid.NewGuid().ToString();
                    string ext = Path.GetExtension(model.UserImage.FileName);
                    string filename = uniqueNumber + ext;

                    // Resize image if needed
                    using (Bitmap UserImage = new Bitmap(model.UserImage.InputStream))
                    {
                        Bitmap resizeImage = new Bitmap(UserImage, 128, 128);

                        // Save image to the server
                        string imagePath = Server.MapPath("~/Areas/Admin/Content/UserImage/" + filename);
                        resizeImage.Save(imagePath);

                        // Update model with image path
                        model.Imagepath = filename;
                    }
                }

                string oldImagePath = bll.UpdateUser(model);

                if (model.UserImage != null)
                {
                    // Delete old image if it exists
                    string oldImageFullPath = Server.MapPath("~/Areas/Admin/Content/UserImage/" + oldImagePath);
                    if (System.IO.File.Exists(oldImageFullPath))
                    {
                        System.IO.File.Delete(oldImageFullPath);
                    }
                }

                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            return View(model);
        }

        public JsonResult DeleteUser(int ID)
        {
            string imagepath = bll.DeleteUser(ID);
            if (System.IO.File.Exists(Server.MapPath("~/Areas/Admin/Content/UserImage/" + imagepath)))
            {
                System.IO.File.Delete(Server.MapPath("~/Areas/Admin/Content/UserImage/" + imagepath));
            }
            return Json("");
        }
    }
}
