using RealEstate.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RealEstate.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Common;
using RealEstate.Data.Entity;
using RealEstate.Utilities.Exceptions;
using System.Net.Http.Headers;
using System.IO;
using System.Net;
using RealEstate.ViewModels.Service.NewsManager;

namespace RealEstate.Application.Service.NewsManagers
{
    public class NewsManagerService : INewsManagerService
    {
        private readonly RealEstateDbContext _context;
        private readonly IStorageService _storageService;

        public NewsManagerService (RealEstateDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task AddSharecount(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            news.Share += 1;
            await _context.SaveChangesAsync();
        }

        public async Task AddViewcount(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            news.View += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> CreateNews(NewsCreateRequest request)
        {
            var news = new News()
            {
                Title = request.Title,
                Summary = request.Summary,
                Content = WebUtility.HtmlEncode(request.Content)
            };
            if (request.ImagePath != null)
            {
                news.ImagePath = await SaveFile(request.ImagePath);
            }
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news.Id;
        }

        public async Task<List<NewsViewModel>> GetAllNews()
        {
            var query = from n in _context.News
                        select new { n };
            var data = await query.Select(x => new NewsViewModel()
            {
                Id = x.n.Id,
                Title = x.n.Title,
                Summary = x.n.Summary,
                ImagePath = x.n.ImagePath,
                DateSubmitted = x.n.DateSubmitted
            }).ToListAsync();
            return data;
        }

        public async Task<PagedResult<NewsViewModel>> GetAllNewsByPaging(PagingRequestBase request)
        {
            var query = from n in _context.News
                        select new { n };
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new NewsViewModel()
                {
                    Id = x.n.Id,
                    Title = x.n.Title,
                    Summary = x.n.Summary,
                    ImagePath = x.n.ImagePath,
                    DateSubmitted = x.n.DateSubmitted
                }).ToListAsync();
            var pagedResult = new PagedResult<NewsViewModel>()
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pagedResult;
        }

        public async Task<NewsViewModel> GetNewsById(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            var newsViewModel = new NewsViewModel()
            {
                Id = news.Id,
                Title = news.Title,
                Summary = news.Summary,
                Content = WebUtility.HtmlDecode(news.Content),
                ImagePath = news.ImagePath,
                View = news.View,
                Share = news.Share,
                DateSubmitted = news.DateSubmitted
            };
            return newsViewModel;
        }

        public async Task<int> DeleteNews(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null) throw new RealEstateException($"Không thể tìm thấy tin: {newsId}");
            _context.News.Remove(news);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateNews(NewsUpdateRequest request)
        {
            var news = await _context.News.FindAsync(request.Id);
            if (news == null) throw new RealEstateException($"Không thể tìm thấy mã tin: {request.Id}");
            news.Title = request.Title;
            news.Summary = request.Summary;
            news.Content = WebUtility.HtmlEncode(request.Content);
            if (request.ImagePath != null)
            {
                news.ImagePath = await SaveFile(request.ImagePath);
            }
            _context.News.Update(news);
            return await _context.SaveChangesAsync();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
