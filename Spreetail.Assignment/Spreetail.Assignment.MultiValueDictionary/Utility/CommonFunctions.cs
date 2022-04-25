using Spreetail.Assignment.MultiValueDictionary.Response;
using System.Collections.Generic;
namespace Spreetail.Assignment.MultiValueDictionary.Utility
{
    /// <summary>
    /// Utility validation class for multi value dictionary
    /// </summary>
    static class CommonFunctions
    {
        /// <summary>
        /// Validate command 
        /// enforce character limit
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ValidateCommandText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return ResponseMessages.EmptyString;
            }

            if (value.Length > 150)
            {
                return ResponseMessages.CharacterLength;
            }
            return string.Empty;
        }

        /// <summary>
        /// return the first word from string by splitting with empty space
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetCommandText(string value)
        {
            return value.Split(" ")[0].ToLower();
        }

        /// <summary>
        /// validate reqest 
        /// check key member existance
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ValidateKeyMemeberExists(string value)
        {
            value = value.Trim();
            if (string.IsNullOrWhiteSpace(value))
            {
                return ResponseMessages.EmptyString;
            }

            if (value.Split(" ").Length < 2)
            {
                return ResponseMessages.MemeberInfoMissing;
            }

            return string.Empty;
        }

        /// <summary>
        /// convert input string into key member pair
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static KeyValuePair<string, string> ExtractKeyMemberPair(string value)
        {
            value = value.Trim();
            return KeyValuePair.Create(value.Split(" ")[0],
                    value.Substring(value.IndexOf(" ")).Trim());
        }

        /// <summary>
        /// Trim the command text from the request
        /// </summary>
        /// <param name="requestText"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static string TrimCommandText(this string requestText, string operation)
        {
            return requestText.Substring(operation.Length).Trim();
        }
    }
}
