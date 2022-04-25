using Spreetail.Assignment.MultiValueDictionary.Response;
using System.Collections.Generic;

namespace Spreetail.Assignment.MultiValueDictionary.Repository
{
    /// <summary>
    /// defines contract for multi value dictionary operations
    /// </summary>
    public interface IMultiValueDictionaryOperations
    {
        /// <summary>
        /// Adds a member to a collection for a given key. Displays an error if the member already exists for the key.
        /// </summary>
        /// <param name="key">new key value</param>
        /// <param name="value">new member value</param>
        /// <returns>Operation Response (Success/ Failure)</returns>
        MultiValueDictionaryResponse<string> Add(string key, string member);

        /// <summary>
        /// Removes a member from a key.  If the last member is removed from the key, the key is removed from the dictionary. If the key or member does not exist, displays an error.
        /// </summary>
        /// <param name="key">key to remove</param>
        /// <param name="member">memeber to remove</param>
        /// <returns>Operation Response (Success/ Failure)</returns>
        MultiValueDictionaryResponse<string> Remove(string key, string member);

        /// <summary>
        /// Removes all members for a key and removes the key from the dictionary. Returns an error if the key does not exist.
        /// </summary>
        /// <param name="key">key value</param>
        /// <returns>Operation Response (Success/ Failure)</returns>
        MultiValueDictionaryResponse<string> RemoveAll(string key);

        /// <summary>
        /// Removes all keys and all members from the dictionary.
        /// </summary>
        /// <returns>Operation Response (Success/ Failure)</returns>
        MultiValueDictionaryResponse<string> Clear();

        /// <summary>
        /// Returns all the keys in the dictionary.  Order is not guaranteed.
        /// </summary>
        /// <returns>List of string (Keys)</returns>
        MultiValueDictionaryResponse<IEnumerable<string>> Keys();

        /// <summary>
        /// Returns whether a key exists or not.
        /// </summary>
        /// <param name="key">value to check</param>
        /// <returns>if key exists trun otherwise false</returns>
        MultiValueDictionaryResponse<bool> KeyExists(string key);

        /// <summary>
        /// Returns all the members in the dictionary.  Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        /// <returns>List of members</returns>
        MultiValueDictionaryResponse<IEnumerable<string>> AllMembers();

        /// <summary>
        /// Returns the collection of strings for the given key.  Return order is not guaranteed.  Returns an error if the key does not exists.
        /// </summary>
        /// <param name="key">Name of key</param>
        /// <returns>List of stirng (Members)</returns>
        MultiValueDictionaryResponse<IEnumerable<string>> Members(string key);

        /// <summary>
        /// Returns whether a member exists within a key.  Returns false if the key does not exist.
        /// </summary>
        /// <param name="key">value to check</param>
        /// <param name="member">value to check</param>
        /// <returns>if key exists trun otherwise false</returns>
        MultiValueDictionaryResponse<bool> MemberExists(string key, string member);

        /// <summary>
        /// Returns all keys in the dictionary and all of their members.  Returns nothing if there are none.  Order is not guaranteed.
        /// </summary>
        /// <returns>List of keys and members</returns>
        MultiValueDictionaryResponse<IEnumerable<string>> Items();

    }
}
