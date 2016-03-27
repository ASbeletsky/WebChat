namespace WebChat.Services.Common
{
    #region Using

    using Newtonsoft.Json;
    using AutoMapper;
    using System.Linq;
    using Services.Interfaces;
    using Domain.Core.Identity;
    using Business.Core.Identity;
    using WebChat.Domain.Data;
    using Business.Core.Customer;
    using Domain.Core.Application;
    #endregion

    public class EntityConverter : IEntityConverter
    {
        #region Private Members

        private IMapper mapper;

        #endregion


        public EntityConverter()
        {
            InitializeMappings();
        }


        public T ConvertFromJson<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string ConvertToJson<T>(T value)
        {
            return JsonConvert.SerializeObject(value, typeof(T), null);
        }

        public TOut Convert<TIn, TOut>(TIn value)
        {
            return mapper.Map<TIn, TOut>(value);
        }

        private void InitializeMappings()
        {
            var config = new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<User, UserModel>();

                mapper.CreateMap<UserModel, User>();

                mapper.CreateMap<CustomerApplicationModel, CustomerApplication>();

                mapper.CreateMap<CustomerApplication, CustomerApplicationModel>();

                mapper.CreateMissingTypeMaps = true;                    
            });

           mapper = config.CreateMapper();
        }
    }
}
