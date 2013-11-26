using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumberGuessingGame.ViewModels;
using NumberGuessingGame.Models;

namespace NumberGuessingGame.Controllers
{
    public class SecretNumberController : Controller
    {

        private SecretNumber _secretNumber = new SecretNumber();      
        
        //
        // GET: /SecretNumber/

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["SecretNumberModel"] == null)
            {
                Session["SecretNumberModel"] = new SecretNumber();
            }

            var secretNumber = (SecretNumber)Session["SecretNumberModel"];
            var SecrectNumberViewModel = new SecrectNumberViewModel();
            SecrectNumberViewModel.GuessedNumbers = secretNumber.GuessedNumbers.ToList();
            SecrectNumberViewModel.Number = secretNumber.Number;
            SecrectNumberViewModel.CanMakeGuess = secretNumber.CanMakeGuess;
            return View(SecrectNumberViewModel);
        }

        //
        // POST: /SecretNumber/

        [HttpPost]
        public ActionResult Index(SecrectNumberViewModel viewModel)
        {
            if (ModelState.IsValid)
            {                
                var secretNumber = (SecretNumber)Session["SecretNumberModel"];
                secretNumber.MakeGuess(viewModel.Guess);
                viewModel.Lastguess = secretNumber.LastGuessedNumber;
                viewModel.GuessedNumbers = secretNumber.GuessedNumbers.ToList();                                
            }
            return View(viewModel);
        }

    }
}
