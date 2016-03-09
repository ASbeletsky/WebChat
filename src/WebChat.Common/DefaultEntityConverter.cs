namespace WebChat.Common
{
    #region Using

    using Newtonsoft.Json;
    using AutoMapper;
    using WebChat.Infrastructure;
    using System;

    #endregion

    public class DefaultEntityConverter : IEntityConverter
    {
        private static readonly DefaultEntityConverter _instance;
        private DefaultEntityConverter()
        {
            InitializeMappings();
        }
        static DefaultEntityConverter()
        {
            _instance = new DefaultEntityConverter();           
        }

        public static DefaultEntityConverter Instance
        {
            get
            {
                return _instance;
            }
        }

        public T ConvertFromJson<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string ConvertToJson<T>(T value)
        {
            return JsonConvert.SerializeObject(value, typeof(T), null);
        }

        public TOut ConvertTo<TIn, TOut>(TIn value)
        {
            return Mapper.Map<TIn, TOut>(value);
        }

        private void InitializeMappings()
        {
            throw new NotImplementedException();
        }
    }
}
