namespace CodePulse.API.Models.DTO
{
    public class CreateCategoryRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}
