namespace CandidateManagementSystem.Helper
{
    public static class FileSave
    {


        private static IWebHostEnvironment _env;
        public static void SetEnvironment(IWebHostEnvironment env)
        {
            _env = env;
        }
        public static string SaveResume(IFormFile resume, IWebHostEnvironment env)
        {
            try
            {
                SetEnvironment(env);
                string path = _env.ContentRootPath + "\\files\\resume";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName=Guid.NewGuid().ToString()+ Path.GetExtension(resume.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    resume.CopyTo(stream);
                }

                return fileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
