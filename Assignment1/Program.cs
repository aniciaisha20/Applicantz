
using System.Diagnostics;

class Program
{
    /// <summary>
    /// Assignment 1: Main Program to call ReverseWords function and perform Debug.Assert for test string "String; 2be reversed..."
    /// Using different set of words will fail the assert since the condition is only for "String; 2be reversed...", but it will show the correct reversed words accdg. to the criteria
    /// </summary>
    static void Main()
    {
        try
        {
            Console.WriteLine("Input String:");

            string? sInput = Console.ReadLine();
            if (sInput is not null)
            {
                Console.WriteLine($"Output:{ReverseWords(sInput)}");
                Debug.Assert(ReverseWords(sInput) == "gnirtS; eb2 desrever...", "Testing failed!");
            }
        }
        catch(Exception ex){
            Console.WriteLine(ex);
        }
        
    }

    /// <summary>
    /// 1. To reverse each words in the input string excluding punctuation
    /// 2. Order of the words will be unchanged
    /// 3. Characters such as spaces, punctuation will not be reversed
    /// </summary>
    /// <param name="sInput"></param>
    /// <returns>Reverse words</returns>
    public static string ReverseWords(string sWords)
    {
        string[] sToReverse = sWords.Split(' ');
        string[] sReversed = new string[sToReverse.Length];
        for (int x = 0; x < sToReverse.Length; x++)
        {
            //push to 2D array, row 0 = for original position with punctuation, row 1 = reverse words and numbers
            char[] cArray = sToReverse[x].ToCharArray();
            char[,] cReversed = new char[2, cArray.Length];
            int itempLength = 0;
            for (int y = cArray.Length - 1; y >= 0; y--)
            {
                
                if (char.IsPunctuation(cArray[y]))
                {
                    cReversed[0,y] = cArray[y];
                }
                else
                {
                    cReversed[1, itempLength] = cArray[y];
                    itempLength++;
                }
            }

            //merge reverse words with the punctuation
            itempLength = 0;
            for (int z =0; z < cArray.Length; z++)
            {
                if (!char.IsPunctuation(cReversed[0,z]))
                {
                    sReversed[x] += cReversed[1, itempLength];
                    itempLength++;
                }
                else
                {
                    sReversed[x] += cReversed[0, z];
                }
            }
    
        }
        //to join all the words
        return string.Join(" ", sReversed);
    }
}

