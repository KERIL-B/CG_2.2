using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_3_SORTS
{
    class Program
    {
        public static int shellCOUNT { get; set; }
        public static int quickCOUNT { get; set; }
        public static int mergeCOUNT { get; set; }

        public static Random rand = new Random();

        static void Main(string[] args)
        {
            #region Генерация массива
            Console.WriteLine("//////////////////////////////////////////////////////////////////");
            Console.WriteLine("////////////////////////////// SORTS /////////////////////////////");
            Console.WriteLine("//////////////////////////////////////////////////////////////////");
            Console.WriteLine();
            Console.Write("Enter length of massive ->");
            bool tmp = false;
            int n = 0;

            do
            {
                try
                {
                    n = Convert.ToInt32(Console.ReadLine());
                    tmp = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect length");
                    Console.Write("Enter length of massive ->");
                    tmp = true;
                }
            } while (tmp);

            int[] arr = new int[n];
            Console.WriteLine("Massive generating..");
            RandomGenerate(arr);
            Console.WriteLine("Done.");
            #endregion
            #region Инициализация количества повторений
            Console.Write("Enter number of repetitions ->");
            tmp = false;
            int repetitions = 0;
            do
            {
                try
                {
                    repetitions = Convert.ToInt32(Console.ReadLine());
                    tmp = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Incorrect number");
                    Console.Write("Enter number of repetitions ->");
                    tmp = true;
                }
            } while (tmp);
            Console.WriteLine("Correct.");
            #endregion
            #region Эксперимент
            Console.WriteLine("Experiment in process");

            int shellSum = 0;
            int shellMin = n * n + 1;
            int shellMax = 0;

            int quickSum = 0;
            int quickMin = n * n + 1;
            int quickMax = 0;

            int mergeSum = 0;
            int mergeMin = n * n + 1;
            int mergeMax = 0;

            for (int i = 0; i < repetitions; i++)
            {
                ShellSort(arr);
                QuickSort(arr, 0, arr.Length - 1);
                arr = MergeSortCount(arr);

                shellSum += shellCOUNT;
                quickSum += quickCOUNT;
                mergeSum += mergeCOUNT;

                if (shellCOUNT < shellMin)
                    shellMin = shellCOUNT;
                if (shellCOUNT > shellMax)
                    shellMax = shellCOUNT;

                if (quickCOUNT < quickMin)
                    quickMin = quickCOUNT;
                if (quickCOUNT > quickMax)
                    quickMax = quickCOUNT;

                if (mergeCOUNT < mergeMin)
                    mergeMin = mergeCOUNT;
                if (mergeCOUNT > mergeMax)
                    mergeMax = mergeCOUNT;

                RandomGenerate(arr);
            }
            #endregion
            #region Вывод результата
            Console.WriteLine();
            Console.WriteLine("============================  Resaults  ==========================");
            Console.WriteLine();
            Console.WriteLine("Number of actions in SHELL SORT {0} O(N={1})", shellSum / repetitions, n);
            Console.WriteLine("Number of actions in QUICK SORT {0} O(N*log2N={1})", quickSum / repetitions, Math.Round(n * Math.Log(n, 2)));
            Console.WriteLine("Number of actions in MERGE SORT {0}", mergeSum / repetitions);
            Console.WriteLine();
            Console.WriteLine("Min of actions in SHELL SORT {0} O(N={1})", shellMin, n);
            Console.WriteLine("Min of actions in QUICK SORT {0} O(N*log2N={1})", quickMin, Math.Round(n * Math.Log(n, 2)));
            Console.WriteLine("Min of actions in MERGE SORT {0}", mergeMin);
            Console.WriteLine();
            Console.WriteLine("Max of actions in SHELL SORT {0} O(N*N={1})", shellMax, n * n);
            Console.WriteLine("Max of actions in QUICK SORT {0} O(N*N={1})", quickMax, n * n);
            Console.WriteLine("Max of actions in MERGE SORT {0} O(N*N={1})", mergeMax, n * n);
            Console.ReadKey();
            #endregion
        }

        static private void ShellSort(int[] arr)
        {
            int size = arr.Length;
            int step = size / 2;

            while (step > 0)
            {
                for (int i = 0; i < (size - step); i++)
                {
                    int j = i;
                    while (j >= 0 && arr[j] > arr[j + step])
                    {
                        shellCOUNT++;
                        int temp = arr[j];
                        arr[j] = arr[j + step];
                        arr[j + step] = temp;
                        j--;
                    }
                }
                step = step / 2;
            }
        }

        static void QuickSort(int[] arr, int l, int r)
        {
            int temp;
            int x = arr[l + (r - l) / 2];
            int i = l;
            int j = r;
            while (i <= j)
            {

                while (arr[i] < x)
                {
                    i++;
                    quickCOUNT++;
                }
                while (arr[j] > x)
                {
                    quickCOUNT++;
                    j--;
                }
                if (i <= j)
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                    j--;
                }
            }
            if (i < r)
                QuickSort(arr, i, r);

            if (l < j)
                QuickSort(arr, l, j);
        }

        static int[] MergeSortCount(int[] massive)
        {
            if (massive.Length == 1)
                return massive;
            int mid_point = massive.Length / 2;
            return Merge(MergeSortCount(massive.Take(mid_point).ToArray()), MergeSortCount(massive.Skip(mid_point).ToArray()));
        }

        static int[] Merge(int[] mass1, int[] mass2)
        {
            int a = 0, b = 0;
            int[] merged = new int[mass1.Length + mass2.Length];
            for (int i = 0; i < mass1.Length + mass2.Length; i++)
            {
                mergeCOUNT++;
                if (b < mass2.Length && a < mass1.Length)
                    if (mass1[a] > mass2[b])
                        merged[i] = mass2[b++];
                    else 
                        merged[i] = mass1[a++];
                else
                    if (b < mass2.Length)
                        merged[i] = mass2[b++];
                    else
                        merged[i] = mass1[a++];
            }
            return merged;
        }

        static private void RandomGenerate(int[] arr)
        {
            shellCOUNT = 0;
            quickCOUNT = 0;
            mergeCOUNT = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(200) - 100;
            }
        }

        static private void Print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            { Console.Write(arr[i] + " "); }
            Console.WriteLine();
        }
    }
}
