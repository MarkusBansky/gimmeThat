namespace gimmeThat
{
    public static class GimmeFunctions
    {
        public delegate void DoThen(string uri, RequestType type, string result);
        public delegate void DoThen<in T>(string uri, RequestType type, T result);
    }
}