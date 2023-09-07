using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace NZWalks.UI.Controllers
{
    public class WalksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WalksController(IHttpClientFactory httpClientFactory ) {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<WalksDTO> walks = new List<WalksDTO>();

            var client = _httpClientFactory.CreateClient();

            var httpResponseMessage = await client.GetAsync("https://localhost:7079/api/walks");

            walks.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<WalksDTO>>());

            return View(walks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWalksViewModel walksModel)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7079/api/walks"),
                Content = new StringContent(JsonSerializer.Serialize(walksModel), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var resposne = await httpResponseMessage.Content.ReadFromJsonAsync<WalksDTO>();

            if (resposne is not null)
            {
                return RedirectToAction("Index", "Walks");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<WalksDTO>($"https://localhost:7079/api/Walks/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WalksDTO request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7079/api/walks/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<WalksDTO>();

            if (respose is not null)
            {
                return RedirectToAction("Index", "Walks");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(WalksDTO request)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7079/api/walks/{request.Id}");

                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Walks");
            }
            catch (Exception ex)
            {
            }

            return View("Edit");
        }
    }
}
