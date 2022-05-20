namespace BinanceApp
{
    public class App
    {
        private const string BASE_URL = "https://api.binance.com";

        private bool isRunning = true;

        private HttpClient httpClient = null;

        private BinanceExchangeService exchangeService = null;
        private BinancePingService pingService = null;
        private BinanceTimeService timeService = null;

        public App()
        {
            this.httpClient = new HttpClient();

            this.exchangeService = new BinanceExchangeService(this.httpClient, BASE_URL, "/api/v3/exchangeInfo?symbol={0}");
            this.pingService = new BinancePingService(this.httpClient, BASE_URL, "/api/v3/ping");
            this.timeService = new BinanceTimeService(this.httpClient, BASE_URL, "/api/v3/time");
        }

        public void StartApp()
        {
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

            if (!this.TryGetAssetInfo("BTCUSDT"))
            {
                Console.WriteLine("Failed to get asset info.");
                return;
            }
        }

        private bool TryGetAssetInfo(string pairName)
        {
            Task<BinanceExchangeAsset?> exchangeAssetTask = Task.Run<BinanceExchangeAsset?>(async () => await this.exchangeService.GetAsset(pairName));

            if (exchangeAssetTask.Result != null)
            {
                // Console.WriteLine("Server time: " + serverTimeTask.Result.ToString());
                return true;
            }

            return false;
        }

        /// Gets the server time.
        private bool TryGetServerTime()
        {
            Task<DateTime> serverTimeTask = Task.Run<DateTime>(async () => await this.timeService.GetServerTime());

            if (serverTimeTask.Result != null)
            {
                // Console.WriteLine("Server time: " + serverTimeTask.Result.ToString());
                return true;
            }

            return false;
        }

        /// Ping ap ping path to check if the app is running.
        private bool TryApiPingRequest()
        {
            Task<bool> updateTask = Task.Run<bool>(async () => await this.pingService.TryApiPingRequest());
            return updateTask.Result;
        }

        /// Ping the base url to check if the app is running.
        private bool TryPingBaseUrl()
        {
            Task<bool> updateTask = Task.Run<bool>(async () => await this.pingService.TryPingBaseUrl());
            return updateTask.Result;
        }
    }
}