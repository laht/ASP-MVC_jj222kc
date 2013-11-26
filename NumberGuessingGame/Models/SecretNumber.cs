using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.Models
{
    public class SecretNumber
    {
        private List<GuessedNumber> _guessedNumbers;
        private GuessedNumber _lastGuessedNumber;
        private int? _number;
        public const int MaxNumberOfGuesses = 7;

        public bool CanMakeGuess
        {
            get
            {
                if (_lastGuessedNumber.Outcome == Outcome.Right)
                {
                    return false;
                }
                else if (Count < MaxNumberOfGuesses)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int Count
        {
            get
            {
                return _guessedNumbers.Count;
            }
        }
        public IList<GuessedNumber> GuessedNumbers 
        {
            get
            {
                return _guessedNumbers.AsReadOnly();
            }
        }
        public GuessedNumber LastGuessedNumber 
        { 
            get
            {
                return _lastGuessedNumber;
            }
        }
        public int? Number 
        {
            get 
            {
                if (CanMakeGuess == true)
                {
                    return null;
                }
                else
                {
                    return _number;
                }
            }
            private set { }
        }
        public Outcome MakeGuess(int guess) 
        {
            if (guess < 1 || guess > 100)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < GuessedNumbers.Count; i++)
            {
                if (guess == GuessedNumbers[i].Number)
                {
                    _lastGuessedNumber.Number = guess;
                    _lastGuessedNumber.Outcome = Outcome.OldGuess;
                    return Outcome.OldGuess;
                }
            }

            _lastGuessedNumber = new GuessedNumber();

            if (_guessedNumbers.Count >= MaxNumberOfGuesses)
            {                
                _lastGuessedNumber.Outcome = Outcome.NoMoreGuesses;
                return Outcome.NoMoreGuesses;
            }
            else if (guess == _number)
            {                
                addGuess(Outcome.Right, guess);
                return Outcome.Right;
            }             
            else if (guess > _number)
            {                                
                addGuess(Outcome.High, guess);
                return Outcome.High;
            }
            else
            {
                addGuess(Outcome.Low, guess);
                return Outcome.Low;
            }
        }

        private void addGuess(Outcome outcome, int guess) 
        {
                _lastGuessedNumber.Number = guess;
                _lastGuessedNumber.Outcome = outcome;
                _guessedNumbers.Add(_lastGuessedNumber);                
        }

        public void Initialize()
        {
            _number = new Random().Next(1, 100);
            _guessedNumbers.Clear();
            _lastGuessedNumber.Number = null;
            _lastGuessedNumber.Outcome = Outcome.Indefinite;
        }
        public SecretNumber()
        {            
            _guessedNumbers = new List<GuessedNumber>(7);
            Initialize();
        }
    }
}
