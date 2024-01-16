using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;
using Team21V4._5.Data;
using Team21V4._5.Model;
using Team21V4_5;
using static Team21V4_5.MLModel;

namespace Team21V4._5.Pages.Predictions
{
    public class PredictModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public PredictModel(ApplicationDbContext context)
        {
            dbContext = context;
        }
        [NotNull]
        //public List<ModelOutput> Recommendations = new List<ModelOutput>();
        public List<Prediction> predictions = new List<Prediction>();

        
        public void OnGet(int id, string ProductName, float todayTemperature)
        {
            //var productList = dbContext.Products.ToList();
            //var allproductIDs = dbContext.Products.Select(theId => new { id = theId.ProductID, name = theId.ProductName, temp=theId.Temperature }).ToList();
            var existingPredictions = dbContext.Predictions.ToList();
            dbContext.Predictions.RemoveRange(existingPredictions);
            dbContext.SaveChanges();

            var productWithID = dbContext.Products.FirstOrDefault(p => p.ProductID == id);
            //foreach (var product in allproductIDs)
            //{
            ModelInput theData = new();
            var productOutput = new ModelOutput();
            Prediction prediction1 = new Prediction();
            theData.ProductID = id;
            theData.ProductName = productWithID.ProductName;
            theData.Temperature = todayTemperature;
            var prediction = MLModel.Predict(theData);


            prediction1.ProductID = id;
            prediction1.ProductName = productWithID.ProductName;
            prediction1.Score = prediction.Score;

            productOutput.Score = prediction.Score;
            productOutput.ProductID = id;



            dbContext.Predictions.Add(prediction1);
            predictions.Add(prediction1);

            //dbContext.Recommendations.Add(productOutput);
            //Recommendations.Add(productOutput);
            dbContext.SaveChanges();
            //}

            //ViewBag.Result = prediction;
        }
    }
}
