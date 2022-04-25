using Spreetail.Assignment.MultiValueDictionary.Response;
using Spreetail.Assignment.MultiValueDictionary.Utility;
using System;
using System.Collections.Generic;

namespace Spreetail.Assignment.MultiValueDictionary
{
    /// <summary>
    /// Identify and process the commands
    /// </summary>
    public class CommandProcessor
    {
        /// <summary>
        /// private instance of multi value dictionary business
        /// </summary>
        MultiValueDictionaryOperationsExecutor operationExec;

        /// <summary>
        /// Initialize private instance of multi value dictionary business
        /// </summary>
        /// <param name="operationExec"></param>
        public CommandProcessor(MultiValueDictionaryOperationsExecutor operationExec)
        {
            this.operationExec = operationExec;
        }

        /// <summary>
        /// Run the command
        /// </summary>
        /// <param name="commandText"></param>
        public string Run(string commandText)
        {
            try
            {
                commandText = commandText.Trim();
                string message = CommonFunctions.ValidateCommandText(commandText);

                if (string.IsNullOrEmpty(message))
                {
                    string operation = CommonFunctions.GetCommandText(commandText);
                    MultivalueDictionaryCommands command;
                    var isParsed = Enum.TryParse(operation, true, out command);
                    if (!isParsed)
                    {
                        command = MultivalueDictionaryCommands.HELP;
                    }
                    switch (command)
                    {
                        case MultivalueDictionaryCommands.ADD:
                            return ProcessResponse.Print<string>(
                                   operationExec.Add(
                                       commandText.ToLower().TrimCommandText(operation)));
                        case MultivalueDictionaryCommands.KEYS:
                            return ProcessResponse.Print(operationExec.Keys());
                        case MultivalueDictionaryCommands.MEMBEREXISTS:
                            return ProcessResponse.Print<bool>(
                                 operationExec.MemberExists(
                                     commandText.ToLower().TrimCommandText(operation)));
                        case MultivalueDictionaryCommands.ALLMEMBERS:
                            return ProcessResponse.Print(operationExec.AllMembers());
                        case MultivalueDictionaryCommands.KEYEXISTS:
                            return ProcessResponse.Print<bool>(
                               operationExec.KeyExists(
                                   commandText.ToLower().TrimCommandText(operation)));
                        case MultivalueDictionaryCommands.CLEAR:
                            return ProcessResponse.Print<string>(
                                 operationExec.Clear());
                        case MultivalueDictionaryCommands.ITEMS:
                            return ProcessResponse.Print(operationExec.Items());
                        case MultivalueDictionaryCommands.MEMBERS:
                            return ProcessResponse.Print(operationExec.Members(
                                   commandText.ToLower().TrimCommandText(operation)));
                        case MultivalueDictionaryCommands.REMOVE:
                            return ProcessResponse.Print<string>(operationExec.Remove(
                                   commandText.ToLower().TrimCommandText(operation)));
                        case MultivalueDictionaryCommands.REMOVEALL:
                            return ProcessResponse.Print<string>(
                                 operationExec.RemoveAll(
                                     commandText.ToLower().TrimCommandText(operation)));
                        case MultivalueDictionaryCommands.CLEAN:
                            Console.Clear();
                            return string.Empty;
                        case MultivalueDictionaryCommands.EXIT:
                            Environment.Exit(0);
                            return string.Empty;
                        case MultivalueDictionaryCommands.HELP:
                        default:
                            Console.WriteLine("Supported Commands");
                            string commands = string.Empty;
                            foreach (string items in Enum.GetNames(typeof(MultivalueDictionaryCommands)))
                            {
                                Console.WriteLine(items);
                                commands += string.Concat(items, " ");
                            }
                            return commands;
                    }
                }
                else
                {
                    var response = new MultiValueDictionaryResponse<string>();
                    response.IsSuccess = false;
                    response.Message = message;
                    return ProcessResponse.Print(response);
                }
            }
            catch (Exception ex)
            {
                var response = new MultiValueDictionaryResponse<string>();
                response.IsSuccess = false;
                response.Message = string.Concat(ResponseMessages.ExceptionMessage, " ", ex.Message);
                return ProcessResponse.Print(response);
            }
        }
    }
}
