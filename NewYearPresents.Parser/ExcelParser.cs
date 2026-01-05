using NewYearPresents.Models.DTOs;
using NewYearPresents.Models.Entities;
using NewYearPresents.Models.Extentions;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Data.Common;
using System.Drawing;
using System.Runtime;
using System.Text;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using NewYearPresents.Models.Infrastructure;

namespace NewYearPresents.Parser
{
    public class ExcelParser(Folders folders)
    {
        private Folders folders = folders;

        /// <summary>
        /// Парсит .xlsm файл
        /// </summary>
        /// <param name="filename">Название файла</param>
        /// <returns></returns>
        public async Task<ParsedProductsBoxFileDTO> ParseProductsBoxesFileAsync(string filename)
        {
            if (Directory.Exists(folders.ProductsImages)) Directory.Delete(folders.ProductsImages, true);
            Directory.CreateDirectory(folders.ProductsImages);

            ExcelPackage.License.SetNonCommercialPersonal("Bogdan");
            using var package = new ExcelPackage();

            await package.LoadAsync(folders.Source + filename);

            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            // Чтение данных из ячеек
            int rowCount = worksheet.Dimension.Rows;
            int columnCount = worksheet.Dimension.Columns - 1;

            var dataTmp = new List<object?>();

            List<ProductsBox> productsBoxes = new List<ProductsBox>();
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            List<ProductType> productTypes = new List<ProductType>();
            List<Product> products = new List<Product>();
            productTypes.Add(new ProductType() { Name = null });
            ProductType currentProductType = new ProductType();

            for (int i = 6; i <= rowCount; i++)
            {
                for (int j = 1; j <= columnCount; j++)
                {
                    var value = worksheet.Cells[i, j].Value;
                    if (j == 1 && worksheet.Cells[i, 8].Value != null)
                        continue;
                    if (j == 2 && value is null)
                        break;
                    if (j == 6 && value is null)
                        dataTmp.Add(0);
                    if (j == 7)
                    {
                        dataTmp.Add(GetImageNameFromCell(worksheet, i, j - 1, folders.ProductsImages));
                    }
                    if (value != null)
                    {
                        //Проверка на производителя
                        if (j == 1 && worksheet.Cells[i, 8].Value == null)
                        {
                            //Если строки сгруппированы, то считывает
                            if (worksheet.Row(i).OutlineLevel == 0)
                            {
                                manufacturers.Add(new Manufacturer() { Name = value.ToString().NormalizeText() });
                                if (worksheet.Cells[i + 1, 8].Value != null)
                                    currentProductType = productTypes[0];
                            }
                            else
                            {
                                if (worksheet.Cells[i + 1, 8].Value != null)
                                {
                                    var productType = new ProductType() { Name = value.ToString().NormalizeText() };
                                    if (!productTypes.Any(x => x.Name == productType.Name))
                                    {
                                        productTypes.Add(productType);
                                        currentProductType = productType;
                                    }
                                    else
                                    {
                                        currentProductType = productTypes.Find(x => x.Name == productType.Name)!;
                                    }
                                }
                            }
                            break;
                        }

                        if (j == 8)
                        {
                            string[] val = value.ToString()!.Split();
                            if (val[1] == "суток")
                                value = Convert.ToInt32(val[0]) / 30;
                            else
                                value = val[0];
                            dataTmp.Add(value);
                            break;
                        }

                        dataTmp.Add(value);
                    }
                }
                if (dataTmp.Count == 7)
                {
                    var name = dataTmp[0]?.ToString().NormalizeText();

                    var product = new Product()
                    {
                        Name = name,
                        ExpirationDate = Convert.ToInt32(dataTmp[6]),
                        Manufacturer = manufacturers.Last(),
                        ProductType = currentProductType,
                        ImageName = dataTmp[5]?.ToString()
                    };

                    products.Add(product);

                    var productBox = new ProductsBox()
                    {
                        Product = product,
                        Price30K = Convert.ToSingle(dataTmp[1]),
                        Price60K = Convert.ToSingle(dataTmp[2]),
                        Price100K = Convert.ToSingle(dataTmp[3]),
                        Price150K = Convert.ToSingle(dataTmp[4]) != 0 ? Convert.ToSingle(dataTmp[4]) : Convert.ToSingle(dataTmp[3]),
                        
                        TotalWeight = GetTotalWeightFromString(name)
                    };

                    productsBoxes.Add(productBox);
                }
                if (dataTmp.Count > 0)
                    dataTmp.Clear();
            }

            return new ParsedProductsBoxFileDTO(products, productTypes, manufacturers, productsBoxes);
        }

