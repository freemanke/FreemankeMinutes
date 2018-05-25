using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Timeout;
using Xunit;
using Xunit.Abstractions;

namespace FreemankeMinutes.Tests
{
    public class PollyTimeoutTest
    {
        private static ITestOutputHelper _output;
        private HttpClient _httpClient;

        public PollyTimeoutTest(ITestOutputHelper output)
        {
            _output = output;
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri("https://www.baidu.com/");
        }

        [Fact]
        public async Task Timeout()
        {
            var policy = Policy.TimeoutAsync(TimeSpan.FromSeconds(5), TimeoutStrategy.Optimistic);
            await policy.ExecuteAsync(async () =>
            {
                var result = await _httpClient.GetStringAsync("");
                Assert.NotNull(result);
                _output.WriteLine(result);
            });
        }

        [Fact]
        public async Task TimeoutSecond()
        {
            var policy = Policy.TimeoutAsync(TimeSpan.FromMilliseconds(1), TimeoutStrategy.Optimistic);
            await policy.ExecuteAsync(async () =>
            {
                var result = await _httpClient.GetStringAsync("");
                Assert.NotNull(result);
                _output.WriteLine(result);
            });
        }
    }
}
