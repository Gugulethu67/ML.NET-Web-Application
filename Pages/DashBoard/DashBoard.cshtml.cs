using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Team21V4._5.Data;
using Team21V4._5.Model;
using Team21V4_5;
using static Team21V4_5.MLModel;


namespace Team21V4._5.Pages.DashBoard
{
    
    public class DashBoardModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public DashBoardModel(ApplicationDbContext context)
        {
            dbContext = context;
        }
        [NotNull]
        public List<ModelOutput> Recommendations = new List<ModelOutput>();
        public List<Prediction> predictions = new List<Prediction>();


        public void OnGet(string selectedTimeframe)
        {
            //WEATHER IMPACT ANALYSIS
            //double currentTemp = 23;
            //double impactScore = ((currentTemp - 17.76) * 8.069) / 100;
            //string impact = impactScore.ToString("F2");
            //ViewData["impact"] = impact;

            //TIMEFRAME
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Date;

            string inputString = "18,24,20,18,16,19,21,25,26,26,28,25,24,24";
            string[] valuesArray = inputString.Split(',');
            double average = 0.0;

            if (selectedTimeframe == "ThisWeek")
            {
                startDate = DateTime.Now.Date;
                endDate = startDate.AddDays(6);
                string[] first7Values = valuesArray.Take(7).ToArray();
                double sum = first7Values.Select(s => double.Parse(s, CultureInfo.InvariantCulture)).Sum();
                average = sum / 7;
            }
            else if (selectedTimeframe == "NextWeek")
            {
                startDate = DateTime.Now.Date.AddDays(5);
                endDate = startDate.AddDays(6);
                string[] last7Values = valuesArray.Skip(Math.Max(0, valuesArray.Length - 7)).ToArray();
                double sum = last7Values.Select(s => double.Parse(s, CultureInfo.InvariantCulture)).Sum();
                average = sum / 7;
            }
            else
            {
                startDate = DateTime.Now.Date;
                endDate = startDate.AddDays(30);
                string[] first30Values = valuesArray.Take(30).ToArray();
                double sum = first30Values.Select(s => double.Parse(s, CultureInfo.InvariantCulture)).Sum();
                average = sum / 13;
            }

            List<string> daysOfWeek = new List<string>();
            for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                daysOfWeek.Add(currentDate.ToString("dddd"));
            }

            string daysOfWeekString = string.Join(", ", daysOfWeek.Select(day => $"'{day}'"));

            //WEATHER IMPACT ANALYSIS

            double impactScore = ((average - 17.76) * 8.069);
            string impact = impactScore.ToString("F2", CultureInfo.InvariantCulture); // Format to 2 decimal places
            ViewData["impact"] = impact;
            ViewData["avg"] = average.ToString("F2", CultureInfo.InvariantCulture); // Format to 2 decimal places

            //CHART PREDICTION
            var productList = dbContext.Products.ToList();
            var allproductIDs = dbContext.Products.Select(theId => new { id = theId.ProductID, name = theId.ProductName, temp = theId.Temperature, date = theId.DateAdded }).ToList();

            var existingPredictions = dbContext.Predictions.ToList();
            dbContext.Predictions.RemoveRange(existingPredictions);
            dbContext.SaveChanges();

            // WEEKLY CHART
            var dailySalesList = new List<double>();


            for (DateTime currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                int dailyTotalSales = 0;

                allproductIDs.ForEach(product =>
                {
                    ModelInput theData = new();
                    theData.ProductID = product.id;
                    theData.ProductName = product.name;
                    theData.Temperature = product.temp;
                    theData.Date = currentDate.ToString("yyyy-MM-dd"); // Set the date to the current date
                    var prediction = MLModel.Predict(theData);

                    dailyTotalSales += (int)prediction.Score;
                });

                // Add the daily total sales to the list
                dailySalesList.Add(dailyTotalSales);
            }

            var weeklySalesData = string.Join(", ", dailySalesList.Select(dailyTotalSales => dailyTotalSales.ToString()));

