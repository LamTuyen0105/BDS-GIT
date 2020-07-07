using RealEstate.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.Service.Property
{
    public class GetPostPropertyPagingRequest : PagingRequestBase
    {
        public List<int> TypeOfTransactionIds { get; set; }
    }
}
