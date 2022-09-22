namespace BinanceApp.PingService
{
    [Serializable]
    public struct BinancePing
    {
        public readonly static BinancePing InvalidPing = new BinancePing() { isInvalid = true };

        public bool isInvalid;
    }
}