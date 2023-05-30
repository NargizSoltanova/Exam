namespace WebAppExam.Utilities
{
    public static class FileExtensions
    {
        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type + "/");
        }
        public static bool CheckFileSize(this IFormFile file, int size)
        {
            return file.Length/1024 > size;
        }
        public static async Task<string> SaveFileAsync(this IFormFile file, string root, string folder)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string path = Path.Combine(root, "uploads" ,folder, uniqueFileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return uniqueFileName;
        }
        public static void DeleteFile(this IFormFile file, string root, string folder, string fileName)
        {
            string path = Path.Combine(root, "uploads", folder, fileName);
            if(File.Exists(path))
            {
                File.Delete(path);
            }

        }
    }
}
