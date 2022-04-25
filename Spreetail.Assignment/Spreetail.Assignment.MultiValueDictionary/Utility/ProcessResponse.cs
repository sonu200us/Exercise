using Spreetail.Assignment.MultiValueDictionary.Response;
using System;
using System.Collections.Generic;

namespace Spreetail.Assignment.MultiValueDictionary.Utility
{
    /// <summary>
    /// handle response messages from the inner classes
    /// </summary>
    public static class ProcessResponse
    {
        /// <summary>
        /// print bool and string type responses
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        public static string Print<T>(MultiValueDictionaryResponse<T> response)
        {
            string message = string.Empty;
            if (response.IsSuccess)
            {
                message = response.Result.ToString();
                Console.Write(response.Result);
            }
            else
            {
                message = response.Message;
                Console.Write(response.Message);
            }
            return message;
        }

        /// <summary>
        /// print enumberable type response
        /// </summary>
        /// <param name="response"></param>
        public static string Print(MultiValueDictionaryResponse<IEnumerable<string>> response)
        {
            int index = 1;
            string message = string.Empty;
            if (response.IsSuccess)
            {
                if (response.Result != null)
                {
                    foreach (string s in response.Result)
                    {
                        Console.WriteLine(string.Concat(index.ToString(), ") ", s));
                        index++;
                    }
                }
                if (index == 1)
                {
                    message = ResponseMessages.EmptySet;
                    Console.Write(ResponseMessages.EmptySet);
                }
            }
            else
            {
                message = response.Message;
                Console.Write(response.Message);
            }
            return message;
        }
    }
}
