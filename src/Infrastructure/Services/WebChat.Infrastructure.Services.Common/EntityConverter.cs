namespace WebChat.Infrastructure.Services.Common
{
    #region Using

    using Newtonsoft.Json;
    using AutoMapper;
    using Services.Interfaces;
    using Domain.Core.Identity;
    using Domain.Core.Customer;
    using Data.Models.Identity;
    using Data.Models.Application;

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

        public TOut MapToExisting<TIn, TOut>(TIn source, ref TOut existingTarget)
        {
            return mapper.Map(source, existingTarget);
        }

        private void InitializeMappings()
        {
            var config = new MapperConfiguration(mapper =>
            {
                var modelToUser = mapper.CreateMap<UserModel, User>();
                modelToUser.ForMember(user => user.Login, model => model.MapFrom(m => m.UserName))
                           .ForMember(user => user.Login, model => model.MapFrom(m => m.UserName))

                mapper.CreateMap<ApplicationModel, Application>();

                mapper.CreateMap<Application, ApplicationModel>();

                mapper.CreateMissingTypeMaps = true;                    
            });

           mapper = config.CreateMapper();
        }
    }
}
