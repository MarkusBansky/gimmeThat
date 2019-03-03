using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using gimmeThat.OP;

namespace gimmeThat
{
    public class GimmeThat
    {
        /// <summary>
        /// Contains all previous and the latest operations been made in the sequence
        /// </summary>
        private readonly List<Operation> _operations;
        
        /// <summary>
        /// Retrieves a response message in RAW string format. 
        /// </summary>
        public string Response => _operations.LastOrDefault()?.ResponseText;
        
        /// <summary>
        /// If the last call had any Exceptions then this will be true.
        /// </summary>
        public bool HasError => _operations.LastOrDefault()?.Exception != null;

        private Operation LatestOperation => _operations.LastOrDefault();

        /// <summary>
        /// Constructor. Used to initialize all required fields for this class to work.
        /// </summary>
        public GimmeThat()
        {
            _operations = new List<Operation>();
        }

        /// <summary>
        /// Performs a GET request to a url, then if success then sets all values from response and if any errors they
        /// are caught and 
        /// </summary>
        /// <param name="uri">Represent a URL that this call is made to.</param>
        /// <returns>Self, with changed values and a callback.</returns>
        public GimmeThat Get(string uri)
        {
            var operation = new Operation {Type = RequestType.GET, Url = uri};
            GimmeOperations.MakeGetCall(ref operation);
            _operations.Add(operation);

            return this;
        }

        public GimmeThat Then(GimmeFunctions.DoThen func)
        {
            if (LatestOperation == null) 
                return this;
            
            func.Invoke(LatestOperation.Url, LatestOperation.Type, Response);
            return this;
        }

        public GimmeThat ThenAs<T>(GimmeFunctions.DoThen<T> func) where T : IGimmeResponse<T>, new()
        {
            if (LatestOperation == null) 
                return this;
            
            try
            {
                var converter = new T();
                func.Invoke(LatestOperation.Url, LatestOperation.Type, converter.ConvertTo(Response));
            }
            catch (Exception ex)
            {
                LatestOperation.Exception = ex;
            }

            return this;
        }
    }
}
