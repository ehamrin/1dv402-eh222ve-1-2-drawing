using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1DV402.S1.L02C.Properties;

namespace _1DV402.S1.L02C
{
    class Program
    {

        static void Main(string[] args)
        {
            /*
            * Set program settings 
            ****************************************************************/
            const byte MaxWidth = 79;

            /*
            * Aesthetic changes to console. 
            ****************************************************************/
            Console.Title = Resources.Console_Title;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            do
            {
                /*
                * Read user input.
                ****************************************************************/
                byte maxCount = ReadOddByte(Resources.MaxCount_Prompt, MaxWidth);

                /*
                * Start drawing the diamond.
                ****************************************************************/
                RenderDiamond(maxCount);

            } while (IsContinuing());
        }

        static byte ReadOddByte(string prompt, byte maxValue = 255)
        {

            byte maxCount = 0;
            bool inputValid = false;

            do
            {
                Console.Write(prompt, maxValue);
                string userInput = Console.ReadLine();

                try
                {
                    //Convert input to byte (Throws exceptions on NaN)
                    maxCount = Convert.ToByte(userInput);

                    //Check if input is in valid range
                    if (maxCount > 0 && maxCount <= maxValue)
                    {
                        //Check if input equals odd number 
                        if (maxCount % 2 != 0)
                        {
                            inputValid = true;
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
                catch
                {
                    ViewMessage(Resources.Input_Error, true, userInput);
                }
            } while (inputValid == false);

            return maxCount;
        }

        static void RenderDiamond(byte maxCount)
        {
            //Top triangle and widest point.
            for (int i = 1; i <= maxCount; i += 2)
            {
                RenderRow(maxCount, i);
            }

            //Bottom triangle without the widest point
            for (int i = (maxCount - 2); i > 0; i -= 2)
            {
                RenderRow((int)maxCount, i);
            }
        }

        static void RenderRow(int maxCount, int asteriskCount)
        {
            //Determine number of spaces needed on row
            int spaces = (maxCount - asteriskCount) / 2;

            //Write spaces
            for (int i = 0; i < spaces; i++)
            {
                Console.Write(" ");
            }

            //Write asterisks
            for (int i = 0; i < asteriskCount; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }

        static bool IsContinuing()
        {
            ConsoleKeyInfo keyPress;

            ViewMessage(Resources.Continue_Prompt);
            keyPress = Console.ReadKey();

            if (keyPress.Key == ConsoleKey.Escape)
            {
                return false;
            }
            else
            {
                //Clear any information in console before next loop.
                Console.Clear();

                return true;
            }
        }

        static void ViewMessage(string message, bool isError = false, string referenceString = null)
        {
            if (isError)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (referenceString != null)
            {
                Console.WriteLine(message, referenceString);
            }
            else
            {
                Console.WriteLine(message);
            }

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
