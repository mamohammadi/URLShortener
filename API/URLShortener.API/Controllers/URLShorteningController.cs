using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortener.Contracts.Services;

namespace URLShortener.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URLShorteningController : Controller
    {
        private IURLShortenerService _urlShortenerervice;

        public URLShorteningController(IURLShortenerService urlShortenerService)
        {
            _urlShortenerervice = urlShortenerService;
        }

        [HttpGet]
        [Route("shorten")]
        public async Task<ActionResult<string>> Shorten(string url)
        {
            //Pre-Validate
            if (!_urlShortenerervice.IsValidUrl(url))
                return BadRequest("Provided url is not a valid URL!");

            var shortenResult = await _urlShortenerervice.GetShortURLVersionAsync(url);
            if (string.IsNullOrWhiteSpace(shortenResult))
                return StatusCode(500);

            return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/urlinfo/{shortenResult}";
        }
    }
}
