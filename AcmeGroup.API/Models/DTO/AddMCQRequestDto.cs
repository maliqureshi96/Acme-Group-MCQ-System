using System.ComponentModel.DataAnnotations;

namespace AcmeGroup.API.Models.DTO
{
    public class AddMCQRequestDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "MCQ Number must be a positive number")]
        public int McqNumber { get; set; }


        [Required]
        public string Question { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage ="Char. cross the max limit")]
        public string Option1 { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Char. cross the max limit")]
        public string Option2 { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Char. cross the max limit")]
        public string Option3 { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Char. cross the max limit")]
        public string Option4 { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Char. cross the max limit")]
        public string RightAnswer { get; set; }
    }
}
