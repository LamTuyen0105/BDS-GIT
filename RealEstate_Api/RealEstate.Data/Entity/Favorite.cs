using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Entity
{
    public class Favorite
    {
        public Guid UserId { get; set; }
        public int PropertyId { get; set; }
        public bool Like { get; set; }

        public AppUser AppUser { get; set; }
        public Property Property { get; set; }
    }
}
