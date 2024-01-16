using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Team21V4._5.Model;
using Team21V4_5;
using static Team21V4_5.MLModel;
using static Team21V4_5.MLModel.ModelInput;
using Team21V4._5.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using static Team21V4._5.Pages.Predictions.SinglePredictionsModel;
using Microsoft.AspNetCore.Authorization;

namespace Team21V4._5.Pages.Predictions
{
    [Authorize("LoggedInPolicy")]
    public class SinglePredictionsModel : PageModel
    {
        [NotNull]
        public List<Prediction> predictions = new List<Prediction>();
        [BindProperty]
        public InputModel Input { get; set; }
        public class PredictionResult
        {
            public float Score { get; set; }
            // Add other properties you need from ModelOutput
        }
        public class InputModel
        {
            [Required(ErrorMessage = "ProductID is required.")]
            public int ProductID { get; set; }

            [Required(ErrorMessage = "ProductName is required.")]
            public string ProductName { get; set; }

            [Required(ErrorMessage = "Date is required.")]
            [DataType(DataType.Date)]
            public DateTime Date { get; set; }
        }

        // Your other code here...

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Process and store the form data
                // For example, you can store it in a TempData object
                TempData["ProductID"] = Input.ProductID;
                TempData["ProductName"] = Input.ProductName;
                TempData["Date"] = Input.Date;

                ModelInput theData = new();
                theData.ProductID = Input.ProductID;
                theData.ProductName = Input.ProductName;
                theData.Date = Input.Date.ToString();
                var prediction = MLModel.Predict(theData);

                predictions.Add(new Prediction { ProductID = (float)Input.ProductID, ProductName = Input.ProductName, Score = prediction.Score });


                var predictionResult = new PredictionResult
                {
                    Score = prediction.Score,
                    // Add other properties you need from ModelOutput
                };
                TempData["PredictionResult"] = JsonConvert.SerializeObject(predictionResult);
                //TempData["prediction"] = prediction;

                // Redirect to another page where you want to use the data
                return Page(); // Update the path to your desired destination
            }

            // If there are validation errors, redisplay the form
            return Page();
        }

    }
}