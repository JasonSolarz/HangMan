using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

//PseudoCode
//List of random words
//List of characters a - z. Delete when used.
//Create the Hangman post
//Choose a word and display the words as ------
//User Input, search random word for char. If available, display and delete from list. Clear Screen/refresh.

namespace Hangmann
{
    class Program
    {
        static void Main(string[] args)
        {

            //Variables, Declarations and Initialization
            string[] words = File.ReadAllLines("Words.txt");
            List<char> availableCharacters = new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
                'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};


            Random rand = new Random();

            var randomWord = words[rand.Next(1, words.Length)];
            char[] blankWord = new char[randomWord.Length];

            var numWrongGuesses = 0;
            char userInput = ' ';

            //Initialize blankWord Array
            for(int LCV = 0; LCV < blankWord.Length; LCV++)
            {
                blankWord[LCV] = '-';
            }

            while (userInput != '!')
            {
                Console.Clear();
                Console.WriteLine("--------/Hangman - Animals!/--------");
                Console.WriteLine("Created by Jason Solarz - 2/18/20\n");

                DisplayHangman(numWrongGuesses);
                DisplayWordToGuess(blankWord);
                DisplayLettersToGuess(availableCharacters);
                userInput = UserChooseLetter(availableCharacters);
                blankWord = UpdateBlankWord(userInput, blankWord, randomWord, ref numWrongGuesses);
                DisplayWordToGuess(blankWord);
                userInput = CheckIfWon(blankWord, randomWord, numWrongGuesses);
            }

            void DisplayHangman(int userGuesses)
            {
                if (userGuesses == 0)
                {
                    Console.WriteLine("   _______");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |         ");
                    Console.WriteLine("   |         ");
                    Console.WriteLine("   |         ");
                    Console.WriteLine("   |\\       ");
                    Console.WriteLine("   | \\       ");
                }
                else if(userGuesses == 1)
                {
                    Console.WriteLine("   _______");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |       ");
                    Console.WriteLine("   |       ");
                    Console.WriteLine("   |\\       ");
                    Console.WriteLine("   | \\       ");
                }
                else if(userGuesses == 2)
                {
                    Console.WriteLine("   _______");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |      #");
                    Console.WriteLine("   |         ");
                    Console.WriteLine("   |\\       ");
                    Console.WriteLine("   | \\       ");
                }
                else if(userGuesses == 3)
                {
                    Console.WriteLine("   _______");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |     /#");
                    Console.WriteLine("   |         ");
                    Console.WriteLine("   |\\       ");
                    Console.WriteLine("   | \\       ");
                }
                else if(userGuesses == 4)
                {
                    Console.WriteLine("   _______");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |     /#\\");
                    Console.WriteLine("   |         ");
                    Console.WriteLine("   |\\       ");
                    Console.WriteLine("   | \\       ");
                }
                else if(userGuesses == 5)
                {
                    Console.WriteLine("   _______");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |     /#\\");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |\\       ");
                    Console.WriteLine("   | \\       ");
                }
                else if(userGuesses == 6)
                {
                    Console.WriteLine("   _______");
                    Console.WriteLine("   |      |");
                    Console.WriteLine("   |      O");
                    Console.WriteLine("   |     /#\\");
                    Console.WriteLine("   |      |\\");
                    Console.WriteLine("   |\\       ");
                    Console.WriteLine("   | \\       ");
                }
            }

            void DisplayWordToGuess(char[] blankWord)
            {
                //Console.WriteLine("The random word we choose is: " + randomWord);
                Console.WriteLine();
                Console.Write("Guess This Word: ");
                for (int LCV = 0; LCV < blankWord.Length; LCV++)
                {
                    Console.Write(blankWord[LCV]);
                }
                Console.WriteLine();
            }

            void DisplayLettersToGuess(List<char> letters)
            {
                Console.WriteLine("\nLetters To Choose From:");
                int rows = letters.Count / 3;
                int num = 0;

                foreach(char letter in letters)
                {
                    if (num < rows)
                    {
                        Console.Write(letter);
                        Console.Write(" ");
                        num++;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write(letter);
                        Console.Write(" ");
                        num = 0;
                    }
                }
            }
            char UserChooseLetter(List<char> availableLetters)
            {
                char userInput;

                Console.Write("\n\nChoose a letter: ");
                userInput = Convert.ToChar(Console.ReadLine());

                //Check to see if character is available
                if (availableLetters.Contains(userInput))
                {
                    availableLetters.RemoveAt(availableLetters.IndexOf(userInput));
                    
                }
                else
                {
                    Console.WriteLine("Invalid Choice. Please try again");
                }
                return userInput;
            }
            char[] UpdateBlankWord(char userInput, char[] blankWord, string randomWord, ref int numWrongGuesses)
            {
                bool letterFound = false;

                for (int LCV = 0; LCV < randomWord.Length; LCV++)
                {

                    if (randomWord[LCV] == userInput)
                    {
                        letterFound = true;
                        blankWord[LCV] = userInput;
                    }
                    
                    if (LCV == randomWord.Length - 1 && letterFound)
                    {
                        Console.WriteLine("That Letter Exists! Please Continue!");
                        Thread.Sleep(2500);
                    }
                    
                }
                if (!letterFound)
                {
                    Console.WriteLine("That Letter Does Not Exist! " + (5 - numWrongGuesses) + " Guesses Remaining!");
                    Thread.Sleep(2500);
                    numWrongGuesses++;
                }
                return blankWord;
            }

            char CheckIfWon(char[] blankWord, string randomWord, int numWrongGuessses)
            {
                string temp = new string(blankWord);
                if (temp == randomWord)
                {
                    Console.WriteLine("CONGRATULATIONS! YOU SAVED THE MAN!");
                    Thread.Sleep(4000);
                    return '!';
                }
                else if (numWrongGuesses == 6)
                {
                    Console.WriteLine("The Word Was: " + randomWord);
                    Console.WriteLine("YOU HAVE SENTENCED THE MAN TO DEATH! THANKS FOR PLAYING!");
                    Thread.Sleep(4000);
                    return '!';
                }
                else
                {
                    return ' ';
                }
            }

        }


    }
}
