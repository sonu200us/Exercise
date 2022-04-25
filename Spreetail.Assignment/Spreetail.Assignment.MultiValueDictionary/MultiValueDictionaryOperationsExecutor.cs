using Spreetail.Assignment.MultiValueDictionary.Repository;
using Spreetail.Assignment.MultiValueDictionary.Response;
using Spreetail.Assignment.MultiValueDictionary.Utility;
using System.Collections.Generic;
namespace Spreetail.Assignment.MultiValueDictionary
{
    /// <summary>
    /// execute all the commands and validate the input
    /// </summary>
    public class MultiValueDictionaryOperationsExecutor
    {
        /// <summary>
        /// multi value dictionary operation instance
        /// </summary>
        IMultiValueDictionaryOperations _dictionaryInstance;

        /// <summary>
        /// Initialize dictionary operation instance
        /// </summary>
        /// <param name="dictionaryInstance"></param>
        public MultiValueDictionaryOperationsExecutor(
            IMultiValueDictionaryOperations dictionaryInstance)
        {
            this._dictionaryInstance = dictionaryInstance;
        }

        /// <summary>
        /// Add new Key and Memeber
        /// validate the input and convert value into key and member
        /// </summary>
        /// <param name="value">new key and member value</param>
        /// <returns>Operation Response (Success/ Failure)</returns>
        public MultiValueDictionaryResponse<string> Add(string value)
        {
            string message = CommonFunctions.ValidateKeyMemeberExists(value);
            MultiValueDictionaryResponse<string> response = null;
            if (string.IsNullOrEmpty(message))
            {
                var pair = CommonFunctions.ExtractKeyMemberPair(value);
                return _dictionaryInstance.Add(pair.Key, pair.Value);
            }
            else
            {
                response = new MultiValueDictionaryResponse<string>();
                response.IsSuccess = false;
                response.Message = message;
            }
            return response;
        }

        /// <summary>
        /// remove the item from the dictionary
        /// validate the input and convert into key and member
        /// </summary>
        /// <param name="value">single stirng for key and member</param>
        /// <returns>result</returns>
        public MultiValueDictionaryResponse<string> Remove(string value)
        {
            string message = CommonFunctions.ValidateKeyMemeberExists(value);
            MultiValueDictionaryResponse<string> response = null;
            if (string.IsNullOrEmpty(message))
            {
                var pair = CommonFunctions.ExtractKeyMemberPair(value);
                return _dictionaryInstance.Remove(pair.Key, pair.Value);
            }
            else
            {
                response = new MultiValueDictionaryResponse<string>();
                response.IsSuccess = false;
                response.Message = message;
            }
            return response;
        }

        /// <summary>
        /// Remove all the members from the key
        /// </summary>
        /// <param name="key">key value</param>
        /// <returns></returns>
        public MultiValueDictionaryResponse<string> RemoveAll(string key)
        {
            return _dictionaryInstance.RemoveAll(key.Trim());
        }

        /// <summary>
        /// validate the input request
        /// check if the key and memeber exists in dictionary
        /// </summary>
        /// <param name="value">key and member</param>
        /// <returns></returns>
        public MultiValueDictionaryResponse<bool> MemberExists(string value)
        {
            string message = CommonFunctions.ValidateKeyMemeberExists(value);
            MultiValueDictionaryResponse<bool> response = null;
            if (string.IsNullOrEmpty(message))
            {
                var pair = CommonFunctions.ExtractKeyMemberPair(value);
                return _dictionaryInstance.MemberExists(pair.Key, pair.Value);
            }
            else
            {
                response = new MultiValueDictionaryResponse<bool>();
                response.IsSuccess = false;
                response.Message = message;
            }
            return response;
        }

        /// <summary>
        /// check if the key exists
        /// </summary>
        /// <param name="value">key and member</param>
        /// <returns></returns>
        public MultiValueDictionaryResponse<bool> KeyExists(string key)
        {
            return _dictionaryInstance.KeyExists(key.Trim());
        }

        /// <summary>
        /// validate the input and return all the members by key
        /// </summary>
        /// <param name="key">key value</param>
        /// <returns>List of memebers</returns>
        public MultiValueDictionaryResponse<IEnumerable<string>> Members(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                var response = new MultiValueDictionaryResponse<IEnumerable<string>>();
                response.IsSuccess = false;
                response.Message = ResponseMessages.EmptyString;
                return response;
            }
            return _dictionaryInstance.Members(key.Trim());
        }

        /// <summary>
        /// Return all the members under all the keys
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResponse<IEnumerable<string>> AllMembers()
        {
            return _dictionaryInstance.AllMembers();
        }

        /// <summary>
        /// return all the items keys and members concatinated
        /// </summary>
        /// <returns>list of members and keys</returns>
        public MultiValueDictionaryResponse<IEnumerable<string>> Items()
        {
            return _dictionaryInstance.Items();
        }

        /// <summary>
        /// Remove all the keys and members
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResponse<string> Clear()
        {
            return _dictionaryInstance.Clear();
        }

        /// <summary>
        /// Return all the keys
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResponse<IEnumerable<string>> Keys()
        {
            return _dictionaryInstance.Keys();
        }
    }
}
