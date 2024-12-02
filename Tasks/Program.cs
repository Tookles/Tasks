namespace Tasks
{
    internal class Program
    {

        static async Task PrintWorld()
        {

            Task task1 = Task.Run(async () =>
            {

                await Task.Delay(3000);
                Console.WriteLine("Hello...");
            });


            Task task2 = Task.Run(async () =>
            {

                await Task.Delay(3000);
                Console.WriteLine("...World");
            });

            await Task.WhenAll([task1, task2]);

        }


        static async Task Main(string[] args)
        {
            await PrintWorld();


        }
    }
}
