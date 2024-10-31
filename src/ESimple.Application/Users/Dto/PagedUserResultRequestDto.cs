using Abp.Application.Services.Dto;
using System;
using static ESimple.Enums.Enum;

namespace ESimple.Users.Dto
{
    //custom PagedResultRequestDto
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public UserType? Type { get; set; }
    }
}
