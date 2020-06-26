using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.Property;
using RealEstate.ViewModels.Service.PropertyImage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.Properties
{
    public interface IPostPropertyService
    {
        Task<int> Create(PropertyCreateRequest request);

        Task<int> Update(PropertyUpdateRequest request);

        Task<int> Delete(int propertyId);

        Task<PropertyViewModel> GetById(int propertyId);

        Task<PagedResult<PropertyViewModel>> GetAllPaging(GetPostPropertyPagingRequest request);

        Task<int> AddImage(int propertyId, PropertyImageCreateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, PropertyImageUpdateRequest request);

        Task<PropertyImageViewModel> GetImageById(int imageId);

        Task<List<PropertyImageViewModel>> GetListImages(int propertyId);
    }
}
