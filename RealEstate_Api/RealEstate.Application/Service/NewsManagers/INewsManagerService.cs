using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.NewsManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.NewsManagers
{
    public interface INewsManagerService
    {
        Task<NewsViewModel> GetNewsById(int newsId);

        Task<PagedResult<NewsViewModel>> GetAllNewsByPaging(PagingRequestBase request);

        Task<List<NewsViewModel>> GetAllNews();

        Task<int> CreateNews(NewsCreateRequest request);

        Task<int> UpdateNews(NewsUpdateRequest request);

        Task<int> DeleteNews(int newsId);

        Task AddViewcount(int newsId);

        Task AddSharecount(int newsId);
    }
}
