using NUnit.Framework;
using Spreetail.Assignment.MultiValueDictionary;
using Spreetail.Assignment.MultiValueDictionary.Response;
using Spreetail.Assignment.MultiValueDictionary.Utility;
using System.Collections.Generic;

namespace Spreetail.Assignent.MultiValueDictionaryTests
{
    public class MultiValueDictionaryOperationsTests
    {
        Dictionary<string, List<string>> db = null;
        [SetUp]
        public void Setup()
        {
                db = new Dictionary<string, List<string>>();
                db.Add("foo", new List<string>
                {
                    "bar",
                    "baz",
                    "bal"
                });
            MultiValueDictionaryFactory.db = db;
        }

        [Test]
        public void AddMeber_Test()
        {
            var operationExecutor =
                 new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            commandProcessor.Run("add address Allentown");
            Assert.AreEqual(db.Keys.Count, 2);
            Assert.IsTrue(db.ContainsKey("address"));
        }

        [Test]
        public void AddMeber_Duplicate_Test()
        {
            var operationExecutor =
                 new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            string message = commandProcessor.Run("add foo bar");
            Assert.AreEqual(message, ResponseMessages.MemeberAlreadyExist);

        }

        [Test]
        public void GetAllKeys_Test()
        {
            var operationExecutor =
                 new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            commandProcessor.Run("add Address Allentown");
            commandProcessor.Run("add Home Allentown");
            Assert.AreEqual(db.Keys.Count, 3);
            commandProcessor.Run("REMOVEALL foo");
            commandProcessor.Run("REMOVEALL Address");
            commandProcessor.Run("REMOVEALL Home");
            var message = commandProcessor.Run("keys");
            Assert.AreEqual(message, ResponseMessages.EmptySet);
        }


        [Test]
        public void MemeberExists_Test()
        {
            var operationExecutor =
                new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);

            Assert.AreEqual(operationExecutor.MemberExists("foo bal").Result, true);
        }

        [Test]
        public void GetMemebers_Test()
        {
            var operationExecutor =
                new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            int count = 0;
            foreach (var item in operationExecutor.Members("foo").Result)
            {
                count++;
            }
            Assert.AreEqual(count, 3);
        }


        [Test]
        public void Remove_Tests()
        {
            var operationExecutor =
                new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            Assert.AreEqual(db["foo"].Count, 3);
            commandProcessor.Run("remove foo bar");
            Assert.AreEqual(db["foo"].Count, 2);
            var message = commandProcessor.Run("remove foo bar");
            Assert.AreEqual(message, ResponseMessages.InvalidMemeber);

        }

        [Test]
        public void Remove_Clear_Test()
        {
            var operationExecutor =
                new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            Assert.AreEqual(db["foo"].Count, 3);
            commandProcessor.Run("clear");
            Assert.AreEqual(db.Keys.Count, 0);
            var message = commandProcessor.Run("remove foo bar");
            Assert.AreEqual(message, ResponseMessages.InvalidMemeber);

        }

        [Test]
        public void KeyExists_Test()
        {
            var operationExecutor =
                new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            var message = commandProcessor.Run("KeyExists foo");
            Assert.AreEqual(message.ToLower(), "true");
            message = commandProcessor.Run("KeyExists foox");
            Assert.AreEqual(message.ToLower(), "false");
        }

        #region Negative Scenarios
        [Test]
        public void AddMeber_ExtraSpace_lowerCase_UpperCase_Test()
        {
            var operationExecutor =
                 new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            commandProcessor.Run("  aDd    adDress AlleNtown");
            Assert.AreEqual(db.Keys.Count, 2);
            Assert.IsTrue(db.ContainsKey("address"));

            string message = commandProcessor.Run("  aDd    ADDRESS AllENtown");
            Assert.AreEqual(message, ResponseMessages.MemeberAlreadyExist);
        }

        [Test]
        public void WrongCommand_Test()
        {
            var operationExecutor =
                 new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
           var message = commandProcessor.Run("xyz hello");
            Assert.AreEqual(message, "ADD REMOVE REMOVEALL KEYS MEMBERS ALLMEMBERS CLEAR ITEMS KEYEXISTS MEMBEREXISTS CLEAN HELP EXIT ");
        }

        [Test]
        public void SpecialCharacter_Test()
        {
            var operationExecutor =
                 new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            var message = commandProcessor.Run("add he'll'o sing<>@#$%^&*()h'");
            Assert.AreEqual(db.Keys.Count, 2);
        }

        [Test]
        public void CharacterLength_Test()
        {
            var operationExecutor =
                 new MultiValueDictionaryOperationsExecutor(MultiValueDictionaryFactory.MultiValueDictionaryInstance);
            var commandProcessor = new CommandProcessor(operationExecutor);
            var message = commandProcessor.Run("add character length exceeded test done from the NUnit tests by gurwinder. 150 chacters are the limit for this key memeber, still need more characters limit not reached yet.");
            Assert.AreEqual(message, ResponseMessages.CharacterLength);
        }
        #endregion
    }
}