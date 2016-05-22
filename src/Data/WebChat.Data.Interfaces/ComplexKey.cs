namespace WebChat.Domain.Interfaces
{
    public interface IComplexKey<KeyType1, KeyType2>
    {
        KeyType1 Key1
        {
            get;
            set;
        }
        KeyType2 Key2
        {
            get;
            set;
        }
    }
}
