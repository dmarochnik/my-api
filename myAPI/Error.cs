using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myAPI
{
    public class Error
    {
        public int ErrorCode { get; }

        public string[] Messages { get; }

        //Where is string message coming from? It isn't a property of this class.
        //returns one error code and one message
        public Error(int ec, string message)
        {
            this.ErrorCode = ec;
            this.Messages = new string[] { message };
        }

        //in case of multiple errors at once, this will return all error codes and messages
        public Error(int ec, string[] messages)
        {
            this.ErrorCode = ec;
            this.Messages = messages;
        }
    }

    //this class is responsible for making and logging new errors. It's static because it's never being instantiated.
    public static class ErrorHandler
    {
        public static Error OnError(string message, int statusCode, ILogger logger)
        {
            //Why am I writing "Error occurred: {0}" twice, once here and once in the logincontroller? Redundant.
            //Where is LogError being defined? Is it a built in method? Does it work because logger is a built in type?
            logger.LogError("Error occurred: {0}", message);
            //What does this mean? Return new error? Where is this returning to?
            //Wheere is statuscode coming from? I see its getting passed in but I don't know why or from where?
            return new Error(statusCode, message);
        }

        public static Error OnError(string message, int statusCode,  ILogger logger, Exception ex)
        {
            logger.LogError("Error occurred {0} with exception: {1}", message, ex.Message);
            return new Error(statusCode, message);
        }

        public static Error OnError(string[] message, int statusCode, ILogger logger)
        {
            logger.LogError("Errors occurred: {0}", string.Join(',', message));
            return new Error(statusCode, message);
        }
    }
}
