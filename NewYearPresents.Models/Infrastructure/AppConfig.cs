namespace NewYearPresents.Models.Infrastructure
{
    public class AppConfig
    {
        public Company Company { get; set; } = new Company();
        public Database Database { get; set; } = new Database();
        public Folders Folders { get; set; } = new Folders();
    }

    public class Database
    {
        public string? ConnectionString { get; set; }
    }

    public class Company
    {
        public string? Name { get; set; }
    }

    public class Folders
    {
        public string Source { get => $@"C:\Users\{Environment.UserName}\Downloads\"; }
 
        private string? productsImages;
        private string? packagingsImages;

        /// <summary>
        /// Название конечной папки из общей директории, в которой хранятся изображения продуктов
        /// </summary>
        public string ProductsImages
        {
            get => Source + productsImages;
            set => productsImages = value;
        }

        /// <summary>
        /// Название конечной папки из общей директории, в которой хранятся изображения упаковок
        /// </summary>
        public string PackagingsImages
        {
            get => Source + packagingsImages;
            set => packagingsImages = value;
        }
    }
}