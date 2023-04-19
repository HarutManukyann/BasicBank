using AutoMapper;

using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Mappers
{
    public static class Mapper<T,S>
    {
        static MapperConfiguration configuration;
        static Mapper()
        {
            configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, S>();
            });
        }
        public static S Map(T model)
        {
            IMapper iMapper = configuration.CreateMapper();
            S destination = iMapper.Map<T,S>(model);
            return destination;
        }
      

    }
}
