using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Common;
using RealEstate.Data.EF;
using RealEstate.Data.Entity;
using RealEstate.Data.Enum;
using RealEstate.Utilities.Exceptions;
using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.News;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Service.Properties
{
    public class PropertyNewsManagerService : IPropertyNewsManagerService
    {
        private readonly RealEstateDbContext _context;
        private readonly IStorageService _storageService;

        public PropertyNewsManagerService (RealEstateDbContext context, IStorageService storageService)
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
                news.ImagePath = await this.SaveFile(request.ImagePath);
            }
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news.Id;
        }

        public async Task<int> DeleteNews(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            if (news == null) throw new RealEstateException($"Không thể tìm thấy tin: {newsId}");
            _context.News.Remove(news);
            return await _context.SaveChangesAsync();
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

        public async Task<PagedResult<NewsViewModel>> GetAllNewsByPagging(PagingRequestBase request)
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

        public async Task<int> UpdateNews(NewsUpdateRequest request)
        {
            var news = await _context.News.FindAsync(request.Id);
            if (news == null) throw new RealEstateException($"Không thể tìm thấy mã tin: {request.Id}");
            news.Title = request.Title;
            news.Summary = request.Summary;
            news.Content = WebUtility.HtmlEncode(request.Content);
            if (request.ImagePath != null)
            {
                news.ImagePath = await this.SaveFile(request.ImagePath);
            }
            _context.News.Update(news);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStatus(int propertyId, Status newStatus)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property == null) throw new RealEstateException($"Không thể tìm thấy mã: {propertyId}");
            property.Status = newStatus;
            return await _context.SaveChangesAsync() > 0;
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