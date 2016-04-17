namespace WebChat.Infrastructure.Services.Interfaces
{
    public interface IEntityConverter
    {
        TOut Convert<TIn, TOut>(TIn value);
        string ConvertToJson<T>(T value);
        T ConvertFromJson<T>(string jsonString);
        TOut MapToExisting<TIn, TOut>(TIn source, ref TOut existingTarget);
    }
}
