using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO categorydao = new CategoryDAO();
        SocialMediaDAO socialdao = new SocialMediaDAO();
        MetaDAO metadao = new MetaDAO();
        PostDAO postdao = new PostDAO();

        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = categorydao.GetCategories();
            List<SocialMediaDTO> socialmedialist = new List<SocialMediaDTO>();
            socialmedialist = socialdao.GetSocialMedias();
            dto.Facebook = socialmedialist.First(x => x.Link.Contains("facebook"));
            dto.Instagram = socialmedialist.First(x => x.Link.Contains("instagram"));
            dto.X = socialmedialist.First(x => x.Link.Contains("x"));
            dto.LinkedIn = socialmedialist.First(x => x.Link.Contains("linkedin"));
            dto.Metalist = metadao.GetMetaData();
            dto.HotNews = postdao.GetHotNews();


            return dto;
        }
    }
}
