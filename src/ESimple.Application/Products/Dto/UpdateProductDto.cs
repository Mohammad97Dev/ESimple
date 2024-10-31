using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESimple.Products.Dto
{
    public class UpdateProductDto : CreateProductDto, IEntityDto
    {
        public int Id { get; set; }
    }
}
