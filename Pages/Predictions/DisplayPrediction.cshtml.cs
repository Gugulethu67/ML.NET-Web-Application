using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;
using System.Security.Policy;
using Team21V4._5.Model;

namespace Team21V4._5.Pages.Predictions
{
    public class DisplayPredictionModel : PageModel
    {
        [NotNull]
        public List<Prediction> predictions = new List<Prediction>();
        public IActionResult OnGet()
        {
            // Retrieve the data from TempData
            var productID = TempData["ProductID"] as float?; // Update the data type as needed
            var productName = TempData["ProductName"] as string;
            var date = TempData["Date"] as DateTime?; // Update the data type as needed

            var predictionResult = TempData["PredictionResult"] as dynamic;
            var property1Value = predictionResult?.Property1;

            var prediction = TempData["PredictionResult"];
            predictions.Add(new Prediction { ProductID = (float)productID,ProductName = productName, Score = property1Value });
            // Use the data as needed in your view or controller logic
            // ...

            return Page();
        }

    }
}
