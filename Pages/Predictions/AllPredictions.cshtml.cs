using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Team21V4._5.Data;
using Team21V4._5.Model;
using Team21V4_5;
using static Team21V4_5.MLModel;

namespace Team21V4._5.Pages.Predictions
{
    public class AllPredictionsModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public AllPredictionsModel(ApplicationDbContext context)
        {
            dbContext = context;
        }
        [NotNull]
        public List<ModelOutput> Recommendations = new List<ModelOutput>();
        public List<Prediction> predictions = new List<Prediction>();

        public void OnGet(int id)
        {
            var productList = dbContext.Products.ToList();
            var allproductIDs = dbContext.Products.Select(theId => new { id = theId.ProductID, name = theId.ProductName, temp = theId.Temperature ,date = theId.DateAdded }).ToList();


            var existingPredictions = dbContext.Predictions.ToList();
            dbContext.Predictions.RemoveRange(existingPredictions);
            dbContext.SaveChanges();

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
                //dbContext.Recommendations.Add(productOutput);
                //Recommendations.Add(productOutput);
                dbContext.SaveChanges();
            });



            //// Assuming you have 10 values in total (5 scores and 5 product IDs)
            //var top5Predictions = Recommendations.OrderByDescending(p => p.Score).Take(5).ToList();

            //// Use InvariantCulture to format scores as strings with decimal points
            //var top5Scores = string.Join(", ", top5Predictions.Select(p => p.Score.ToString(CultureInfo.InvariantCulture)));
            //var top5ProductIDs = string.Join(", ", top5Predictions.Select(p => $"'{p.ProductID}'"));

            //// Store the top 5 scores and product names in ViewData
            //ViewData["Top5Scores"] = top5Scores;
            //ViewData["Top5ProductIDs"] = top5ProductIDs;
            //// Storing data in TempData
            //TempData["Top5Scores"] = top5Scores;
            //TempData["Top5ProductIDs"] = top5ProductIDs;


            var top5Predictions = predictions.OrderByDescending(p => p.Score).Take(5).ToList();

            var top5Scores = string.Join(", ", top5Predictions.Select(p => p.Score.ToString(CultureInfo.InvariantCulture)));
            var top5ProductName = string.Join(", ", top5Predictions.Select(p => $"'{p.ProductName}'"));

            ViewData["Top5Scores"] = top5Scores;
            ViewData["Top5ProductIDs"] = top5ProductName;

            TempData["Top5Scores"] = top5Scores;
            TempData["Top5ProductIDs"] = top5ProductName;

        }
    }
}
