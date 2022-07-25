using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfFlickrViewer.Config;
using WpfFlickrViewer.Models;
using WpfFlickrViewer.Services;

namespace WpfFlickrViewer.Controllers
{
    class FlickrImageController : IImageController
    {
        readonly IDataService dataService;

        public FlickrImageController(IDataService dataService)
        {
            this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService) + " is null");
        }

        public IEnumerable<ImageInfo> GetImages(string keyword)
        {
            string url = Configuration.flickrBaseUrl + "?tags=" + keyword;
            var response = dataService.Get(url);

            IList<ImageInfo> images = ImageInfo.FromXML(response);
            Parallel.ForEach(images, (currentImg) =>
            {
                var imageBytes = dataService.DownloadImage(currentImg.ImageUrl);
                currentImg.UpdateImageData(imageBytes);
            });

            return images;
        }
    }
}
