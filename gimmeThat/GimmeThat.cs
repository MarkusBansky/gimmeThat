using System;
using System.IO;
using System.Net;

namespace gimmeThat
{
    public class GimmeThat
    {
        private string _uri;
        private RequestType _type;

        private Exception _exception;
        private string _innerText;

        private string _response;
        
        public string Response
        {
            get { return _response; }
        }

        public GimmeThat Get(string uri)
        {
            _uri = uri;
            _type = RequestType.GET;

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            try
            {
                using (var response = (HttpWebResponse) request.GetResponse())
                {
                    try
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            if (stream == null)
                            {
                                _exception = new Exception("Response stream was equal to null.");
                                _innerText = "Stream is null.";
                            }
                            else
                            {
                                try
                                {
                                    using (var reader = new StreamReader(stream))
                                    {
                                        try
                                        {
                                            _response = reader.ReadToEnd();
                                        }
                                        catch (Exception ex)
                                        {
                                            _exception = ex;
                                            _innerText = response.StatusDescription;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _exception = ex;
                                    _innerText = "";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _exception = ex;
                        _innerText = response.StatusDescription;
                    }
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
                _innerText = "";
            }

            return this;
        }

        public GimmeThat Then(GimmeFunctions.DoThen func)
        {
            func.Invoke(_uri, _type, Response);
            return this;
        }

        public GimmeThat ThenAs<T>(GimmeFunctions.DoThen<T> func) where T : IGimmeResponse<T>, new()
        {
            try
            {
                var converter = new T();
                func.Invoke(_uri, _type, converter.ConvertTo(Response));
            }
            catch (Exception ex)
            {
                _exception = ex;
                _innerText = "Please check the conversion method.";
            }

            return this;
        }
    }
}
