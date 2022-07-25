using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml;

namespace WpfFlickrViewer.Models
{
    public class ImageInfo
    {
        public string ImageUrl { get; }

        public BitmapImage Image { get; private set; }

        ImageInfo(string imageUrl)
        {
            this.ImageUrl = imageUrl;
        }

        public void UpdateImageData(byte[] array)
        {
            this.Image = CreateBitmap(array);
        }

        public static IList<ImageInfo> FromXML(string xmlString)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            XmlNodeList elements = xmlDocument.GetElementsByTagName("entry");
            var images = new List<ImageInfo>();
            Parallel.ForEach(elements.OfType<XmlElement>(), (element) =>
            {
                var childs = element.ChildNodes;
                foreach (XmlElement child in childs)
                {
                    if (child.Name == "link" && child.HasAttributes)
                    {
                        XmlAttributeCollection xmlAttributeCollection = child.Attributes;
                        XmlNode node = xmlAttributeCollection.GetNamedItem("type");
                        if (node.Value == @"image/jpeg")
                        {
                            XmlNode url = xmlAttributeCollection.GetNamedItem("href");
                            images.Add(new ImageInfo(url.Value));
                        }
                    }
                }
            });

            return images;
        }

        private BitmapImage CreateBitmap(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.DecodePixelHeight = 300;
                image.DecodePixelWidth = 300;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
    }
}
