using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Tasks
{
    internal class Program
    {

        static async Task PrintWorld()
        {
            Random random = new Random();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task task1 = Task.Run(async () =>
            {
                // cts.CancelAfter(1000);
                await Task.Delay(random.Next(1000, 10000), token);
                Console.WriteLine("Hello...");
            });


            Task task2 = Task.Run(async () =>
            {
                //  cts.CancelAfter(1000);
                await Task.Delay(random.Next(1000, 10000), token);
                Console.WriteLine("...World");
            });


            try
            {
                cts.CancelAfter(2000);
                await Task.WhenAll([task1, task2]);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("\nTasks cancelled: timed out.\n");
            }
            finally
            {
                cts.Dispose();
            }

            Console.WriteLine("Application ending.");
        }




        public static async Task<BigInteger> calculateFactorialTask(BigInteger x)

        {
            BigInteger result = await Task.Run(() => Excercises.CalculateFactorial(x));
            return result;
        }





        static async Task Main(string[] args)
        {

            string story = "Mary had a little lamb, its fleece was white as snow.";
            string[] storyArray = story.Split(" ");

            Task storyCounter = Task.Run(async () =>
            {
                foreach (String word in storyArray)
                {
                    await Task.Delay(1000);
                    Console.WriteLine(word);
                }
            });



            string data = "85671 34262 92143 50984 24515 68356 77247 12348 56789 98760";
            List<BigInteger> numbers = data.Split(" ")
                                    .Select(x => (BigInteger.Parse(x))).ToList();


            // condensed version: 
            List<Task<BigInteger>> tasks2 = numbers.Select(async x => await Task.Run(() => Excercises.CalculateFactorial(x))).ToList();

            //await storyCounter.ContinueWith(x => x); 

            foreach (var task in tasks2)
            {
                await task.ContinueWith(x => Console.WriteLine(x.Result));
            };
 


            // ALTERNATVE: 
            //tasks2.ForEach(async x => await x.ContinueWith(x => Console.WriteLine(x.Result)));


            // ALTERNATE VERSIONS OF ABOVE: 

            // await Task.Run(() => Parallel.ForEach(numbers,x => Console.WriteLine(Excercises.CalculateFactorial(x))));

            //List<Task<BigInteger>> tasks = numbers.Select(x => calculateFactorialTask(x)).ToList();
            //foreach (var task in tasks)
            //{
            //   await task.ContinueWith(x => Console.WriteLine(x.Result));
            //}


        }
    }
}

