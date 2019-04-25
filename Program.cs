using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonPath
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || (args[0] == "-h" || args[0] == "--help" || args[0] == "-?"))
            {
                Help();
                return;
            }

            TextReader textReader = new StreamReader(Console.OpenStandardInput(65535));
            var inputJson = textReader.ReadToEnd();
            var input = (JToken)JsonConvert.DeserializeObject(inputJson);
            var tokens = input.SelectTokens(args[0]);
            
            foreach (var token in tokens)
            {
                if (token is JObject)
                {
                    var result = JsonConvert.SerializeObject(tokens, Newtonsoft.Json.Formatting.Indented);
                    Console.WriteLine(result);
                    return;
                }
            }

            foreach (var token in tokens)
            {
                Console.WriteLine(token.ToString());
            }
        }

        private static void Help()
        {
            Console.WriteLine("jsonpath [JSONPath expression]");
            Console.WriteLine("\nDescription:");
            Console.WriteLine("reads json from stdin and evaluates JSONPath expression against the json, outputting json\n");
            Console.WriteLine("Example:\ntype json.txt | jpath.csx [jsonpath]\n");
            Console.WriteLine(@"Cheatsheet data taken from https://goessner.net/articles/JsonPath/
|--------------------------------------------------------------------------------------------------------------|
| XPath                | JSONPath               | Description                                                  |
|----------------------|------------------------|--------------------------------------------------------------|
| /store/book/author   | $.store.book[*].author | the authors of all books in the store                        |
|----------------------|------------------------|--------------------------------------------------------------|
| //author             | $..author              | all authors                                                  |
|----------------------|------------------------|--------------------------------------------------------------|
| /store/*             | $.store.*              | all things in store, which are some books and a red bicycle. |
|----------------------|------------------------|--------------------------------------------------------------|
| /store//price        | $.store..price         | the price of everything in the store.                        |
|----------------------|------------------------|--------------------------------------------------------------|
| //book[3]            | $..book[2]             | the third book                                               |
|----------------------|------------------------|--------------------------------------------------------------|
| //book[last()]       | $..book[(@.length-1)]  | the last book in order.                                      |
|----------------------|------------------------|--------------------------------------------------------------|
|                      | $..book[-1:]           |                                                              |
|----------------------|------------------------|--------------------------------------------------------------|
| //book[position()<3] | $..book[0,1]           | the first two books                                          |
|----------------------|------------------------|--------------------------------------------------------------|
|                      | $..book[:2]            |                                                              |
|----------------------|------------------------|--------------------------------------------------------------|
| //book[isbn]         | $..book[?(@.isbn)]     | filter all books with isbn number                            |
|----------------------|------------------------|--------------------------------------------------------------|
| //book[price<10]     | $..book[?(@.price<10)] | filter all books cheapier than 10                            |
|----------------------|------------------------|--------------------------------------------------------------|
| //*                  | $..*                   | all Elements in XML document. All members of JSON structure. |
|--------------------------------------------------------------------------------------------------------------|
");
            return;
        }
    }
}
