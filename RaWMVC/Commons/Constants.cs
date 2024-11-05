namespace RaWMVC.Commons
{
    public static class Constants
    {
        public const int MAXLENGTH_EntitiesName = 75;
        public const int MAXLENGTH_EntitiesDescription = 200;

        public readonly static string[] Invalid_Extenstion = 
            { "bat", "com", "exe", "msi", "scr", "hta", "cpl", "cpp", "msc", "jar", "cmd" };
        public readonly static string[] Valid_Extenstion = 
            { "jpg", "jpeg", "png", "bmp", "tiff", "tif", "webp", "svg", "heic", "heif" };
        public const string Cover_Img_Path = "~/music/";

        public const int TAKE = 5;
    }
}
