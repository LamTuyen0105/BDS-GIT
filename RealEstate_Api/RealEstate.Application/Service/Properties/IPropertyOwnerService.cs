using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.Property;
using RealEstate.ViewModels.Service.PropertyImage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.NewsManagers
{
    public interface IPropertyOwnerService
    {
        Task<int> Create(PropertyCreateRequest request);

        Task<int> Update(PropertyUpdateRequest request);

        Task<int> Delete(int propertyId);

        Task<PropertyViewModel> GetById(int propertyId);

        Task <PagedResult<PropertyViewModel>> Find(GetFindPropertyPagingRequest request);

        Task<PagedResult<PropertyViewModel>> GetAllPaging(GetPostPropertyPagingRequest request);

        Task<PagedResult<PropertyViewModel>> GetPaging(GetPropertyPagingRequest request);

        Task<PagedResult<PropertyViewModel>> GetAllByTypeOfTransaction(GetViewPropertyPagingRequest request);

        Task<List<PropertyViewModel>> GetAll();

        Task<int> AddImage(int propertyId, PropertyImageCreateRequest request);

        Task<int> RemoveImage(int imageId);

        Task<int> UpdateImage(int imageId, PropertyImageUpdateRequest request);

        Task<PropertyImageViewModel> GetImageById(int imageId);

        Task<List<PropertyImageViewModel>> GetListImages(int propertyId);

    }
}
