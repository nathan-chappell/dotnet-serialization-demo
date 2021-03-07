using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
#nullable enable

namespace cons
{
    class Program
    {
        static void PrintProperties<T>()
        {
            for (Type? t = typeof(T); t != null; t = t.BaseType)
            {
                const BindingFlags bindingFlags =
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.DeclaredOnly;

                Console.WriteLine($"Printing properties for {t.ToString()}");
                Console.WriteLine(String.Join(", ", t.GetProperties(bindingFlags).Select(p => p.ToString())));
            }
        }
        static void Main(string[] args)
        {
            // A quick demonstration of how properties are enumerated in JsonSerialization
            PrintProperties<Derived>();
            PrintProperties<IDerived>();
            //
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            IDerived d = new Derived("a", "b");
            // Because the type of d is IDerived, only the public properties of 
            // IDerived will be serialized
            Console.WriteLine(JsonSerializer.Serialize(d));
            // You can provide a type option to the serializer.
            // Note that GetType return the runtime-type of the object!
            Console.WriteLine(JsonSerializer.Serialize(d, d.GetType()));
            // Type of `object` is treated as a special case, and will retrieve
            // the runtime-type of the object 
            Console.WriteLine(JsonSerializer.Serialize((object)d));

            // Here are demonstrations with ok.Value.
            // the OkObjectResult has a property Value, which gets serialized.
            // OkObjectResult.Value has type object, so when it gets serialized,
            // its runtime type will be retrieved.
            OkObjectResult ok = new OkObjectResult(d);
            Console.WriteLine(JsonSerializer.Serialize(ok.Value));
            // The runtime type is only retrieved for the "top level" type when
            // using OkObject.Value, so here only the properties listed on
            // IDerived will be part of the list
            var o = new
            {
                list = new List<IDerived> { d }
            };
            OkObjectResult ok2 = new OkObjectResult(o);
            Console.WriteLine(JsonSerializer.Serialize(ok2.Value));
        }
    }
}
