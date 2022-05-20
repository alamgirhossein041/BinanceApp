namespace BinanceApp
{
    public struct BinanceExchangeAsset
    {
        public readonly string AssetName { get; }
        public readonly string TimeFrame { get; }
        public readonly double Price { get; }
    }
}