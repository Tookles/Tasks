using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks
{
    internal class AsyncFileManager
    {
        public static async Task<string> ReadFile(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }

        public static async Task Writefile(string filepath, string text)
        {
            await File.WriteAllTextAsync(filepath, text);
        }
    }
}
