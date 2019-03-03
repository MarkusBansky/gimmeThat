using System;
using System.IO;
using System.Net;
using gimmeThat.OP;

namespace gimmeThat
{
    public static class GimmeOperations
    {
        public static void MakeGetCall(ref Operation op)
        {
            try
            {
                var r = (HttpWebRequest) WebRequest.Create(op.Url);
                r.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                CreateWebResponse(ref op, r);
            }
            catch (Exception ex)
            {
                op.Exception = ex;
            }
        }
        
        private static void CreateWebResponse(ref Operation op, HttpWebRequest r)
        {
            try
            {
                using (var s = (HttpWebResponse) r.GetResponse())
                {
                    CreateStream(ref op, s);
                }
            }
            catch (Exception ex)
            {
                op.Exception = ex;
            }
        }
        
        private static void CreateStream(ref Operation op, HttpWebResponse r)
        {
            try
            {
                using (var s = r.GetResponseStream())
                {
                    CreateStreamReader(ref op, s);
                }
            }
            catch (Exception ex)
            {
                op.Exception = ex;
            }
        }
        
        private static void CreateStreamReader(ref Operation op, Stream s)
        {
            if (s == null)
            {
                op.Exception = new Exception("Response stream was equal to null.");
                return;
            }

            try
            {
                using (var r = new StreamReader(s))
                {
                    ReadResponse(ref op, r);
                }
            }
            catch (Exception ex)
            {
                op.Exception = ex;
            }
        }
        
        private static void ReadResponse(ref Operation op, StreamReader sr)
        {
            try
            {
                op.ResponseText = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                op.Exception = ex;
            }
        }
    }
}