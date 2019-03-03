namespace gimmeThat
{
    public interface IGimmeResponse<out T>
    {
        T ConvertTo(string response);
    }
}