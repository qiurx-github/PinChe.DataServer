using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinChe.DataServer 
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg=> {
                //cfg.CreateMap<User, UserViewModel>();
            });
        }
    }
}