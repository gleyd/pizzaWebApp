using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppPizza.Models
{
    public class DetailCommand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdDetailCommand { get; set; }
        public int IdPizza { get; set; }
        public int IdCommand { get; set; }
        public Pizza Pizza { get; set; }
        public Command Command { get; set; }
    }
}