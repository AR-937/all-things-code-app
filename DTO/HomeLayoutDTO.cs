using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HomeLayoutDTO
    {
        public List <CategoryDTO> Categories { get; set; }
        public SocialMediaDTO Facebook { get; set; }
        public SocialMediaDTO Instagram { get; set; }
        public SocialMediaDTO LinkedIn { get; set; }
        public SocialMediaDTO X { get; set; }
        public List<MetaDTO> Metalist { get; set; }
        public List<PostDTO> HotNews { get; set; }
    }
}
