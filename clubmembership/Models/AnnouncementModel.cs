using System.ComponentModel.DataAnnotations;

namespace clubmembership.Models
{
    public class AnnouncementModel
    {
        public Guid Idannouncemment { get; set; }

        //anotari sau decoratori
        [DisplayFormat(DataFormatString = "{0:d}")]  // change 0:MM/dd/yyyy to {0:d} to display in web app
        [DataType(DataType.Date)]
        public DateTime ValidFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime ValidTo { get; set; }

        [StringLength(250, ErrorMessage = "Maximum 250 characters")] //decorator care nu se pune automat din baza de date
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime? EventDateTime { get; set; }
        public string? Tags { get; set; }
    }
}
