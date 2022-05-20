public interface IApp
{
    bool IsRunning { get; }
    string BaseUrl { get; }

    HttpClient HttpClient { get; }

    void StartApp();
    void UpdateApp();
}