using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewWebApi.Models;


namespace NewWebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ISymbolRepository _repository;
        public HomeController(ISymbolRepository repository)
        {
            _repository = repository;
        }
        // GET api/{symbol}/AvgPriceTwentyFourh
        [Produces("application/json", "application/xml")]
        [Microsoft.AspNetCore.Mvc.HttpGet("{symbol}/AvgPriceTwentyFourh")]
        public decimal AvgPriceTwentyFourh([FromUri] string symbol)
        {
            return _repository.Get24AvgPrice(symbol);
        }

        // GET api/{symbol}/SimpleMovingAverage?n={numberOfDataPoints}&p={timePeriod}&s=[startDateTime]
        [Produces("application/json", "application/xml")]
        [Microsoft.AspNetCore.Mvc.HttpGet("{symbol}/SimpleMovingAverage?n={numberOfDataPoints}&p={timePeriod}&s=[startDateTime]")]
        public decimal SimpleMovingAverage([FromUri] string symbol, int n, string p, DateTime s)
        {
            return _repository.SimpleMovingAverage(symbol, n, p, s);
        }

    }
}