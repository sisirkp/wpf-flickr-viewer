using WpfFlickrViewer.Controllers;
using WpfFlickrViewer.Services;
using WpfFlickrViewer.ViewModels;

namespace WpfFlickrViewer
{
    class Composer
    {
        public IDataService dataService;
        public IImageController imageController;
        public IErrorController errorController;
        public ImageViewerViewModel imageViewerViewModel;

        public void Compose()
        {
            dataService = new HttpService();
            imageController = new FlickrImageController(dataService);
            errorController = new ErrorController();

            imageViewerViewModel = new ImageViewerViewModel(imageController, errorController);
        }
    }
}
