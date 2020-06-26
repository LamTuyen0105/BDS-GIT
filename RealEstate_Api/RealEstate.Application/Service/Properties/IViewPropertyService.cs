using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.Property;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.Properties
{
    public interface IViewPropertyService
    {
        Task<PagedResult<PropertyViewModel>> GetAllByTypeOfTransaction(GetViewPropertyPagingRequest request);

        Task<List<PropertyViewModel>> GetAll();
    }
}
