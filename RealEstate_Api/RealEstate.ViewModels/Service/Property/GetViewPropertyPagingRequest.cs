using RealEstate.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.Service.Property
{
    public class GetViewPropertyPagingRequest : PagingRequestBase
    {
        public int? typeOfTransactionId { get; set; }
    }
}
