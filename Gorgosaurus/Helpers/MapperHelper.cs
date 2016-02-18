using AutoMapper;
using Gorgosaurus.BO.Entities;
using Gorgosaurus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.Helpers
{
    public static class MapperHelper
    {
        public static MapperConfiguration GetMapperConfig()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ForumUser, BasicUserInfo>();
            });

            return config;
        }
    

    }
}
