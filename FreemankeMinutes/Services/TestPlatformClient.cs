using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FreemankeMinutes.Services
{
    public class TestPlatformClient
    {
        private ILogger<TestPlatformClient> _logger;
        private HttpClient _httpClient;

        public TestPlatformClient(ILogger<TestPlatformClient> logger, HttpClient httpClient, IConfiguration config)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(config["TestPlatformClientBaseUrl"]);
        }

        public Task<string> GetSikuliMasterSlaveVersionAsync()
        {
            var apiUrl = new Uri("api/sikulimasterslave/version", UriKind.Relative);
            _logger.LogInformation($"HttpClient: loading {apiUrl}");
            return _httpClient.GetStringAsync(apiUrl);
        }
    }
}
