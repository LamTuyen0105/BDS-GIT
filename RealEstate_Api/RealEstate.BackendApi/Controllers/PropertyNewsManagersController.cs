using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Service.Properties;
using RealEstate.Data.Enum;
using RealEstate.ViewModels.Common;
using RealEstate.ViewModels.Service.News;

namespace RealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyNewsManagersController : ControllerBase
    {
        private readonly IPropertyNewsManagerService _propertyNewsManagerService;
        public PropertyNewsManagersController (IPropertyNewsManagerService propertyNewsManagerService)
        {
            _propertyNewsManagerService = propertyNewsManagerService;
        }

        [HttpPatch("{propertyId}/{newStatus}")]
        public async Task<IActionResult> UpdateStatus(int propertyId, Status newStatus)
        {
            var isSuccessful = await _propertyNewsManagerService.UpdateStatus(propertyId, newStatus);
            if (isSuccessful)
                return Ok();
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetNews()
        {
            var news = await _propertyNewsManagerService.GetAllNews();
            return Ok(news);
        }

        [HttpGet("Paging")]
        public async Task<IActionResult> GetAllNewsByPaging([FromQuery] PagingRequestBase request)
        {
            var news = await _propertyNewsManagerService.GetAllNewsByPagging(request);
            return Ok(news);
        }

        [HttpGet("{newsId}")]
        public async Task<IActionResult> GetNewsById(int newsId)
        {
            var news = await _propertyNewsManagerService.GetNewsById(newsId);
            if (news == null)
                return BadRequest("Không tìm thấy được tin");
            return Ok(news);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews([FromForm] NewsCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsId = await _propertyNewsManagerService.CreateNews(request);
            if (newsId == 0)
                return BadRequest();
            var news = await _propertyNewsManagerService.GetNewsById(newsId);
            return CreatedAtAction(nameof(GetNewsById), new { id = newsId }, news);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNews([FromForm] NewsUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _propertyNewsManagerService.UpdateNews(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{newsId}")]
        public async Task<IActionResult> DeleteNews(int newsId)
        {
            var affectedResult = await _propertyNewsManagerService.DeleteNews(newsId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPatch("{newsId}/view")]
        public async Task AddView(int newsId)
        {
            await _propertyNewsManagerService.AddViewcount(newsId);
        }

        [HttpPatch("{newsId}/share")]
        public async Task AddShare(int newsId)
        {
            await _propertyNewsManagerService.AddSharecount(newsId);
        }
    }
}
