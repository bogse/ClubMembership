using System.ComponentModel.DataAnnotations;

namespace clubmembership.Models
{
    public class CodeSnippetModel
    {
        public Guid IdcodeSnippet { get; set; }
        public string Title { get; set; } = null!;
        public string ContentCode { get; set; } = null!;
        public Guid Idmember { get; set; }
        public int Revision { get; set; }
        public Guid? IdsnippetPreviousVersion { get; set; }

        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        [DataType(DataType.Date)]
        public DateTime DateTimeAdded { get; set; }
        public bool IsPublished { get; set; }
    }
}
