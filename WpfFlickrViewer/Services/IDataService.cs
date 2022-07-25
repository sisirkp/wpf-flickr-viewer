namespace WpfFlickrViewer.Services
{
    public interface IDataService
    {
        string Get(string requestedUrl);

        byte[] DownloadImage(string fromUrl);
    }
}
