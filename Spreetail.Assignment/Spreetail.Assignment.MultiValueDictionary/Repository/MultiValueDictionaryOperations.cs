using Spreetail.Assignment.MultiValueDictionary.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Assignment.MultiValueDictionary.Repository
{
    /// <summary>
    /// Implementation of multi value dictionary operations
    /// </summary>
    class MultiValueDictionaryOperations : IMultiValueDictionaryOperations
    {
        /// <summary>
        /// Private instance of multi value dictionary
        /// </summary>
        Dictionary<string, List<string>> multiValueDictionary;

        /// <summary>
        /// Intitialize data store with class object
        /// </summary>
        /// <param name="multiValueDictionary">data store</param>
        public MultiValueDictionaryOperations(Dictionary<string, List<string>> multiValueDictionary)
        {
            this.multiValueDictionary = multiValueDictionary; ;
        }

        /// <summary>
        /// Adds a member to a collection for a given key. Displays an error if the member already exists for the key.
        /// </summary>
        /// <param name="key">new key value</param>
        /// <param name="value">new member value</param>
        /// <returns>Operation Response (Success/ Failure)</returns>
        public MultiValueDictionaryResponse<string> Add(string key, string member)
        {
            var response = new MultiValueDictionaryResponse<string>();
            if (!KeyExists(key).Result)
            {
                this.multiValueDictionary.Add(key,
                    new List<string>() { member }
                    );
                response.Result = ResponseMessages.KeyAdded;
            }
            else
            {
                if (!this.multiValueDictionary[key].Contains(member))
                {
                    this.multiValueDictionary[key].Add(member);
                    response.Result = ResponseMessages.KeyAdded;
                }
                else
                {
                    response.Result = ResponseMessages.MemeberAlreadyExist;
                }
            }

            return response;
        }

        /// <summary>
        /// Removes a member from a key.  If the last member is removed from the key, the key is removed from the dictionary. If the key or member does not exist, displays an error.
        /// </summary>
        /// <param name="key">key to remove</param>
        /// <param name="member">memeber to remove</param>
        /// <returns>Operation Response (Success/ Failure)</returns>
        public MultiValueDictionaryResponse<string> Remove(string key, string member)
        {
            var response = new MultiValueDictionaryResponse<string>();

            if (this.multiValueDictionary.ContainsKey(key))
            {
                var res = this.multiValueDictionary[key];
                if (res.Contains(member))
                {
                    if (res.Count == 1)
                    {
                        this.multiValueDictionary.Remove(key);
                    }
                    else
                    {
                        res.Remove(member);
                    }
                    response.Result = ResponseMessages.KeyRemoved;
                    return response;
                }
            }
            response.IsSuccess = false;
            response.Message = ResponseMessages.InvalidMemeber;
            return response;
        }

        /// <summary>
        /// Removes all members for a key and removes the key from the dictionary. Returns an error if the key does not exist.
        /// </summary>
        /// <param name="key">key value</param>
        /// <returns>Operation Response (Success/ Failure)</returns>
        public MultiValueDictionaryResponse<string> RemoveAll(string key)
        {
            var response = new MultiValueDictionaryResponse<string>();

            if (this.multiValueDictionary.ContainsKey(key))
            {
                this.multiValueDictionary.Remove(key);
                response.Result = ResponseMessages.KeyRemoved;
                return response;
            }
            response.IsSuccess = false;
            response.Message = ResponseMessages.EmptySet;
            return response;
        }

        /// <summary>
        /// Removes all keys and all members from the dictionary.
        /// </summary>
        /// <returns>Operation Response (Success/ Failure)</returns>
        public MultiValueDictionaryResponse<string> Clear()
        {
            var response = new MultiValueDictionaryResponse<string>();
            this.multiValueDictionary.Clear();
            response.Result = ResponseMessages.Cleared;
            return response;

        }

        /// <summary>
        /// Returns all the keys in the dictionary.  Order is not guaranteed.
        /// </summary>
        /// <returns>List of string (Keys)</returns>
        public MultiValueDictionaryResponse<IEnumerable<string>> Keys()
        {
            var response = new MultiValueDictionaryResponse<IEnumerable<string>>();
            response.Result = this.multiValueDictionary.Keys;
            return response;
        }

        /// <summary>
        /// Returns whether a key exists or not.
        /// </summary>
        /// <param name="key">value to check</param>
        /// <returns>if key exists trun otherwise false</returns>
        public MultiValueDictionaryResponse<bool> KeyExists(string key)
        {
            MultiValueDictionaryResponse<bool> response = new MultiValueDictionaryResponse<bool>();
            response.Result = this.multiValueDictionary.ContainsKey(key);
            return response;
        }

        /// <summary>
        /// Returns all the members in the dictionary.  Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        /// <returns>List of members</returns>
        public MultiValueDictionaryResponse<IEnumerable<string>> AllMembers()
        {
            var response = new MultiValueDictionaryResponse<IEnumerable<string>>();
            var members = new List<string>();
            foreach (var key in this.multiValueDictionary.Keys)
            {
                members.AddRange(this.multiValueDictionary[key]);
            }
            response.Result = members.ToArray();
            return response;
        }

        /// <summary>
        /// Returns the collection of strings for the given key.  Return order is not guaranteed.  Returns an error if the key does not exists.
        /// </summary>
        /// <param name="key">Name of key</param>
        /// <returns>List of stirng (Members)</returns>
        public MultiValueDictionaryResponse<IEnumerable<string>> Members(string key)
        {
            var response = new MultiValueDictionaryResponse<IEnumerable<string>>();
            var members = new List<string>();
            if (this.multiValueDictionary.ContainsKey(key))
            {
                members.AddRange(this.multiValueDictionary[key]);
                response.Result = members.ToArray();
            }
            return response;

        }

        /// <summary>
        /// Returns whether a member exists within a key.  Returns false if the key does not exist.
        /// </summary>
        /// <param name="key">value to check</param>
        /// <param name="member">value to check</param>
        /// <returns>if key exists trun otherwise false</returns>
        public MultiValueDictionaryResponse<bool> MemberExists(string key, string member)
        {
            var response = new MultiValueDictionaryResponse<bool>();
            if (this.multiValueDictionary.ContainsKey(key))
            {
                response.Result = this.multiValueDictionary[key].Contains(member);
            }
            return response;
        }

        /// <summary>
        /// Returns all keys in the dictionary and all of their members.  Returns nothing if there are none.  Order is not guaranteed.
        /// </summary>
        /// <returns>List of keys and members</returns>
        public MultiValueDictionaryResponse<IEnumerable<string>> Items()
        {
            var response = new MultiValueDictionaryResponse<IEnumerable<string>>();
            var members = new List<string>();
            foreach (var key in this.multiValueDictionary.Keys)
            {
                foreach (var item in this.multiValueDictionary[key])
                    members.Add(string.Concat(key, " : ", item));
            }
            response.Result = members.ToArray();
            return response;
        }

    }
}
