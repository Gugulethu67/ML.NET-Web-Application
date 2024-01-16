using Team21V4._5.Model;

namespace Team21V4._5.Data
{
    public class DBInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            /*context.Products.AddRange(
                new Product
                {
                    ProductID = 75,
                    ProductName = "Roast Chk Sw",
                    DateAdded = DateTime.Parse("2022/06/01"),
                    StockOnHand = 58,
                    DeliveredQuantity = 36,
                    SalesQuantity = 36,
                    WasteQuantity = 6,
                    BoughtStock = 94,
                    Temperature = 13.4f,
                    Precipitation = 0.002f,
                },

                new Product
                {
                    ProductID = 76,
                    ProductName = "FR Egg Mayo Sw",
                    DateAdded = DateTime.Parse("2022/06/01"),
                    StockOnHand = 67,
                    DeliveredQuantity = 40,
                    SalesQuantity = 42,
                    WasteQuantity = 7,
                    BoughtStock = 109,
                    Temperature = 13.4f,
                    Precipitation = 0.002f,
                },

                new Product
                {
                    ProductID = 99,
                    ProductName = "12 Frozen Scones",
                    DateAdded = DateTime.Parse("2022/06/01"),
                    StockOnHand = 6,
                    DeliveredQuantity = 0,
                    SalesQuantity = 0,
                    WasteQuantity = 0,
                    BoughtStock = 6,
                    Temperature = 13.4f,
                    Precipitation = 0.002f,
                },

                new Product
                {
                    ProductID = 117,
                    ProductName = "Beef Steak Pie MB",
                    DateAdded = DateTime.Parse("2022/06/01"),
                    StockOnHand = 5,
                    DeliveredQuantity = 4,
                    SalesQuantity = 1,
                    WasteQuantity = 0,
                    BoughtStock = 6,
                    Temperature = 13.4f,
                    Precipitation = 0.2f,
                }

            );
            context.SaveChanges();*/

            //try
            //{
                string[] lines = System.IO.File.ReadAllLines("DataNoNAInitialise.csv");
                for (var i = 1; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(';');


                    var products = new Product[]
                    {
                                  new Product
                                  {
                                        ProductID = int.Parse(columns[0]),
                                        ProductName = columns[1],
                                        DateAdded = DateTime.Parse(columns[2]),
                                        StockOnHand = int.Parse(columns[3]),
                                        //DeliveredQuantity = int.Parse(columns[4]),
                                        Department = columns[4],
                                        //WasteQuantity = int.Parse(columns[6]),
                                        //BoughtStock = int.Parse(columns[7]),
                                        Temperature = float.Parse(columns[6].Substring(0,2)),
                                        
                                        //Precipitation = float.Parse(columns[9].Substring(0,1))

                                  }

                    };
                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            //}
            //catch (Exception ex)
            //{
            //    throw new ArgumentException(ex.Message, nameof(context));
            //}

        }
    }
}
