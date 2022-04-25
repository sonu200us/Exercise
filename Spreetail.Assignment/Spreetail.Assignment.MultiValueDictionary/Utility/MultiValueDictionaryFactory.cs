using Spreetail.Assignment.MultiValueDictionary.Repository;
using System.Collections.Generic;

namespace Spreetail.Assignment.MultiValueDictionary.Utility
{
    /// <summary>
    /// defines the multi value dictionary factor for creating instance
    /// </summary>
    public static class MultiValueDictionaryFactory
    {
        /// <summary>
        /// instance of data store (injecting behaviour)
        /// </summary>
        public static Dictionary<string, List<string>> db;

        /// <summary>
        /// create instance for multi value dictionary
        /// </summary>
        /// <returns>MultiValueDictionaryOperations instance</returns>
        public static IMultiValueDictionaryOperations MultiValueDictionaryInstance
        {
            get
            {
                if (db is null)
                {
                    db = new Dictionary<string, List<string>>();
                }
                return new MultiValueDictionaryOperations(db);
            }
        }
    }
}
