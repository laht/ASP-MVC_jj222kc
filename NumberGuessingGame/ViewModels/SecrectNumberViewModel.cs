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
        public GuessedNumber Lastguess { get; set; }
        public List<GuessedNumber> GuessedNumbers { get; set; }
        public int? Number { get; set; }
        public bool CanMakeGuess { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Ange gissning")]
        public int Guess
        { get; set; }
    }
}