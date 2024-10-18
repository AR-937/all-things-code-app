using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AdsDAO
    {
        public int AddAds(Ad ads)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                try
                {
                    db.Ads.Add(ads);
                    db.SaveChanges();
                    return ads.ID;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public string DeleteAds(int ID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Ad ads = db.Ads.First(x => x.ID == ID);
                string imagepath = ads.ImagePath;
                ads.isDeleted = true;
                ads.DeletedDate = DateTime.Now;
                ads.LastUpdateDate = DateTime.Now;
                ads.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
                return imagepath;
            }
        }

        public List<AdsDTO> GetAds()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<Ad> list = db.Ads.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
                List<AdsDTO> dtolist = new List<AdsDTO>();
                foreach (var item in list)
                {
                    AdsDTO dto = new AdsDTO();
                    dto.ID = item.ID;
                    //dto.Name = item.Name;
                    dto.Link = item.Link;
                    dto.ImagePath = item.ImagePath;
                    dtolist.Add(dto);
                }
                return dtolist;
            }
        }
    }
}
