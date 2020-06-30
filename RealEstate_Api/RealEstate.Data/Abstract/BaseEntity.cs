using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
