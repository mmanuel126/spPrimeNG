using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbVideoCategory
    {
        public TbVideoCategory()
        {
            TbVideos = new HashSet<TbVideos>();
        }

        public int CategoryId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TbVideos> TbVideos { get; set; }
    }
}
