using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Contracts.DTO;
using URLShortener.Contracts.Services;

namespace URLShortener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URLInfoController : Controller
    {
        private IURLInfoService _urlInfoService;

        public URLInfoController(IURLInfoService urlInfoService)
        {
            _urlInfoService = urlInfoService;
        }

        [HttpGet]
        [Route("hitcount")]
        public async Task<ActionResult<long>> HitCount(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
                return BadRequest("Invalid Url!");

            return await _urlInfoService.GetURLHitCountAsync($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/urlinfo/", shortUrl);
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public async Task<ActionResult> Go([FromRoute] string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl))
                return BadRequest("Invalid short url!");

            var longUrl = await _urlInfoService.GetLongURLVersionAsync(shortUrl);
            if (string.IsNullOrWhiteSpace(longUrl))
                return NotFound("URL not found!");

            return Redirect(longUrl);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ICollection<URLInfoDTO>>> All()
        {
            return (await _urlInfoService.GetAllURLsAsync()).ToList();
        }
    }
}
