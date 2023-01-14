using System;
using System.Collections.Generic;

namespace sportprofiles.DBContextModels
{
    public partial class TbNetworkRegulators
    {
        public int? NetworkId { get; set; }
        public int? OwnerId { get; set; }
        public int? AdministratorId { get; set; }

        public virtual TbMembers Administrator { get; set; }
        public virtual TbNetworks Network { get; set; }
        public virtual TbMembers Owner { get; set; }
    }
}
