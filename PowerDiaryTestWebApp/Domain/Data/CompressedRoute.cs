using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerDiaryTestWebApp.Domain.Data
{
    public class CompressedRoute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Uri { get; set; }

        public string CompressedUri { get; set; }
    }
}