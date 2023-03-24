using INFORCE.Models;
using INFORCE.Models.Data;
using INFORCE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace INFORCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;
        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet]
        public async Task<ActionResult<List<URLs>>> GetTable()
        {
            return Ok(_urlService.GetUrls());
        }

       
        [HttpPost("CreateRecord")]
        public void CreateRecord([FromBody] URLs value)
        {
            _urlService.CreateUrl(value);
        }

        [HttpPut("UpdateRecord")]
        public IActionResult UpdateRecord([FromBody] URLs value)
        {
           return Ok(_urlService.UpdateUrl(value));
        }
        [HttpPut("{Name}")]
        public List<URLs> SerachRecord(string Name)
        {
            return _urlService.SearchByCodedUrl(Name);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<URLs>>> DeleteRecord(Guid id)
        {
            return Ok(await _urlService.DeleteUrl(id));
        }
    }
}
