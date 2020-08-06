using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{

    // Complete the highestValuePalindrome function below.
    static string highestValuePalindrome(string s, int n, int k)
    {
        List<int> inputNumber = s.Select(x => Convert.ToInt32(x.ToString())).ToList();
        var strInputNumber = new List<string>();

        List<int> leftSide = new List<int>();
        List<int> rightSide = new List<int>();
        string result = string.Empty;
        string differenceBetweenNumbersStr = string.Empty;

        int middleNumber = -1;

        if (inputNumber.Count > 1)
        {
            if (n % 2 == 0)
            {
                leftSide = inputNumber.GetRange(0, n / 2);
                rightSide = inputNumber.GetRange(n / 2, n / 2);
            }
            else
            {
                leftSide = inputNumber.GetRange(0, n / 2);
                rightSide = inputNumber.GetRange(n / 2 + 1, n / 2);
                middleNumber = inputNumber[n / 2];
            }

            rightSide.Reverse();

            int k_needed = 0;

            for (int i = 0; i < leftSide.Count; i++)
            {
                if (leftSide[i] != rightSide[i])
                    k_needed++;
            }

            if (k_needed == k)
            {
                for (int i = 0; i < leftSide.Count; i++)
                {
                    if (leftSide[i] == rightSide[i])
                        continue;
                    else
                    {
                        if (leftSide[i] > rightSide[i])
                            rightSide[i] = leftSide[i];
                        else
                            leftSide[i] = rightSide[i];
                    }
                }
            }
            else if (k_needed < k)
            {
                if (k_needed == 0)
                {
                    for (int i = 0; i < leftSide.Count; i++)
                    {
                        if (k != 0 && k / 2 >= 1)
                        {
                            if (leftSide[i] == 9 && rightSide[i] == 9)
                                continue;
                            if (leftSide[i] == 9 && rightSide[i] != 9)
                            {
                                rightSide[i] = 9;
                                k--;
                            }
                            else if (rightSide[i] == 9 && leftSide[i] != 9)
                            {
                                leftSide[i] = 9;
                                k--;
                            }
                            else
                            {
                                rightSide[i] = 9;
                                leftSide[i] = 9;
                                k -= 2;
                            }
                        }
                        else if (k == 1 && middleNumber != -1)
                        {
                            middleNumber = 9;
                            k--;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < leftSide.Count; i++)
                    {
                        if (k > 0)
                        {
                            if (k_needed < k)
                            {
                                if (leftSide[i] == 9 && rightSide[i] == 9)
                                    continue;
                                if (leftSide[i] == 9 && rightSide[i] != 9)
                                {
                                    rightSide[i] = 9;
                                    k--;
                                    k_needed--;
                                }
                                else if (rightSide[i] == 9 && leftSide[i] != 9)
                                {
                                    leftSide[i] = 9;
                                    k--;
                                    k_needed--;
                                }
                                else if (k >= 2 && leftSide[i] == rightSide[i])
                                {
                                    rightSide[i] = 9;
                                    leftSide[i] = 9;
                                    k -= 2;
                                }
                                else if (k >= 2)
                                {
                                    rightSide[i] = 9;
                                    leftSide[i] = 9;
                                    k_needed--;
                                    k -= 2;
                                }

                                //if (k <= k_needed)
                                //    i = 0;
                            }
                            else
                            {
                                if (leftSide[i] == rightSide[i])
                                    continue;
                                else
                                {
                                    if (leftSide[i] > rightSide[i])
                                    {
                                        rightSide[i] = leftSide[i];
                                        k--;
                                        k_needed--;
                                    }
                                    else
                                    {
                                        leftSide[i] = rightSide[i];
                                        k--;
                                        k_needed--;
                                    }
                                }
                            }
                        }

                    }
                    if (k > 0 && middleNumber != -1)
                    {
                        middleNumber = 9;
                    }
                }
            }
            else
            {
                result = "-1";
            }
        }
        else
        {
            if (k > 0 && inputNumber[0] != 9)
            {
                inputNumber[0] = 9;
                k--;
            }

            if (k < 0)
            {
                result = "-1";
            }
        }


        if (result == "-1")
            return result;
        else if (inputNumber.Count == 1)
        {
            return inputNumber[0].ToString();
        }
        else
        {
            rightSide.Reverse();

            if (middleNumber == -1)
                result = string.Join("", leftSide) + string.Join("", rightSide);
            else
                result = string.Join("", leftSide) + middleNumber + string.Join("", rightSide);
        }
        return result;
    }

    static void Main(string[] args)
    {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nk = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nk[0]);

        int k = Convert.ToInt32(nk[1]);

        string s = File.ReadAllText(@"C:\tmp\hackerRankTestData.txt");

        //string s = Console.ReadLine();

        string result = highestValuePalindrome(s, n, k);

        Console.WriteLine(result);
        File.WriteAllText(@"C:\tmp\hackerRankTestData1.txt", result);
        Console.ReadLine();


        //textWriter.Flush();
        //textWriter.Close();
    }
}
