using System.ComponentModel.DataAnnotations;

namespace ESimple.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}