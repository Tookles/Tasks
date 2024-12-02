using System;
using System.Numerics;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers; 


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
                await Task.Delay(random.Next(1000, 10000), token);
                Console.WriteLine("Hello...");
            });


            Task task2 = Task.Run(async () =>
            {
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

        private static string DecryptMessage(string message)
        {
            string builtString = "";
            foreach (char c in message)
            {
                char charShifted = (char)(c + 1);
                builtString = builtString + charShifted;
            }
            return builtString;
        }

        public static async Task<string> GetFileAndDecrypt(string fileName)
        {
           string result = await AsyncFileManager.ReadFile(fileName);
           return DecryptMessage(result);  
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


            List<Task<BigInteger>> tasks = numbers.Select(async x => await Task.Run(() => Excercises.CalculateFactorial(x))).ToList();

            var newList = tasks.Select(async x => await x.ContinueWith(x => Console.WriteLine(x.Result))).ToList();



            // condensed together: 
            List<Task> tasks2 = numbers.Select( x =>  (Task.Run(() => Excercises.CalculateFactorial(x)))
                .ContinueWith(x => Console.WriteLine(x.Result)))
                .ToList();


            // run with the storycounter: 
            await Task.WhenAll(tasks2).ContinueWith(x => storyCounter);


            //var combinedResults = await Task.WhenAll([
            //    GetFileAndDecrypt("resources/SuperSecretFile.txt"), 
            //    GetFileAndDecrypt("resources/ReallySuperSecretTextFile.txt"), 
            //    GetFileAndDecrypt("resources/SuperTopSecretFile.txt")]);

            //await AsyncFileManager.Writefile("resources/DecryptedMessage.txt", String.Join(" ", combinedResults));

            //List<int> numbers = Enumerable.Range(1, 100).ToList();
            //await ParallelOperations.RunParallelTasksAsync(numbers);

            await AsyncRequest.GetPokemon();

        }
    }
}

