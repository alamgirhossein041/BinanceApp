namespace BinanceApp.SpotService
{
    public struct SpotSymbol
    {
        public string asset;
        public string free;
        public string locked;

        public override string ToString()
        {
            return $"asset:{this.asset}, free:{this.free}, locked: {this.locked}";
        }
    }
}