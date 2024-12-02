using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Tasks
{
    internal class AsyncRequest
    {
            public static async Task GetPokemon()
            {
                string baseUrl = "http://pokeapi.co/api/v2/pokemon/";
                var uri = new Uri(baseUrl);
                try
                {
                    using (HttpClient client = new HttpClient())
                    using (HttpResponseMessage res = await client.GetAsync(uri))
                    using (HttpContent content = res.Content)
                            {
                                String? data = await content.ReadAsStringAsync();
                                Console.WriteLine("Data------------------");
                                Console.WriteLine(data);
                                // Now that we have this data we could parse and utilise this however we like.
                            }
                        
                    
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Exception Hit------------");
                    Console.WriteLine(exception);
                }
            }
        }
    }
  
