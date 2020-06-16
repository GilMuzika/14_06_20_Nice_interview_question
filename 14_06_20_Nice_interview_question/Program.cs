using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;

namespace _14_06_20_Nice_interview_question
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputsLst = new List<string>();
            inputsLst.Add("A,D,F");
            inputsLst.Add("B,D,F");
            inputsLst.Add("F,D,A");
            inputsLst.Add("F,E,B");

            inputsLst.Add("A,B,D,F");
            inputsLst.Add("A,D");
            inputsLst.Add("D,C,F,A");
            inputsLst.Add("E, F");
            inputsLst.Add("N");
            inputsLst.Add("A,E,F");
            inputsLst.Add("A,J,E,F");



            Console.WriteLine("Choose the input sentence:");
            for (int i = 0; i < inputsLst.Count; i++)
            {
                Console.WriteLine($"Press {i} for {inputsLst[i]}");
            }

            int[] arrayOfNumbers;
            Console.WriteLine();
            do {
                
                arrayOfNumbers = (int[])PleaseEnterSomeNumbers(1).Clone();
                

                if ((arrayOfNumbers[0] < 0 || arrayOfNumbers[0] > inputsLst.Count - 1) && arrayOfNumbers[0] != -1)
                    Console.WriteLine($"You must choose between 0 and {inputsLst.Count - 1}. \nTo exit press -1");
                else
                {                    
                    if (arrayOfNumbers[0] == -1) break;



                    int choosenInputNum = arrayOfNumbers[0];
                    Console.WriteLine($"\n\n\n{arrayOfNumbers[0]}: The chosen input sentence: {inputsLst[choosenInputNum]}");
                    var rez = IsAppropriateByFormula(formula: "A,B,C@D,E@F", inputSentense: inputsLst[choosenInputNum]);
                    Console.WriteLine("The result:");
                    Console.WriteLine($"{inputsLst[choosenInputNum]}, {rez}");
                    Console.WriteLine("------------------------------------------------\n\n");
                }
            }
            while (arrayOfNumbers[0] != -1);
            

            


            //Displaying the result

        }


        /// <summary>
        /// The formula method
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="inputSentense"></param>
        /// <returns></returns>
        private static bool IsAppropriateByFormula(string formula, string inputSentense)
        {
            List<string> lstFormula = SplitBySeparator(formula, '@');
            List<List<string>> str1 = new List<List<string>>();
            for (int i = 0; i < lstFormula.Count; i++)
            {
                List<string> lstChar = SplitBySeparator(lstFormula[i], ',');
                str1.Add(lstChar);
            }
            //(A or B or C) AND (D or E) AND F            
            Dictionary<string, bool> dictDict = new Dictionary<string, bool>();

            for (int i = 0; i < str1.Count; i++)
            {                                
                for (int j = 0; j < str1[i].Count; j++)
                {
                    dictDict.Add(str1[i][j], true);
                }
            }

            
            bool flag = false;
            int groupsNum = lstFormula.Count;
            int count = 0;
            List<string> inputLst = SplitBySeparator(inputSentense, ',');

            for (int i = 0; i < inputLst.Count; i++)
            {
                //this row can be removed if you want to allow symbols that don't occur in the formula
                if (!formula.Contains(inputLst[i])) { flag = false; break; }

                try
                {
                    flag = dictDict[inputLst[i]];
                    if (flag) { count++; }

                    for(int j = 0; j < str1.Count; j++)
                    {
                        if (str1[j].Contains(inputLst[i]))
                        {
                            if (dictDict.ContainsKey(inputLst[i]))
                            {
                                for (int n = 0; n < str1[j].Count; n++)
                                {
                                    if(!dictDict.Remove(str1[j][n]))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    
                }
                catch
                {
                    if (formula.Contains(inputLst[i])) return false;
                }
            }

            if (count < groupsNum) return false;

            return flag;


        }


        //input: "A,D,F"
        //"A,B,C@D,E@F"
        //it means: you need to get only a or b or c AND d or e AND f. and must get one of them   
        private static List<string> SplitBySeparator(string stringToBrSplit, char separator)
        {
            List<string> separatedParstLst = new List<string>();
            string tmp = string.Empty;
            char c = default(char);
            for (int i = 0; i < stringToBrSplit.Length; i++)
            {
                if (stringToBrSplit[i] != separator) tmp += stringToBrSplit[i];
                if (i + 1 < stringToBrSplit.Length) c = stringToBrSplit[i + 1];
                if (i == stringToBrSplit.Length - 1) c = separator;
                if (c.Equals(separator)) { separatedParstLst.Add(tmp); tmp = string.Empty; }
            }

            return separatedParstLst;
        }

        private static List<string> SplitByPreSeparator(string stringToBrSplit, char preSeparator)
        {
            List<string> separatedParstLst = new List<string>();
            string tmp = string.Empty;
            char c = default(char);
            for (int i = 0; i < stringToBrSplit.Length; i++)
            {
                if (stringToBrSplit[i] != preSeparator) tmp += stringToBrSplit[i];
                if (i + 1 < stringToBrSplit.Length) c = stringToBrSplit[i + 1];
                if (i == stringToBrSplit.Length - 1) c = preSeparator;
                if(c.Equals(preSeparator))
                {
                    tmp += stringToBrSplit[i + 1];
                    tmp += stringToBrSplit[i + 2];
                    i = i + 3;
                    separatedParstLst.Add(tmp);
                    tmp = string.Empty;
                }

            }

            return separatedParstLst;
        }





        static int[] PleaseEnterSomeNumbers(int iterations)
        {
            if (iterations == 1) { Console.WriteLine("Please enter one number:\nTo exit, enter \"-1\"\n"); }
            else { Console.WriteLine("Please enter {0} numbers:\n", iterations); }

            int[] arriterations = new int[iterations];

            for (int i = 0; i < iterations; i++)
            {
                if (Int32.TryParse(Console.ReadLine(), out int line)) arriterations[i] = line;
                else { i--; Console.WriteLine("\n This is not a number! \nPlease enter only numbers. \nNow lets try again: \n"); }
            }
            return arriterations;
        }
    }
}
