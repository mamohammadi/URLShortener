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
            return await _urlInfoService.GetURLHitCountAsync(shortUrl);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ICollection<URLInfoDTO>>> All()
        {
            return (await _urlInfoService.GetAllURLsAsync()).ToList();
        }
    }
}
