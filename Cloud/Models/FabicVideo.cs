namespace BLS.Cloud.Models
{
    public class FabicVideo : BaseModel
    {
        public string Name
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public string URL
        {
            get; set;
        }
        public string ImageFilePath
        {
            get; set;
        }
        public byte[] ImageData
        {
            get; private set;
        }
        public bool AboutFabic
        {
            get; set;
        }

        public FabicVideo()
        {

        }

        public void LoadImageData()
        {
            if (System.IO.File.Exists(ImageFilePath))
                ImageData = System.IO.File.ReadAllBytes(ImageFilePath);
        }
    }
}