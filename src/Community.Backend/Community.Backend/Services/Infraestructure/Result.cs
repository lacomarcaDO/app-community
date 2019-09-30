using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Backend.Services.Infraestructure
{
    public class Result
    {
        /// <summary>
        /// Determine if a Task has been executed Succesfully
        /// </summary>
        public bool ExecutedSuccesfully { get; set; }

        /// <summary>
        /// List of all messages that were recorded while performing the task
        /// </summary>
        public IList<string> MessageList { get; private set; } = new List<string>();

        /// <summary>
        /// In case we have an exception performing our task
        /// </summary>
        public Exception Exception { get; set; }

        public Result()
        {
            ExecutedSuccesfully = true;
        }

        /// <summary>
        /// Summary of messages. This can include error messages (if ExecutedSuccesfully = false) or
        /// a result message (if ExecutedSuccesfully = true)
        /// </summary>
        public string Messages()
        {
            if (Exception != null)
            {
               AddErrorMessage($"Exception: {Exception.ToString()}");
            }
            return string.Join("\n \n", MessageList);
        }

        public Result AddErrorMessage(string errorMessage,Exception exception = null)
        {
            if(exception != null)
            {
                Exception = exception;
            }
            ExecutedSuccesfully = false;

            MessageList.Add(errorMessage);
            return this;
        }

        public Result AddMessage(string message)
        {
            MessageList.Add(message);
            return this;
        }

        public Result AddAllMessages(IList<string> messages)
        {
            MessageList = MessageList.Concat(messages).ToList();
            return this;
        }

        public Result AppendTaskResultData(Result result)
        {
            Exception = result.Exception;

            AddAllMessages(result.MessageList);

            if (ExecutedSuccesfully != false)
            {
                ExecutedSuccesfully = result.ExecutedSuccesfully;
            }
            return this;
        }
    }
}
