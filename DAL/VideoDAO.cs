﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoDAO
    {
        public int AddVideo(Video video)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.Videos.Add(video);
                    db.SaveChanges();
                    return video.ID;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DeleteVideo(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Video video = db.Videos.First(x => x.ID == ID);
                    video.isDeleted = true;
                    video.DeletedDate = DateTime.Now;
                    video.LastUpdateDate = DateTime.Now;
                    video.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public List<VideoDTO> GetVideos()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
                List<VideoDTO> dtolist = new List<VideoDTO>();
                foreach (var item in list)
                {
                    VideoDTO dto = new VideoDTO();
                    dto.Title = item.Title;
                    dto.VideoPath = item.VideoPath;
                    dto.OriginalVideoPath = item.OriginalVideoPath;
                    dto.ID = item.ID;
                    dto.AddDate = item.AddDate;
                    dtolist.Add(dto);
                }
                return dtolist;
            }
        }

        public VideoDTO GetVideoWithID(int iD)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Video video = db.Videos.First(x => x.ID == iD);
                VideoDTO dto = new VideoDTO();
                dto.ID = video.ID;
                dto.OriginalVideoPath = video.OriginalVideoPath;
                dto.Title = video.Title;
                return dto;
            }
        }

        public void UpdateVideo(VideoDTO model)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    Video video = db.Videos.First(x => x.ID == model.ID);
                    video.VideoPath = model.VideoPath;
                    video.OriginalVideoPath = model.OriginalVideoPath;
                    video.Title = model.Title;
                    video.LastUpdateDate = DateTime.Now;
                    video.LastUpdateUserID = UserStatic.UserID;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
