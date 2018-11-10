using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingLesson5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число (факториал ) :");
            int fact = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите число (сумма целых чисел) :");
            int summ = Convert.ToInt32(Console.ReadLine());


            var secondThread = new Thread(new ParameterizedThreadStart(SecondThread));
            secondThread.Name = "Second Thread";
            secondThread.Start(fact);
            int result = 0;
            for (int i=1;i<summ+1;i++)
            {
               result += i;
            }

            Console.WriteLine($"Сумма чисел до N: {result}");


        }

        private static object _SyncRoot = new object();

        static void SecondThread(object parameter)
        {
            var currentThread = Thread.CurrentThread;

            #region Факториал числа
            lock (_SyncRoot)
            {
                if (currentThread.IsAlive)
                {
                    int temp = (int)parameter;
                    Func<int, int> fact = null;
                    fact = (x) => x > 1 ? x * fact(x - 1) : 1;
                    Console.WriteLine($"Факториал числа:{fact(temp)}");
                    Console.ReadKey();
                }
                currentThread.Interrupt();
                }
            #endregion
        }
    }
}
