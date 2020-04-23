using AutoMapper;
using Goldies.Data.Entities;
using Goldies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldies.Data
{
    public class GoldiesMappingProfile : Profile
    {
        public GoldiesMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
