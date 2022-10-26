using ConsumingBinanceWebApi.Data;
using ConsumingBinanceWebApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ConsumingBinanceWebApi.Consuming
{
    public class ConsumingClass
    {
        private ISymbolRepository _reposotory;
        private readonly IOptions<MyConfiguration> _myConfiguration;

        public ConsumingClass(ISymbolRepository reposotory, IOptions<MyConfiguration> myConfiguration)
        {
            _reposotory = reposotory;
            _myConfiguration = myConfiguration;
        }

        public ConsumingClass()
        {
            
        }
        public bool IsvalidJson(string input)
        {
            try
            {
                var obj = JObject.Parse(input);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " " + e.StackTrace.ToString());
                return false;
            }
            
        }

        public bool IsJsonToObject(SymbolsHelp symbolsHelp, string input)
        {
            try
            {
                if (!IsvalidJson(input))
                {
                    return false;
                }
                else
                {
                    var obj = JObject.Parse(input);
                    symbolsHelp = obj.ToObject<SymbolsHelp>();
                    return true;
                }              
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " " + e.StackTrace.ToString());
                return false;
            }

        }

        public async Task ConsumeETH()
        {

            using (ClientWebSocket client = new ClientWebSocket())
            {
                string getConfigApiUrl = _myConfiguration.Value.ApiUrl;
                var url = new Uri(getConfigApiUrl+"ETH");
                var cts = new CancellationTokenSource();
                cts.CancelAfter(1200000000);
                try
                {
                    
                    string message = "{\"method\": \"SUBSCRIBE\", \"params\": [ \"ethusdt@aggTrade\", \"ethusdt@depth\"],\"id\": 1}";
                    await client.ConnectAsync(url, cts.Token);
                    while (client.State == WebSocketState.Open)
                    {
                        ArraySegment<byte> byteToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                        await client.SendAsync(byteToSend, WebSocketMessageType.Text, true, cts.Token);
                        var responseBuffer = new byte[1024];
                        var offset = 0;
                        var packet = 1024;
                        while (true)
                        {
                            ArraySegment<byte> byteReceived = new ArraySegment<byte>(responseBuffer, offset, packet);
                            WebSocketReceiveResult response = await client.ReceiveAsync(byteReceived, cts.Token);
                            string responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                            if (string.IsNullOrWhiteSpace(responseMessage))
                            { 
                                break; 
                            }
                            responseMessage = responseMessage.Trim();

                            if (!IsvalidJson(responseMessage))
                            {
                                break;
                            }
                            SymbolsHelp symbolHelp = new SymbolsHelp();
                            if (!IsJsonToObject(symbolHelp,responseMessage))
                            {
                                break;
                            }
                            var jsonResult =  JObject.Parse(responseMessage);                            
                            symbolHelp = jsonResult.ToObject<SymbolsHelp>();

                            Symbols symbol = new Symbols();
                            symbol.EventType = symbolHelp.e;
                            symbol.EventTime = symbolHelp.E;
                            symbol.Symbol = symbolHelp.s;
                            symbol.AgregateTradeId = symbolHelp.a;
                            symbol.Price = symbolHelp.p;
                            symbol.Quantity = symbolHelp.q;
                            symbol.FirstTradeID = symbolHelp.f;
                            symbol.LastTradeId = symbolHelp.l;
                            symbol.TradeTime = symbolHelp.T;
                            symbol.IsBuyer = symbolHelp.m;
                            symbol.Ignore = symbolHelp.M;
                            symbol.DateInsert = DateTime.UtcNow;

                            if (symbol.Price > 0)
                            {
                                _reposotory.Add(symbol);
                            }
                            
                            if (response.EndOfMessage)
                                break;
                        }
                    }
                }
                catch (WebSocketException e)
                {

                }
            }




        }

        public async Task ConsumeBTC()
        {

            using (ClientWebSocket client = new ClientWebSocket())
            {
                string getConfigApiUrl = _myConfiguration.Value.ApiUrl;
                var url = new Uri(getConfigApiUrl + "BTC");
                var cts = new CancellationTokenSource();
                cts.CancelAfter(1200000000);
                try
                {

                    string message = "{\"method\": \"SUBSCRIBE\", \"params\": [ \"btcusdt@aggTrade\", \"btcusdt@depth\"],\"id\": 1}";
                    await client.ConnectAsync(url, cts.Token);
                    while (client.State == WebSocketState.Open)
                    {
                        ArraySegment<byte> byteToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                        await client.SendAsync(byteToSend, WebSocketMessageType.Text, true, cts.Token);
                        var responseBuffer = new byte[1024];
                        var offset = 0;
                        var packet = 1024;
                        while (true)
                        {
                            ArraySegment<byte> byteReceived = new ArraySegment<byte>(responseBuffer, offset, packet);
                            WebSocketReceiveResult response = await client.ReceiveAsync(byteReceived, cts.Token);
                            string responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                            if (string.IsNullOrWhiteSpace(responseMessage))
                            {
                                break;
                            }
                            responseMessage = responseMessage.Trim();

                            if (!IsvalidJson(responseMessage))
                            {
                                break;
                            }
                            SymbolsHelp symbolHelp = new SymbolsHelp();
                            if (!IsJsonToObject(symbolHelp, responseMessage))
                            {
                                break;
                            }
                            var jsonResult = JObject.Parse(responseMessage);
                            symbolHelp = jsonResult.ToObject<SymbolsHelp>();

                            Symbols symbol = new Symbols();
                            symbol.EventType = symbolHelp.e;
                            symbol.EventTime = symbolHelp.E;
                            symbol.Symbol = symbolHelp.s;
                            symbol.AgregateTradeId = symbolHelp.a;
                            symbol.Price = symbolHelp.p;
                            symbol.Quantity = symbolHelp.q;
                            symbol.FirstTradeID = symbolHelp.f;
                            symbol.LastTradeId = symbolHelp.l;
                            symbol.TradeTime = symbolHelp.T;
                            symbol.IsBuyer = symbolHelp.m;
                            symbol.Ignore = symbolHelp.M;
                            symbol.DateInsert = DateTime.UtcNow;

                            if (symbol.Price > 0)
                            {
                                _reposotory.Add(symbol);
                            }

                            if (response.EndOfMessage)
                                break;
                        }
                    }
                }
                catch (WebSocketException e)
                {

                }
            }




        }

        public async Task ConsumeADA()
        {

            using (ClientWebSocket client = new ClientWebSocket())
            {
                string getConfigApiUrl = _myConfiguration.Value.ApiUrl;
                var url = new Uri(getConfigApiUrl + "ADA");
                var cts = new CancellationTokenSource();
                cts.CancelAfter(1200000000);
                try
                {

                    string message = "{\"method\": \"SUBSCRIBE\", \"params\": [ \"adausdt@aggTrade\", \"adausdt@depth\"],\"id\": 1}";
                    await client.ConnectAsync(url, cts.Token);
                    while (client.State == WebSocketState.Open)
                    {
                        ArraySegment<byte> byteToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                        await client.SendAsync(byteToSend, WebSocketMessageType.Text, true, cts.Token);
                        var responseBuffer = new byte[1024];
                        var offset = 0;
                        var packet = 1024;
                        while (true)
                        {
                            ArraySegment<byte> byteReceived = new ArraySegment<byte>(responseBuffer, offset, packet);
                            WebSocketReceiveResult response = await client.ReceiveAsync(byteReceived, cts.Token);
                            string responseMessage = Encoding.UTF8.GetString(responseBuffer, offset, response.Count);
                            if (string.IsNullOrWhiteSpace(responseMessage))
                            {
                                break;
                            }
                            responseMessage = responseMessage.Trim();

                            if (!IsvalidJson(responseMessage))
                            {
                                break;
                            }
                            SymbolsHelp symbolHelp = new SymbolsHelp();
                            if (!IsJsonToObject(symbolHelp, responseMessage))
                            {
                                break;
                            }
                            var jsonResult = JObject.Parse(responseMessage);
                            symbolHelp = jsonResult.ToObject<SymbolsHelp>();

                            Symbols symbol = new Symbols();
                            symbol.EventType = symbolHelp.e;
                            symbol.EventTime = symbolHelp.E;
                            symbol.Symbol = symbolHelp.s;
                            symbol.AgregateTradeId = symbolHelp.a;
                            symbol.Price = symbolHelp.p;
                            symbol.Quantity = symbolHelp.q;
                            symbol.FirstTradeID = symbolHelp.f;
                            symbol.LastTradeId = symbolHelp.l;
                            symbol.TradeTime = symbolHelp.T;
                            symbol.IsBuyer = symbolHelp.m;
                            symbol.Ignore = symbolHelp.M;
                            symbol.DateInsert = DateTime.UtcNow;

                            if (symbol.Price > 0)
                            {
                                _reposotory.Add(symbol);
                            }

                            if (response.EndOfMessage)
                                break;
                        }
                    }
                }
                catch (WebSocketException e)
                {

                }
            }




        }


    }
}
