namespace WebChat.Services.Interfaces
{
    public interface IEntityConverter
    {
        TOut Convert<TIn, TOut>(TIn value);
        string ConvertToJson<T>(T value);
        T ConvertFromJson<T>(string jsonString);
    }
}
