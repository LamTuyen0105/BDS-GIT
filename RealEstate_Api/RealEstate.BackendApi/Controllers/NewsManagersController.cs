using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Service.NewsManagers;
using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.NewsManager;

namespace RealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsManagersController : ControllerBase
    {
        private readonly INewsManagerService _newsManagersService;
        public NewsManagersController(INewsManagerService newsManagersService)
        {
            _newsManagersService = newsManagersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var news = await _newsManagersService.GetAllNews();
            return Ok(news);
        }

        [HttpGet("Paging")]
        public async Task<IActionResult> GetAllNewsByPaging(PagingRequestBase request)
        {
            var news = await _newsManagersService.GetAllNewsByPaging(request);
            return Ok(news);
        }

        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetNewsById(int newsId)
        {
            var news = await _newsManagersService.GetNewsById(newsId);
            if (news == null)
                return BadRequest("Không tìm thấy được tin");
            return Ok(news);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews(NewsCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _newsManagersService.CreateNews(request);
            if (newsId == 0)
                return BadRequest();
            var news = await _newsManagersService.GetNewsById(newsId);
            return CreatedAtAction(nameof(GetNewsById), new { id = newsId }, news);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNews(NewsUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _newsManagersService.UpdateNews(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{newsId}")]
        public async Task<IActionResult> DeleteNews(int newsId)
        {
            var affectedResult = await _newsManagersService.DeleteNews(newsId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch("{newsId}/view")]
        public async Task AddView(int newsId)
        {
            await _newsManagersService.AddViewcount(newsId);
        }

        [HttpPatch("{newsId}/share")]
        public async Task AddShare(int newsId)
        {
            await _newsManagersService.AddSharecount(newsId);
        }
    }
}
