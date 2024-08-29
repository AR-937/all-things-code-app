using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace UI.Areas.Admin.Controllers
{
    public class VideoController : BaseController
    {
        VideoBLL bll = new VideoBLL();
        // GET: Admin/Video
        public ActionResult VideoList()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            dtolist = bll.GetVideos();
            return View(dtolist);
        }

        public ActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)] // This allows HTML input in model properties
        public ActionResult AddVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/" + path;

                // Construct the iframe tag without extra spaces
                model.VideoPath = $"<iframe width=\"300\" height=\"200\" src=\"{mergelink}\" title=\"YouTube video player\" frameborder=\"0\" allow=\"accelerometer;autoplay;clipboard-write;encrypted-media;gyroscope;picture-in-picture\" referrerpolicy=\"strict-origin-when-cross-origin\" allowfullscreen></iframe>";

                if (bll.AddVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            return View(model);
        }

        public ActionResult UpdateVideo(int ID)
        {
            VideoDTO dto = bll.GetVideoWithID(ID);
            return View(dto);
        }

        public ActionResult UpdateVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/" + path;

                // Construct the iframe tag without extra spaces
                model.VideoPath = $"<iframe width=\"300\" height=\"200\" src=\"{mergelink}\" title=\"YouTube video player\" frameborder=\"0\" allow=\"accelerometer;autoplay;clipboard-write;encrypted-media;gyroscope;picture-in-picture\" referrerpolicy=\"strict-origin-when-cross-origin\" allowfullscreen></iframe>";

                if (bll.UpdateVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            return View(model);
        }

        public JsonResult DeleteVideo(int ID)
        {
            bll.DeleteVideo(ID);
            return Json("");
        }
    }
}