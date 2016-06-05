namespace WebChat.Services.Common
{
    #region Using

    using Newtonsoft.Json;
    using AutoMapper;
    using Services.Interfaces;
    using WebUI.ViewModels.Application;
    using Data.Models.Application;
    using System;
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

        public TOut ConvertToExisting<TIn, TOut>(TIn value, TOut existing)
        {
            return mapper.Map<TIn, TOut>(value, existing);
        }

        private void InitializeMappings()
        {
            var config = new MapperConfiguration(mapper =>
            {
                mapper.CreateMap<ApplicationFieldsViewModel, ApplicationModel>().ReverseMap();
            });

           mapper = config.CreateMapper();
        }
    }
}
