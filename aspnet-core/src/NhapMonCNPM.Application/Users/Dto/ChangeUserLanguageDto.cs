using System.ComponentModel.DataAnnotations;

namespace NhapMonCNPM.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}