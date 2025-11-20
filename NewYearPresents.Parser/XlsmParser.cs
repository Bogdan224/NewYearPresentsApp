using NewYearPresents.Models.DTOs;
using NewYearPresents.Models.Entities;
using OfficeOpenXml;
using OfficeOpenXml.CellPictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Parser
{
    public class XlsmParser
    {
        private readonly string _userName;

        public XlsmParser()
        {
            _userName = $@"C:\Users\{Environment.UserName}\Downloads\";
        }

        /// <summary>
        /// Парсит .xlsm файл 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public ParsedDataDTO InitialParse(string filename)
        {
            using (ExcelPackage package = new ExcelPackage(_userName + filename))
            {
                ExcelPackage.License.SetNonCommercialPersonal("Bogdan");
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Чтение данных из ячеек
                int rowCount = worksheet.Dimension.Rows;
                int columnCount = worksheet.Dimension.Columns - 1;

                var productsTmp = new List<object>();

                List<Product> products = new List<Product>();
                List<Manufacturer> manufacturers = new List<Manufacturer>();
                List<ProductType> productTypes = new List<ProductType>();
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
                                    manufacturers.Add(new Manufacturer() { Name = ToUpperFirstLetter(value.ToString()) });
                                    if (worksheet.Cells[i + 1, 8].Value != null)
                                        currentProductType = productTypes[0];
                                }
                                else
                                {
                                    if (worksheet.Cells[i + 1, 8].Value != null)
                                    {
                                        var productType = new ProductType() { Name = ToUpperFirstLetter(value.ToString()) };
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
                        var product = new Product()
                        {
                            Name = productsTmp[0].ToString(),
                            Price30K = Convert.ToSingle(productsTmp[1]),
                            Price60K = Convert.ToSingle(productsTmp[2]),
                            Price100K = Convert.ToSingle(productsTmp[3]),
                            Price150K = Convert.ToSingle(productsTmp[4]) != 0 ? Convert.ToSingle(productsTmp[4]) : Convert.ToSingle(productsTmp[3]),
                            //Image = productsTmp[5].ToString(),
                            ExpirationDate = Convert.ToInt32(productsTmp[5]),
                            Manufacturer = manufacturers.Last(),
                            ProductType = currentProductType
                        };
                        products.Add(product);

                    }
                    if (productsTmp.Count > 0)
                        productsTmp.Clear();
                }

                return new ParsedDataDTO() { Products = products, ProductTypes = productTypes, Manufacturers = manufacturers };
            }
        }

        private string ToUpperFirstLetter(string? source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            source = source.ToLower();
            while (source[0] == ' ' || source[0] == '*')
            {
                source = source.Substring(1);
            }
            while (source[source.Length - 1] == ' ')
            {
                source = source.Substring(0, source.Length - 1);
            }
            char[] letters = source.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            return new string(letters);
        }
    }
}
