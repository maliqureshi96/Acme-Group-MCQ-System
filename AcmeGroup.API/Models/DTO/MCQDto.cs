namespace AcmeGroup.API.Models.DTO
{
    public class MCQDto
    {
        public Guid Id { get; set; }

        public int McqNumber { get; set; }

        public string Question { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public string RightAnswer { get; set; }
    }
}
