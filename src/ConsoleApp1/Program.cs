// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

Console.WriteLine("Hello, World!");

JsonObject obj = new JsonObject { Code = 1, Name = "json" };
IJsonObject jsonObject = obj;

JsonObject? nullObj = null;
Console.WriteLine(nullObj.JsonSerialize());
Console.WriteLine(obj.GetType().Name);
Console.WriteLine(obj.JsonSerialize());
Console.WriteLine(jsonObject.GetType().Name);
Console.WriteLine(jsonObject.JsonSerialize());

Console.ReadLine();