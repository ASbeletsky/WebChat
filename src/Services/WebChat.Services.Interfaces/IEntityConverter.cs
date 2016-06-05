namespace WebChat.Services.Interfaces
{
    public interface IEntityConverter
    {
        TOut Convert<TIn, TOut>(TIn value);
        TOut ConvertToExisting<TIn, TOut>(TIn value, TOut existing);
        string ConvertToJson<T>(T value);
        T ConvertFromJson<T>(string jsonString);
    }
}
