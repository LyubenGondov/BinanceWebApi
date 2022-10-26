using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConsumingBinanceWebApi.Models;
using Websocket.Client;
using System.Threading;
using System.Net.WebSockets;
using System.Text;
using ConsumingBinanceWebApi.Consuming;
using Microsoft.Extensions.Options;

namespace ConsumingBinanceWebApi.Controllers
{
    public class HomeController : Controller
    {
        
        private ISymbolRepository _repository;
        private IOptions<MyConfiguration> _myConfiguration;
        public HomeController(ISymbolRepository repository, IOptions<MyConfiguration> myConfiguration)
        {
            _repository = repository;
            _myConfiguration = myConfiguration;
            
        }

        
        public IActionResult Index()
        {
            //ConsumingClass consumingClass = new ConsumingClass(_repository, _myConfiguration);
            //await consumingClass.ConsumeETH();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string option)
        {
            option = Request.Form["Symbol"].ToString();
            ConsumingClass consumingClass = new ConsumingClass(_repository, _myConfiguration);
            switch (option)
            {
                case "0":
                    break;
                case "1":
                    await consumingClass.ConsumeBTC();
                    break;
                case "2":
                    await consumingClass.ConsumeETH();
                    break;
                case "3":
                    await consumingClass.ConsumeADA();
                    break;
                default:
                    break;
            }

            return View();
        }

    }
}
