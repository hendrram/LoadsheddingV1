using LoadsheddingV1.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LoadsheddingV1.Controllers
{
    public class LoadSheddingUpdateService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private const int UpdateIntervalInSeconds = 60; // Update interval in seconds

        public LoadSheddingUpdateService(
            IHttpClientFactory httpClientFactory,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateInformation();
                await Task.Delay(TimeSpan.FromSeconds(UpdateIntervalInSeconds), stoppingToken);
            }
        }

        private async Task UpdateInformation()
        {
            var apiKey = _configuration["LoadSheddingApi:Token"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return;
            }

            var areaId = _configuration["LoadSheddingApi:AreaId"] ?? "capetown-15-rondebosch";
            var baseUrl = _configuration["LoadSheddingApi:BaseUrl"] ?? "https://developer.sepush.co.za/business/2.0/area";

            var client = _httpClientFactory.CreateClient();

            // Set the API key in the headers
            client.DefaultRequestHeaders.Remove("Token");
            client.DefaultRequestHeaders.Add("Token", apiKey);

            // Construct the URL for the area information endpoint
            var apiUrl = $"{baseUrl}?id={Uri.EscapeDataString(areaId)}&test=current";

            // Make a GET request to the API
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a JSON object
                AreaInformation? areaInformation = await response.Content.ReadFromJsonAsync<AreaInformation>();
                var areaInfo = areaInformation;

                // Check if there are any events
                if (areaInfo?.Events != null && areaInfo.Events.Length > 0)
                {
                    // Get the first event
                    var firstEvent = areaInfo.Events.FirstOrDefault();

                    // Check if the first event is not null
                    if (firstEvent != null)
                    {
                        // Use a scoped service to get the DbContext
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var context = scope.ServiceProvider.GetRequiredService<LoadSheddingContext>();

                            // Construct parameters for the query
                            var existingEvent = context.LoadSheddingEvents.FirstOrDefault(e => e.AreaId == areaId);

                            if (existingEvent != null)
                            {
                                // Update the existing record with new start and end times
                                existingEvent.StartTime = DateTime.Parse(firstEvent.Start ?? DateTime.MinValue.ToString());
                                existingEvent.EndTime = DateTime.Parse(firstEvent.End ?? DateTime.MinValue.ToString());
                                existingEvent.LastUpdated = DateTime.Now;
                            }
                            else
                            {
                                // If no existing record, add a new one
                                context.LoadSheddingEvents.Add(new LoadsheddingEvent
                                {
                                    AreaId = areaId,
                                    StartTime = DateTime.Parse(firstEvent.Start ?? DateTime.MinValue.ToString()),
                                    EndTime = DateTime.Parse(firstEvent.End ?? DateTime.MinValue.ToString()),
                                    LastUpdated = DateTime.Now
                                });
                            }

                            // Save changes to the database
                            await context.SaveChangesAsync();
                        }
                    }

                }
            }
            else
            {
                // Log or handle error when response is not successful
            }
        }


    }
}








public class AreaInformation
    {
        public string? Name { get; set; }
        public string? Region { get; set; }
        public Event[]? Events { get; set; }
    }

    public class Event
    {
        public string? Start { get; set; }
        public string? End { get; set; }
        public string? Note { get; set; }
    }

