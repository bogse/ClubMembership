using Microsoft.AspNetCore.Mvc.Rendering;

namespace clubmembership.Models
{
    public class ViewModel
    {
        public IEnumerable<MemberModel> Members { get; set; }
        public IEnumerable<MembershipTypeModel> MembershipTypes { get; set; }
    }
}
