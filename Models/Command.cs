using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppPizza.Models
{
    public class Command
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCommand { get; set; }
        public DateTime CommandDate { get; set; }
        public decimal Total { get; set; }

        [NotMapped] // ajoute un champs qui ne sera pas dans la base de donnée

        public string Description { get; set; }
        public ICollection<DetailCommand> DetailCommands { get; set; }

        public Command()
        {
            DetailCommands = new HashSet<DetailCommand>();  // initialise une collection ordonné
        }
    }
}