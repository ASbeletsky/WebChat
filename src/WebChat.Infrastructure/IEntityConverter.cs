namespace WebChat.Infrastructure
{
    public interface IEntityConverter
    {
        TOut ConvertTo<TIn, TOut>(TIn value);
        string ConvertToJson<T>(T value);
        T ConvertFromJson<T>(string jsonString);
    }
}