            ViewData["WeeklySalesData"] = weeklySalesData;
            ViewData["DaysOfWeekString"] = daysOfWeekString;

            // MONTHLY CHART
            var monthlyTotal = 0.0;
            DateTime monthEnd = startDate.AddDays(29);

            for (DateTime currentDate = startDate; currentDate <= monthEnd; currentDate = currentDate.AddDays(1))
            {
                int dailyTotalSales = 0;

                allproductIDs.ForEach(product =>
                {
                    ModelInput theData = new();
                    theData.ProductID = product.id;
                    theData.ProductName = product.name;
                    theData.Temperature = product.temp;
                    theData.Date = currentDate.ToString("yyyy-MM-dd"); // Set the date to the current date
                    var prediction = MLModel.Predict(theData);

                    dailyTotalSales += (int)prediction.Score;
                });

                monthlyTotal += dailyTotalSales;
            }

            ViewData["monthlyData"] = monthlyTotal.ToString();
            ViewData["TodayTemp"] = 15.7;


            //for (DateTime currentDate = startDate; currentDate <= currentDate.AddDays(6); currentDate = currentDate.AddDays(1))
            //{
            //    allproductIDs.ForEach(product =>
            //    {
            //        ModelInput theData = new();
            //        var productOutput = new ModelOutput();
            //        Prediction prediction1 = new Prediction();
            //        theData.ProductID = product.id;
            //        theData.ProductName = product.name;
            //        theData.Temperature = product.temp;
            //        theData.Date = currentDate.ToString("yyyy-MM-dd");
            //        var prediction = MLModel.Predict(theData);

            //        prediction1.ProductID = product.id;
            //        prediction1.ProductName = product.name;
            //        prediction1.Score = prediction.Score;


            //        productOutput.Score = prediction.Score;
            //        productOutput.ProductID = product.id;



            //        dbContext.Predictions.Add(prediction1);
            //        predictions.Add(prediction1);
            //        dbContext.SaveChanges();
            //    });
            //}
            allproductIDs.ForEach(product => {
                ModelInput theData = new();
                var productOutput = new ModelOutput();
                Prediction prediction1 = new Prediction();
                theData.ProductID = product.id;
                theData.ProductName = product.name;
                theData.Temperature = product.temp;
                theData.Date = (product.date).ToString();
                var prediction = MLModel.Predict(theData);

                prediction1.ProductID = product.id;
                prediction1.ProductName = product.name;
                prediction1.Score = prediction.Score;


                productOutput.Score = prediction.Score;
                productOutput.ProductID = product.id;



                dbContext.Predictions.Add(prediction1);
                predictions.Add(prediction1);
                dbContext.SaveChanges();
            });

            var top5Predictions = predictions.OrderByDescending(p => p.Score).Take(5).ToList();

            var top5Scores = string.Join(", ", top5Predictions.Select(p => p.Score.ToString(CultureInfo.InvariantCulture)));
            var top5ProductName = string.Join(", ", top5Predictions.Select(p => $"'{p.ProductName}'"));

            TempData["Top5Scores"] = top5Scores;
            TempData["Top5ProductIDs"] = top5ProductName;

            var top5PredictionswithlessstockOnHand = productList.OrderBy(p => p.StockOnHand).Take(2).ToList();
            var top5ProductNames = top5PredictionswithlessstockOnHand.Select(p => p.ProductName).ToList();
            var stockOut = top5PredictionswithlessstockOnHand.Select(p => p.StockOnHand).ToList();

            var anotherPredictions = predictions.OrderBy(p => p.Score).Take(2).ToList();
            var scores = string.Join(", ", anotherPredictions.Select(p => p.Score.ToString(CultureInfo.InvariantCulture)));
            TempData["1stScores"] = (int)scores[0];
            TempData["2ndScores"] = (int)scores[1];

            TempData["Top5ProductNames"] = top5ProductNames;
            TempData["1stStockOut"] = stockOut[0];
            TempData["2ndStockOut"] = stockOut[1];


        }

    }
}


