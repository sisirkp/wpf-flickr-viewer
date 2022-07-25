using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using WpfFlickrViewer.Controllers;

namespace WpfFlickrViewer.ViewModels
{
    class ImageViewerViewModel : ViewModelBase
    {
        private readonly IImageController imageController;
        private readonly IErrorController errorController;
        private ObservableCollection<ImageSource> searchedImages;
        private string searchKeyword = "nature";

        public ObservableCollection<ImageSource> SearchedImages
        {
            get { return searchedImages; }
            private set { SetProperty(ref searchedImages, value); }
        }

        public ICommand SearchCommand { get; private set; }

        public ImageViewerViewModel(IImageController imageController, IErrorController errorController)
        {
            this.imageController = imageController ?? throw new ArgumentNullException(nameof(imageController) + " is null");
            this.errorController = errorController ?? throw new ArgumentNullException(nameof(errorController) + " is null");

            SearchCommand = new RelayCommand(o => SearchButtonHandler(this));
        }

        private void SearchButtonHandler(object sender)
        {
            var images = imageController.GetImages(searchKeyword);
            SearchedImages = new ObservableCollection<ImageSource>(images.Select(i => i.Image));
        }
    }
}
