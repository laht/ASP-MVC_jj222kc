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
        private const string SECRETNUMBER = "SecretNumberModel";

        //
        // GET: /SecretNumber/

        [HttpGet]
        public ActionResult Index()
        {
            //Create session object if null
            if (Session[SECRETNUMBER] == null)
            {
                Session[SECRETNUMBER] = new SecretNumber();
            }
            //Get the secrectNumber model
            var secretNumber = (SecretNumber)Session[SECRETNUMBER];
            
            //Create a viewModel
            var viewModel = new SecrectNumberViewModel();

            //Set viewModel properties to match the model
            viewModel.Number = secretNumber.Number;
            viewModel.Lastguess = secretNumber.LastGuessedNumber;
            viewModel.GuessedNumbers = secretNumber.GuessedNumbers;
            viewModel.CanMakeGuess = secretNumber.CanMakeGuess;

            return View(viewModel);
        }

        //
        // POST: /SecretNumber/

        [HttpPost]
        public ActionResult Index(SecrectNumberViewModel viewModel)
        {
            if (Session.IsNewSession)
            {
                return View("SessionEndedView");
            }

            var secretNumber = (SecretNumber)Session[SECRETNUMBER];

            //If user has entered valid data make a guess
            if (ModelState.IsValid)
            {
                //make guess with user input
                secretNumber.MakeGuess(viewModel.Guess);
            }

            //Set viewModel properties to match the model
            viewModel.Number = secretNumber.Number;
            viewModel.Lastguess = secretNumber.LastGuessedNumber;
            viewModel.GuessedNumbers = secretNumber.GuessedNumbers;
            viewModel.CanMakeGuess = secretNumber.CanMakeGuess;

            return View(viewModel);
        }

        //Start new game
        [HttpGet]
        public RedirectResult newGame() 
        {
            Session[SECRETNUMBER] = null;
            return Redirect("~/");
        }

    }
}
