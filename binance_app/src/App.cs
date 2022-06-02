namespace BinanceApp
{
    public class App : IApp
    {
        private const string BASE_URL = "https://api.binance.com";

        private const string SECRET_KEY = "WTGEstCvE97D8FgLiUEf9LEAROnQHR93KVnQ0uqLHRGVdKyFTAOxjXENP6aCFYMq";

        private bool isRunning = true;

        private HttpClient httpClient = null;

        private BinanceExchangeService exchangeService = null;
        private BinancePingService pingService = null;
        private BinanceSpotService spotService = null;
        private BinanceTimeService timeService = null;

        public bool IsRunning => this.isRunning;
        public string BaseUrl => BASE_URL;
        public string SecretKey => SECRET_KEY;

        public HttpClient HttpClient => this.httpClient;

        public App()
        {
            this.httpClient = new HttpClient();

            this.exchangeService = new BinanceExchangeService(this, "/api/v3/exchangeInfo?symbol={0}");
            this.pingService = new BinancePingService(this, "/api/v3/ping");
            this.spotService = new BinanceSpotService(this, "/api/v3/account (HMAC SHA256)");
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

            if (!this.TryGetExchangeAssetInfo("BTCUSDT"))
            {
                Console.WriteLine("Failed to get asset info.");
                return;
            }
        }


        private bool TryGetExchangeAssetInfo(string pairName)
        {
            Task<BinanceExchangeAsset?> exchangeAssetTask = Task.Run<BinanceExchangeAsset?>(async () => await this.exchangeService.GetExchangeAsset(pairName));

            if (exchangeAssetTask.Result != null)
            {
                Console.WriteLine("Server time: " + exchangeAssetTask.Result.ToString());

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
            Task<bool> updateTask = Task.Run<bool>(async () => await this.pingService.ApiPingRequest());
            return updateTask.Result;
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