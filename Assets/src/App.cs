using System.Text;
using BinanceApp.ExchangeService;
using BinanceApp.PingService;
using BinanceApp.SpotService;
using BinanceApp.TimeService;

namespace BinanceApp
{
    public class App : IApp
    {
        private const string BASE_URL = "https://api.binance.com";

        private const string API_KEY = "MCRHqIVL4G7G2OrFYosY8c8HgeAboy3wjC1b8x1AHlY0Mqnax67s3dnTfQyF2xgG";
        private const string SECRET_KEY = "LasxSY0aaf26ACSbjmRg08XJu4MIR2zJvPHp9D76b4gWjC8HN987yAdvHLWLCqqo";

        private bool isRunning = true;

        private HttpClient httpClient = null;

        private BinanceExchangeService exchangeService = null;
        private BinancePingService pingService = null;
        private BinanceSpotService spotService = null;
        private BinanceTimeService timeService = null;

        public bool IsRunning => this.isRunning;
        public string BaseUrl => BASE_URL;
        public string ApiKey => API_KEY;
        public string SecretKey => SECRET_KEY;

        public HttpClient HttpClient => this.httpClient;

        public App()
        {
            this.httpClient = new HttpClient();

            this.exchangeService = new BinanceExchangeService(this, "/api/v3/exchangeInfo?symbol={0}");
            this.pingService = new BinancePingService(this, "/api/v3/ping");
            this.spotService = new BinanceSpotService(this, "/api/v3/account");
            this.timeService = new BinanceTimeService(this, "/api/v3/time");
        }

        public void StartApp()
        {
            this.exchangeService.StartService();
            this.pingService.StartService();
            this.spotService.StartService();
            this.timeService.StartService();

            if (!this.TryPingBaseUrl() || !this.TryApiPingRequest())
            {
                Console.WriteLine("Failed to ping app.");
                return;
            }

            this.isRunning = true;
        }

        public void UpdateApp()
        {
            if (!this.TryGetServerTime())
            {
                Console.WriteLine("Failed to get server time.");
                return;
            }

            // if (!this.TryGetExchangeAssetInfo("BTCBUSD"))
            // {
            //     Console.WriteLine("Failed to get asset info.");
            //     return;
            // }

            // if (!this.TryGetSpotAccount())
            // {
            //     Console.WriteLine("Failed to get spot account.");
            //     return;
            // }

            // if (!this.TryGetAllOrders("BTCUSDT"))
            // {
            //     Console.WriteLine("Failed to get all orders.");
            //     return;
            // }

            if (!this.TryGetAllTrades("BTCUSDT"))
            {
                Console.WriteLine("Failed to get all orders.");
                return;
            }
        }

        private bool TryGetSpotAccount()
        {
            Task<BinanceSpotAccountInformation?> spotTask = Task.Run<BinanceSpotAccountInformation?>(async () => await this.spotService.GetSpotAccountInformation());
            if (spotTask.Result != null)
            {
                // Console.WriteLine($"Spot account: {spotTask.Result.ToString()}");
                return true;
            }
            return false;
        }

        private bool TryGetAllOrders(string symbol)
        {
            Task<IEnumerable<BinanceSpotSymbolOrder>?> spotTask = Task.Run<IEnumerable<BinanceSpotSymbolOrder>?>(async () => await this.spotService.GetAllOrders(symbol));
            if (spotTask.Result != null)
            {
                // StringBuilder stringBuilder = new StringBuilder();
                // foreach (SpotSymbolOrder order in spotTask.Result)
                // {
                //     stringBuilder.AppendLine(order.ToString());
                // }

                // Console.WriteLine($"Orders: {stringBuilder.ToString()}");
                return true;
            }
            return false;
        }

        private bool TryGetAllTrades(string symbol)
        {
            Task<IEnumerable<BinanceSpotSymbolTrade>?> spotTask = Task.Run<IEnumerable<BinanceSpotSymbolTrade>?>(async () => await this.spotService.GetAllTrades(symbol));
            if (spotTask.Result != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (BinanceSpotSymbolTrade order in spotTask.Result)
                {
                    stringBuilder.AppendLine(order.ToString());
                }

                Console.WriteLine($"Trades: {stringBuilder.ToString()}");
                return true;
            }
            return false;
        }

        private bool TryGetExchangeAssetInfo(string pairName)
        {
            Task<BinanceExchangeInfo?> exchangeAssetTask = Task.Run<BinanceExchangeInfo?>(async () => await this.exchangeService.GetExchangeSymbol(pairName));
            if (exchangeAssetTask.Result != null)
            {
                // Console.WriteLine($"Exchange result: {exchangeAssetTask.Result.ToString()}");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to get server date time.
        /// </summary>
        /// <returns>Returns if it was possible to get the server time.</returns>
        private bool TryGetServerTime()
        {
            Task<DateTime?> serverTimeTask = Task.Run<DateTime?>(async () => await this.timeService.GetServerTime());
            if (serverTimeTask.Result != null)
            {
                Console.WriteLine("Server time: " + serverTimeTask.Result.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to send ping request to the server.
        /// </summary>
        /// <returns>Returns if it was possible to send a ping request.</returns>
        private bool TryApiPingRequest()
        {
            Task<BinancePing?> pingTask = Task.Run<BinancePing?>(async () => await this.pingService.ApiPingRequest());
            return pingTask?.Result != null ? !pingTask.Result.Value.isInvalid : false;
        }

        /// <summary>
        /// Tries to ping the base url using the .donet ping class.
        /// </summary>
        /// <returns>Returns if the ping is successfull.</returns>
        private bool TryPingBaseUrl()
        {
            Task<bool> updateTask = Task.Run<bool>(async () => await this.pingService.PingBaseUrl());
            return updateTask.Result;
        }
    }
}