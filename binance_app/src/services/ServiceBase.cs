namespace BinanceApp
{
    public abstract class ServiceBase : IAppService
    {
        private IApp appReference = null;

        public IApp App => this.appReference;

        public ServiceBase(IApp app)
        {
            this.appReference = app;
        }

        public void StartService()
        {
            throw new NotImplementedException();
        }
    }
}