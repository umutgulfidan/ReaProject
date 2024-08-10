namespace Entities.DTOs
{
    public class UserFilterObject
    {
        public string? SearchText { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? UserId { get; set; }
        public bool? Status { get; set; }
        public List<int>? RoleIds { get; set; }

        public DateTime? MinRegisterDate { get; set; }
        public DateTime? MaxRegisterDate { get; set; }

    }
}