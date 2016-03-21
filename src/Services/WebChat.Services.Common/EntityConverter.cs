namespace WebChat.Services.Common
{
    #region Using

    using Newtonsoft.Json;
    using AutoMapper;
    using System.Linq;
    using Services.Interfaces;
    using Domain.Core.Identity;
    using Business.Core.User;
    using WebChat.Domain.Data;

    #endregion

    public class EntityConverter : IEntityConverter
    {
        #region Private Members

        private IMapper mapper;

        #endregion

        #region Static Members

        private static readonly EntityConverter _instance;
        static EntityConverter()
        {
            _instance = new EntityConverter();
        }

        public static EntityConverter Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion


        private EntityConverter()
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

                mapper.CreateMap<UserModel, User>()
                      .Include<UserModel, Customer>()
                      .ForMember(user => user.PhotoSource, model => model.MapFrom(x => x.Claims.FirstOrDefault(c => c.ClaimType == AppClaimTypes.PhotoSource).ClaimValue))
                      .ReverseMap();
                     
            });

           mapper = config.CreateMapper();
        }
    }
}
