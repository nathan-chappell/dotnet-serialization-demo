using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace cons
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            IDerived d = new Derived("a", "b");
            OkObjectResult ok = new OkObjectResult(d);
            Console.WriteLine(JsonSerializer.Serialize(d));
            Console.WriteLine(JsonSerializer.Serialize((object)d));
            Console.WriteLine(JsonSerializer.Serialize(ok.Value));
            var o = new
            {
                list = new List<IDerived> { d }
            };
            OkObjectResult ok2 = new OkObjectResult(o);
            Console.WriteLine(JsonSerializer.Serialize(ok2.Value));
        }
    }
}
