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

        /* ReadOddByte() skriver ut en medskickad prompt och läser
         * av värdet som kommer in. Därefter valideras detta mot
         * NaN och om det är udda och större än 0. Om det inte stämmer
         * skickas felmeddelande och loopen startas om i funktionen.
         */
        static byte ReadOddByte(string prompt, byte maxValue = 255)
        {
            byte maxCount = 0;

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
                            break;
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                catch
                {
                    ViewMessage(Resources.Input_Error, true, userInput);
                }
            } while (true);

            return maxCount;
        }

        /*RenderDiamond() har 2st loopar, den första tar hand om övre 
         * halvan inklusive "bältet". Den andra loopen tar han om undre 
         * halvan och subtraherar två för att undvika dubbel utskrift av
         * "bältet". */
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

        /*RenderRow() Räknar först ut hur många mellanslag som bör
         * vara på raden för att centrera diamanten. Därefter skrivs
         * mellanslag och asterisker ut */
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

        /* IsContinuing() skriver först ut ett meddelande till användaren
         * och lyssnar där efter Escape-tangenten, annars tar den bort
         * allt som finns i konsol-fönstret och låter loopen fortsätta med
         * värdet true */
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

        /* ViewMessage() kollar först om det är ett felmeddelande som
         * ska skrivas ut och sätter färger därefter. Sedan kollar den
         * om det finns en referens till det meddelandet som ska skickas
         * med och skriver ut meddelandet.*/
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
