using System;
using System.Security.AccessControl;

namespace gimmeThat.OP
{
    public class Operation
    {
        /// <summary>
        /// The URL to the request that has been made.
        /// </summary>
        public string Url;
        
        /// <summary>
        /// Type of this request.
        /// </summary>
        public RequestType Type;

        /// <summary>
        /// Text from the response read by the reader.
        /// </summary>
        public string ResponseText;

        /// <summary>
        /// If any exception was caught then this is set to it.
        /// </summary>
        public Exception Exception;
    }
}