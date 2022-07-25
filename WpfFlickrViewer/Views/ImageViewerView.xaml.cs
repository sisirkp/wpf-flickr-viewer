using System.Windows.Controls;

namespace WpfFlickrViewer.Views
{
    /// <summary>
    /// Interaction logic for ImageViewerView.xaml
    /// </summary>
    public partial class ImageViewerView : UserControl
    {
        public ImageViewerView()
        {
            InitializeComponent();
            var composer = new Composer();
            composer.Compose();

            this.DataContext = composer.imageViewerViewModel;
        }
    }
}
