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
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {


        private readonly ILogger<CacheController> _logger;
        private readonly ICache _cache;

        public CacheController(ILogger<CacheController> logger, ICache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet("{key}")]
        public object Get(string key)
        {
            return _cache.Get(key);
        }   
    }
}
