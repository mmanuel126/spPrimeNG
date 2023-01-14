using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworkCategoryType
    {
        public TbNetworkCategoryType()
        {
            TbNetworks = new HashSet<TbNetworks>();
        }

        public int CategoryTypeId { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }

        public virtual ICollection<TbNetworks> TbNetworks { get; set; }
    }
}
