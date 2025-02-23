using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groupwork.Services
{
    public static class ErrorHandler
    {
        public static void HandleError(Exception e)
        {
            Console.WriteLine($"[red]Ett fel uppstod: {e.Message}[/]");
        }
    }
}