        public async Task<ParsedPackagingFileDTO> ParsePackagingsFileAsync(string filename)
        {
            if (!Directory.Exists(folders.PackagingsImages)) Directory.Delete(folders.PackagingsImages, true);
            Directory.CreateDirectory(folders.PackagingsImages);

            ExcelPackage.License.SetNonCommercialPersonal("Bogdan");
            using var package = new ExcelPackage();

            await package.LoadAsync(folders.Source + filename);

            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            // Чтение данных из ячеек
            int rowCount = worksheet.Dimension.Rows;
            int columnCount = 3;

            var dataTmp = new List<object?>();

            var packagings = new List<Packaging>();
            var packagingsInStorage = new List<PackagingInStorage>();

            for (int i = 2; i <= rowCount; i++)
            {
                for (int j = 1; j <= columnCount; j++)
                {
                    var value = worksheet.Cells[i, j].Value;
                    if (j == 1 && worksheet.Cells[i, j].Value == null)
                        continue;
                    if (j == 2 && value is null)
                        dataTmp.Add(GetImageNameFromCell(worksheet, i, j - 1, folders.PackagingsImages));
                    if (value != null)
                        dataTmp.Add(value);
                }
                if (dataTmp.Count == 3)
                {
                    var name = dataTmp[0]?.ToString().NormalizeText();

                    var packaging = new Packaging()
                    {
                        Name = name,
                        ImageName = dataTmp[1]?.ToString(),
                        MaxWeight = GetTotalWeightFromString(name)
                    };


                    packagings.Add(packaging);

                    var packagingInStorage = new PackagingInStorage()
                    {
                        Packaging = packaging,
                        Count = Convert.ToInt32(dataTmp[2])
                    };

                    packagingsInStorage.Add(packagingInStorage);
                }
                if (dataTmp.Count > 0)
                    dataTmp.Clear();
            }
            return new ParsedPackagingFileDTO(packagings, packagingsInStorage);
        }

        protected float GetTotalWeightFromString(string? source)
        {
            if (string.IsNullOrEmpty(source))
                return 0.0f;

            float weight = 0.0f;
            int pieces = 1;

            if (source.Contains("шт"))
            {
                if (source.Contains('(') && source.Contains(')'))
                {
                    int lastIndex = source.LastIndexOf("шт");
                    pieces = Convert.ToInt32(source.GetFloat(lastIndex));

                    if (!(source.Contains("кг") || source.Contains("гр") || source.Contains("г р") || source.Contains("г ") || source.Contains("г/") || source.Contains("гр/") || source.Contains("гр)")))
                    {
                        lastIndex = source.LastIndexOf('(');
                        try
                        {
                            weight = source.GetFloat(lastIndex);
                        }
                        catch
                        {
                            lastIndex = source.LastIndexOf('/');
                            try
                            {
                                weight = source.GetFloat(lastIndex);
                                return weight * pieces;
                            }
                            catch { }
                        }

                    }
                }
                else if (source.Contains('/'))
                {
                    int lastIndex = source.LastIndexOf("шт");
                    pieces = Convert.ToInt32(source.GetFloat(lastIndex));

                    if (!(source.Contains("кг") || source.Contains("гр") || source.Contains("г р") || source.Contains("г ") || source.Contains("г/") || source.Contains("гр/") || source.Contains("гр)")))
                    {
                        lastIndex = source.IndexOf('/');
                        try
                        {
                            weight = source.GetFloat(lastIndex);
                        }
                        catch
                        {
                            weight = 0;
                        }
                        return weight * pieces;
                    }
                }
                else
                {
                    int lastIndex = source.LastIndexOf("шт");
                    pieces = Convert.ToInt32(source.GetFloat(lastIndex));
                }
            }

            if (source.Contains("кг"))
            {
                int lastIndex = source.LastIndexOf("кг");
                weight = source.GetFloat(lastIndex);
            }
            else if (source.Contains("гр.") || source.Contains("гр ") || source.Contains("гр/") || source.Contains("гр)"))
            {
                int lastIndex = source.LastIndexOf("гр");
                weight = source.GetFloat(lastIndex) / 1000;
            }
            else if (source.Contains("г р"))
            {
                int lastIndex = source.LastIndexOf("г р");
                weight = source.GetFloat(lastIndex) / 1000;
            }
            else if (source.Contains("г "))
            {
                int lastIndex = source.LastIndexOf("г ");
                weight = source.GetFloat(lastIndex) / 1000;
            }
            else if (source.Contains("г/"))
            {
                int lastIndex = source.LastIndexOf("г/");
                weight = source.GetFloat(lastIndex) / 1000;
            }
            return weight * pieces;
        }

        protected string? GetImageNameFromCell(ExcelWorksheet worksheet, int row, int column, string path)
        {
            var picture = (ExcelPicture?)worksheet.Drawings.FirstOrDefault(x =>
            {
                if (x is ExcelPicture picture)
                    if (picture.From.Row == row && picture.From.Column == column)
                        return true;
                return false;
            });

            if (picture is null) return null;

            using var fs = new FileStream(path + picture.Name + ".png", FileMode.Create);

            fs.Write(picture.Image.ImageBytes);
            return picture.Name + ".png";
        }
    }
}