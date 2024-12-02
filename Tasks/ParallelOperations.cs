using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    public class ParallelOperations
    {
        static void ProcessNumber(int number)
        {
            var random = new Random();
            Console.WriteLine($"Processing number: {number}");
            Task.Delay(random.Next(1000, 10000)).Wait();
            Console.WriteLine($"{number} has been processed");
        }

        public static async Task RunParallelTasksAsync(List<int> inputList) {
            Parallel.For(0, inputList.Count, x =>
            {
               ProcessNumber(inputList[x]);
            });  
        
        }

     
    }
}
