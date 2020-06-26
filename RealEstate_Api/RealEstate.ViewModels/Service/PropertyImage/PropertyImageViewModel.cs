using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.Service.PropertyImage
{
    public class PropertyImageViewModel
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public string LinkName { get; set; }

        public string Caption { get; set; }

        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }

        public int SortOrder { get; set; }

        public long FileSize { get; set; }
    }
}
