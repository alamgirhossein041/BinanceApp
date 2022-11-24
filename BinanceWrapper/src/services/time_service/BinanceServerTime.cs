namespace BinanceWrapper.TimeService
{
    public struct BinanceServerTime
    {
        public long serverTime { get; set; }

        public override string ToString()
        {
            return this.serverTime.ToString();
        }
    }
}