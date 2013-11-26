using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NumberGuessingGame.Models;

namespace NumberGuessingGame.ViewModels
{
    public class SecrectNumberViewModel
    {   
        //GuessedNumber from SecrectNumber 
        public GuessedNumber Lastguess { get; set; }
        //List of all guesses made from SecrectNumber
        public IList<GuessedNumber> GuessedNumbers { get; set; }
        //Nullable int containing the secret number from SecretNumber
        public int? Number { get; set; }
        //Bool to see if it is possible to make guess from SecretNumber
        public bool CanMakeGuess { get; set; }

        //Make sure guess is set
        [Required(ErrorMessage = "Du måste ange en gissning")]
        //Make sure the guess is within accepted range
        [Range(1, 100, ErrorMessage = "Du måste ange ett heltal mellan 1 och 100")]
        [Display(Name = "Ange gissning")]
        //Guess needs to be a number
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Du måste ange ett heltal mellan 1 och 100")]
        public int Guess { get; set; }

        //Get the current guess number
        public int GetCurrentGuess(int index)
        {
            return index+1;
        }

        //Return string representing the current guess to be made
        public string GetCurrentGuessString() 
        {
            switch (GuessedNumbers.Count)
            {
                case 0:
                    return "Första gissningen";
                case 1:
                    return "Andra gissningen";
                case 2:
                    return "Tredje gissningen";
                case 3:
                    return "Fjärde gissningen";
                case 4:
                    return "Femte gissningen";
                case 5:
                    return "Sjätte gissningen";
                case 6:
                    return "Sjunde gissningen";
                default:
                    return "";
            }
        }

        //Return string representing the last guess made
        public string GuessResult() 
        {            
            string result = "";

            if (Lastguess.Outcome == Outcome.OldGuess)
            {
                result = "Du har har redan gissat på talet " + Lastguess.Number;
            }
            else if (Lastguess.Outcome == Outcome.Right)
            {
                result = "Du gissade rätt! Det tog " + GuessedNumbers.Count + " gissningar.";
            }
            else if (Lastguess.Outcome == Outcome.Low)
            {
                result = Lastguess.Number + " är för lågt";
            }
            else if (Lastguess.Outcome == Outcome.High)
            {
                result = Lastguess.Number + " är för högt";
            }
            if (GuessedNumbers.Count == 7 && Lastguess.Outcome != Outcome.Right)
            {
                result = "Du har slut på gissningar! Det hemliga  talet var " + Number; 
            }

            return result;
        }
    }
}