using RealEstate.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int ProvinceId { get; set; }

        public Province Province { get; set; }

        public List<Ward> Wards { get; set; }
    }
}
