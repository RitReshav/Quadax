using System;
using System.Text;

namespace Mastermind
{
    class Program
    {
        private const int CodeLength = 4;
        private const int MaxNumber = 6;
        private const int NumOfAttempts = 10;
        static void Main(string[] args)
        {
            string intent = string.Empty;
            do
            {
                string randomCodeString = "";
                bool brokenCode = false;
                Console.WriteLine("******Welcome to Mastermind*******");
                int[] randomCode = new int[MaxNumber + 1];
                randomCode = GetRandomCode(ref randomCodeString);

                Console.WriteLine("You now have 10 attempts to break the code");
                for (int i = 0; i < NumOfAttempts; i++)
                {
                    int numOfPlus = 0;
                    int numOfMinus = 0;
                    int[] numberOfPlusTracker = new int[MaxNumber + 1];
                    int[] numberOfMinusTracker = new int[MaxNumber + 1];
                    Console.WriteLine($"Attempt {i+1}");
                    Console.WriteLine("==========");
                    Console.Write($"Please enter {CodeLength} digits between 1 and {MaxNumber} without space: ");
                    for (int j = 1; j <= CodeLength; j++)
                    {
                        char input = Console.ReadKey().KeyChar;
                        int number;
                        bool validInput = Int32.TryParse(input.ToString(), out number);
                        if (!validInput || number < 1 || number > 6)
                        {
                            Console.WriteLine("Invalid Input. Please try again");
                            i--;
                            break;
                        }
                        
                        if (randomCode[number] == j)
                        {
                            numOfPlus++;
                            numberOfPlusTracker[number]++;
                        }
                        else if (randomCode[number] > CodeLength)
                        {
                            int checkNumber = randomCode[number];
                            bool matchFound = false;
                            while (checkNumber != 0)
                            {
                                if (checkNumber % 10 == j)
                                {
                                    numOfPlus++;
                                    numberOfPlusTracker[number]++;
                                    matchFound = true;
                                    break;
                                }
                                checkNumber = checkNumber / 10;
                            }
                            if (!matchFound)
                            {
                                numOfMinus++;
                                numberOfMinusTracker[number]++;
                            }
                        }
                        else if (randomCode[number] > 0)
                        {
                            numOfMinus++;
                            numberOfMinusTracker[number]++;
                        }
                    }
                    Console.WriteLine();
                    if (numOfPlus == CodeLength)
                    {
                        Console.WriteLine("You successfully broke the code");
                        brokenCode = true;
                        break;
                    }
                    Console.Write("Result for this attempt: ");
                    for (int j = 1; j <= MaxNumber; j++)
                    {
                        int num = 0;
                        if(randomCode[j] > 0) num = Convert.ToInt32(Math.Floor(Math.Log10(randomCode[j]) + 1));
                        if (num > 0)
                        {
                            int surplusNegatives = numberOfPlusTracker[j] + numberOfMinusTracker[j] - num;
                            if(surplusNegatives > 0) numOfMinus -= surplusNegatives;
                        }
                    }
                    for (int j = 0; j < numOfPlus; j++)
                    {
                        Console.Write("+");
                    }
                    for (int j = 0; j < numOfMinus; j++)
                    {
                        Console.Write("-");
                    }
                    Console.WriteLine();
                }
                if (!brokenCode)
                {
                    Console.WriteLine("Random Code is " + randomCodeString);
                    Console.WriteLine("Better luck next time.");
                }
                Console.WriteLine();
                Console.Write("DO you want to play again? Please y to play again:");
                intent = Console.ReadLine();
            } while (intent == "y");
        }

        private static int[] GetRandomCode(ref string randomCodeString)
        {
            StringBuilder sb = new StringBuilder();
            int[] randomCode = new int[MaxNumber + 1];
            Random rnd = new Random();
            for (int i = 0; i < CodeLength; i++)
            {               
                int randomDigit = rnd.Next(1, MaxNumber + 1);
                sb.Append(randomDigit);
                if(randomCode[randomDigit] != 0)
                {
                    randomCode[randomDigit] = randomCode[randomDigit] * 10 + (i + 1);
                }
                else
                {
                    randomCode[randomDigit] = i + 1;
                }                
            }
            randomCodeString = sb.ToString();
            return randomCode;
        }
    }
}
