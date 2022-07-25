using System.Collections.Generic;
using WpfFlickrViewer.Models;

namespace WpfFlickrViewer.Controllers
{
    public interface IImageController
    {
        IEnumerable<ImageInfo> GetImages(string keyword);
    }
}