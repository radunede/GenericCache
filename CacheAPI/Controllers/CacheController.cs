using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CacheAPI.Controllers
{
    [ApiController]
    [Route("api/cache")]
    public class CacheController : ControllerBase
    {


        private readonly ILogger<CacheController> _logger;
        private readonly ICache _cache;

        public CacheController(ILogger<CacheController> logger, ICache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]
        public string Get([FromQuery] string key)
        {
            return (string)_cache.Get(key);
        }

        [HttpPost]
        public void Put([FromQuery] string key, [FromQuery] string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                _cache.Add(key, value);
        }
    }
}
