using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задание_22.Параллельное_программирование
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов массива:\n");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            //Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(SortArray);
            //Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            //Action<Task<int[]>> action = new Action<Task<int[]>>(PrintArray);
            //Task task3 = task2.ContinueWith(action);

            Func<Task<int[]>, int[]> func3 = new Func<Task<int[]>, int[]>(GetSum);
            Task<int[]> task4 = task1.ContinueWith<int[]>(func3);

            Action<Task<int[]>> action = new Action<Task<int[]>>(MaxArray);
            Task task5 = task1.ContinueWith(action);

            task1.Start();
            Console.ReadKey();

        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                array[i] = random.Next(0, 50);
            }
            return array;
        }
        //static int[] SortArray(Task<int[]> task)
        //{
        //    int[] array = task.Result;
        //    for (int i = 0; i < array.Count() - 1; i++)
        //    {
        //        for (int j = i + 1; j < array.Count(); j++)
        //        {
        //            if (array[i] > array[j])
        //            {
        //                int t = array[i];
        //                array[i] = array[j];
        //                array[j] = t;
        //            }
        //        }
        //    }
        //    return array;
        //}
        //static void PrintArray(Task<int[]> task)
        //{
        //    int[] array = task.Result;
        //    for (int i = 0; i < array.Count(); i++)
        //    {

        //        Console.Write($"{array[i]} ");
        //    }
        //}

        static int GetSum(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            foreach (int i in array)
            {
                sum += i;
            }
            return sum;
        }

        static void MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            foreach (int i in array)
            {
                if (i > max)
                    max = i;
            }
        }
    }
}
