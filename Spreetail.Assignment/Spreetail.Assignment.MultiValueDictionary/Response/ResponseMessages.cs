namespace Spreetail.Assignment.MultiValueDictionary.Response
{
    /// <summary>
    /// manage all the messages for dictionary operations
    /// </summary>
   public static class ResponseMessages
    {
        public const string EmptyString = ") KEY VALUE IS INCORRECT.";

        public const string MemeberInfoMissing = ") ERROR, Member information missing from the input";

        public const string CharacterLength = ") only 150 characters are allowed.";

        public const string KeyAdded = ") Added";

        public const string KeyRemoved = ") Removed";

        public const string  Cleared = ") Cleared";

        public const string InvalidKey = ") ERROR, key does not exist.";

        public const string InvalidMemeber = ") ERROR, memeber does not exist.";

        public const string MemeberAlreadyExist = ") ERROR, memeber already exists for key.";

        public const string EmptySet = "( empty set)";

        public const string ExceptionMessage = "ERROR, internal error occured. Kindly try valid command.";
    }
}
