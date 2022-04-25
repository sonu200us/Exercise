using System;

namespace Spreetail.Assignment.MultiValueDictionary.Response
{
    /// <summary>
    /// holds the reponse for multi value dictionary
    /// </summary>
    /// <typeparam name="T">type of inner response</typeparam>
    public class MultiValueDictionaryResponse<T>
    {
        /// <summary>
        /// Initiaize succes and message
        /// </summary>
        public MultiValueDictionaryResponse()
        {
            IsSuccess = true;
            Message = string.Empty;
        }

        /// <summary>
        ///generic inner response based on operation type
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// true / false
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Exception/error message
        /// </summary>
        public string Message { get; set; }

    }
}
