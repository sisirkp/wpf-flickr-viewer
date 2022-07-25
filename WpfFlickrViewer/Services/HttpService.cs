using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WpfFlickrViewer.Services
{
    class HttpService : IDataService
    {
        private readonly HttpClient client = new HttpClient();

        public string Get(string requestUri)
        {
            Task<string> result = GetResponseAsync(requestUri);
            result.Wait();

            return result.Result;
        }

        public byte[] DownloadImage(string fromUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Proxy = null;
                using (Stream stream = webClient.OpenRead(fromUrl))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        return ms.ToArray();
                    }
                }
            }
        }

        private async Task<string> GetResponseAsync(string requestUri)
        {
            string jsonstring = await client.GetStringAsync(requestUri).ConfigureAwait(false);
            return jsonstring;
        }
    }
}
