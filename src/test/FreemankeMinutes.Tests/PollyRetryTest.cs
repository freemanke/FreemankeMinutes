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
    public class PollyRetryTest
    {
        private static ITestOutputHelper _output;
        private HttpClient _httpClient;

        public PollyRetryTest(ITestOutputHelper output)
        {
            _output = output;
            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri("http://a.b.c/");
        }

        [Fact]
        public async Task RetryOnce()
        {
            _output.WriteLine("start to test RetryOnce");
            var output = "";
            var policy = Policy.Handle<HttpRequestException>().RetryAsync((e, r, c) => { output += r; });
            await Assert.ThrowsAsync<HttpRequestException>(async () => await policy.ExecuteAsync(async () => { await _httpClient.GetAsync(""); }));
            Assert.Equal("1", output);
        }

        [Fact]
        public async Task RetryThreeTimes()
        {
            var output = "";
            var policy = Policy.Handle<HttpRequestException>().RetryAsync(3, (e, r, c) => { output += r; });
            await Assert.ThrowsAsync<HttpRequestException>(async () => await policy.ExecuteAsync(async () => { await _httpClient.GetAsync(""); }));
            Assert.Equal("123", output);
        }

        [Fact]
        public async Task WaitAndRetry()
        {
            var output = "";
            var policy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3),
                }, (e, r, c) => { output += r; });

            await Assert.ThrowsAsync<HttpRequestException>(async () => await policy.ExecuteAsync(async () => { await _httpClient.GetAsync(""); }));
            Assert.Equal("00:00:0100:00:0200:00:03", output);
        }

        [Fact]
        public async Task WaitAndRetryWithFunction()
        {
            var output = "";
            var policy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, r => TimeSpan.FromSeconds(Math.Pow(1, r)), (e, r, c) => { output += r; });

            await Assert.ThrowsAsync<HttpRequestException>(async () => await policy.ExecuteAsync(async () => { await _httpClient.GetAsync(""); }));
            Assert.Equal("00:00:0100:00:0100:00:01", output);
        }

        [Fact(Skip = "This test will run forever")]
        public async Task WaitAndRetryForever()
        {

            var policy = Policy.Handle<HttpRequestException>()
                .WaitAndRetryForeverAsync(r => TimeSpan.FromSeconds(Math.Pow(2, r)));
            await policy.ExecuteAsync(async () => { await _httpClient.GetAsync(""); });
        }
    }
}
