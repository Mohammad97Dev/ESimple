using AutoMapper;
using ESimple.Domains.Products;
using ESimple.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Products.Mapper
{
    public class ProductMapProfile : Profile
    {
        public ProductMapProfile()
        {
            CreateMap<CreateProductDto,Product>().ReverseMap();
            CreateMap<UpdateProductDto,Product>().ReverseMap();
            CreateMap<ProductDto,Product>().ReverseMap();
        }
    }
}
