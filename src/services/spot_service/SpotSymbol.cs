namespace BinanceApp.SpotService
{
    [Serializable]
    public struct SpotSymbol
    {
        public string asset { get; set; }
        public string free { get; set; }
        public string locked { get; set; }

        public override string ToString()
        {
            return $"asset:{this.asset}, free:{this.free}, locked: {this.locked}";
        }
    }
}