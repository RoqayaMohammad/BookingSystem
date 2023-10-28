namespace MVC.Services
{
    public class ServiceRepository
    {
        private IConfiguration _configuration;
        public HttpClient Client { get; set; }
        public ServiceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            Client = new HttpClient();
            string serviceUrl = _configuration["ServiceUrl"];
            Client.BaseAddress = new Uri(serviceUrl);
            //Client = new HttpClient();
            //Client.BaseAddress = new Uri(ConfigurationManager.GetSection("ServiceUrl").ToString());
        }
        public async Task<HttpResponseMessage> GetResponse(string url)
        {
            return await Client.GetAsync(url);
        }
        public async Task <HttpResponseMessage> PutResponse(string url, object model)
        {
            return await Client.PutAsJsonAsync(url, model);
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }
    }
}
