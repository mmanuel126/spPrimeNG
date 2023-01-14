using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworkCategory
    {
        public TbNetworkCategory()
        {
            TbNetworks = new HashSet<TbNetworks>();
        }

        public int CategoryId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TbNetworks> TbNetworks { get; set; }
    }
}
