using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.SocialMediaDto;
using System.Net.Http;
using System.Text;

namespace SignalRWebUI.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SocialMediaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region Index Method

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7097/api/SocialMedia");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        #endregion

        #region CreateSocialMedia Methods

        [HttpGet]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createSocialMediaDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7097/api/SocialMedia", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        #endregion

        #region DeleteSocialMedia Method

        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7097/api/SocialMedia/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion

        #region UpdateSocialMedia Methods

        [HttpGet]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7097/api/SocialMedia/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateSocialMediaDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateSocialMediaDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync($"https://localhost:7097/api/SocialMedia/", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion
    }
}
