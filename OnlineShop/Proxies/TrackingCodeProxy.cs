
using Microsoft.Extensions.Options;

namespace OnlineShop.Proxies;

public class TrackingCodeProxy(IOptions<Settings> settings) : ITrackingCodeProxy
{
    private readonly TrackingCodeSettings _settings = settings.Value.TrackingCode;

    public async Task<string> Get(CancellationToken cancellationToken)
    {
        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(_settings.BaseURL)
        };

        var url = string.Format(_settings.GetUrl, _settings.Prefix);

        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        var trackingCode = await response.Content.ReadAsStringAsync(cancellationToken);
        return trackingCode;
    }
}

