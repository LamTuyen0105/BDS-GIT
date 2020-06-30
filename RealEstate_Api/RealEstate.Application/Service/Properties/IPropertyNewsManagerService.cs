using RealEstate.Data.Enum;
using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.News;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.Properties
{
    public interface IPropertyNewsManagerService
    {
        Task<bool> UpdateStatus(int propertyId, Status newStatus);

        Task<int> CreateNews(NewsCreateRequest request);

        Task<int> UpdateNews(NewsUpdateRequest request);

        Task<int> DeleteNews(int newsId);

        Task<NewsViewModel> GetNewsById(int newsId);

        Task AddViewcount(int newsId);

        Task AddSharecount(int newsId);

        Task<PagedResult<NewsViewModel>> GetAllNewsByPagging(PagingRequestBase request);

        Task<List<NewsViewModel>> GetAllNews();
    }
}