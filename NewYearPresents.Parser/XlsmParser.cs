using NewYearPresents.Models.DTOs;
using NewYearPresents.Models.Entities;
using NewYearPresents.Models.Extentions;
using OfficeOpenXml;
using System.Text;
using System.Xml.Linq;

namespace NewYearPresents.Parser
{
    public class XlsmParser
    {
        private readonly string _sourceDirectory;

        public XlsmParser()
        {
            _sourceDirectory = $@"C:\Users\{Environment.UserName}\Downloads\";
        }

        /// <summary>
        /// Парсит .xlsm файл
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<ParsedDTO> ParseProductsAsync(string filename)
        {
            ExcelPackage.License.SetNonCommercialPersonal("Bogdan");
            using var package = new ExcelPackage();

            await package.LoadAsync(_sourceDirectory + filename);

            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            // Чтение данных из ячеек
            int rowCount = worksheet.Dimension.Rows;
            int columnCount = worksheet.Dimension.Columns - 1;

            var productsTmp = new List<object>();

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
                        productsTmp.Add(0);
                    if (j == 7 && worksheet.Cells[i, j].Picture.Exists)
                        productsTmp.Add(worksheet.Cells[i, j].Picture.Get().GetImageBytes());
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
                            productsTmp.Add(value);
                            break;
                        }

                        productsTmp.Add(value);
                    }
                }
                if (productsTmp.Count == 6)
                {
                    var name = productsTmp[0].ToString().NormalizeText();

                    var product = new Product()
                    {
                        Name = name,
                        ExpirationDate = Convert.ToInt32(productsTmp[5]),
                        Manufacturer = manufacturers.Last(),
                        ProductType = currentProductType
                    };

                    products.Add(product);

                    var productBox = new ProductsBox()
                    {
                        Product = product,
                        Price30K = Convert.ToSingle(productsTmp[1]),
                        Price60K = Convert.ToSingle(productsTmp[2]),
                        Price100K = Convert.ToSingle(productsTmp[3]),
                        Price150K = Convert.ToSingle(productsTmp[4]) != 0 ? Convert.ToSingle(productsTmp[4]) : Convert.ToSingle(productsTmp[3]),
                        //Image = productsTmp[5].ToString()
                        TotalWeight = GetTotalWeightFromString(name)
                    };

                    productsBoxes.Add(productBox);
                }
                if (productsTmp.Count > 0)
                    productsTmp.Clear();
            }

            return new ParsedDTO() { ProductsBoxes = productsBoxes, ProductTypes = productTypes, Manufacturers = manufacturers, Products = products };

        }

        protected float GetTotalWeightFromString(string source)
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

                    if (!(source.Contains("кг") || source.Contains("гр") || source.Contains("г р") || source.Contains("г ") || source.Contains("г/") || source.Contains("гр/")))
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

                    if (!(source.Contains("кг") || source.Contains("гр") || source.Contains("г р") || source.Contains("г ") || source.Contains("г/") || source.Contains("гр/")))
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
            else if (source.Contains("гр.") || source.Contains("гр ") || source.Contains("гр/"))
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
    }
}