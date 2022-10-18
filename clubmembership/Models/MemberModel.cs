namespace clubmembership.Models
{
    public class MemberModel
    {
        public Guid Idmember { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string? Description { get; set; }
        public string? Resume { get; set; }
    }
}
