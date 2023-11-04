namespace MajorService.Data.MajorRequest
{
    public class MajorRequestDto
    {
        public Guid Id { get; set; }
        public string tenChuyenNganh { get; set; }
        public string email { get; set; }
        public int Status { get; set; } = 1;
    }
}
