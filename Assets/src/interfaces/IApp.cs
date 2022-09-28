public interface IApp
{
    bool IsRunning { get; }
    string BaseUrl { get; }
    string ApiKey { get; }
    string SecretKey { get; }

    HttpClient HttpClient { get; }

    void StartApp();
    void UpdateApp();
}