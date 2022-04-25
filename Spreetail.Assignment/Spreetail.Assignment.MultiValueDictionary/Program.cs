using Spreetail.Assignment.MultiValueDictionary.Utility;
using System;

namespace Spreetail.Assignment.MultiValueDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            //instance for business layer
            var operationExecutor =
                new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);

            //command processor class (identify and process the command)
            var commandProcessor = new CommandProcessor(operationExecutor);

            Console.WriteLine("Welcome to the Gurwinder Assignment");
            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                commandProcessor.Run(input);
                Console.WriteLine("");
            }
        }
    }
}
