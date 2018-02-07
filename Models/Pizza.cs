using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebAppPizza.Models
{
    public class Pizza
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPizza { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PriceHT { get; set; }
        public string Image { get; set; }

        public ICollection<DetailCommand> DetailCommands { get; set; }

        public Pizza()
        {

        }
        /*public DateTime Date { get; set; }
                DateTime? le ? permet de transformer le type primitif en type nullable

                structure

                    pas de classe
                    pas d'instance
                    pas d'héritage
                    pas la meme place au niveau de la memoire
                    le ctor doit implemente toute les propertie de la structure
                    la structur est plus rapide sur les donnée numérique que la classe*/






        public Pizza(int idPizza, string title, decimal priceHT)
        {
            this.IdPizza = idPizza;
            this.Title = title;
            this.PriceHT = priceHT;

        }

        public Pizza(int idPizza, string title, decimal priceHT, string description, string image)
            :this(idPizza, title, priceHT)
        {
            this.Description = description;
            this.Image = image;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var pi in typeof(Pizza).GetProperties() )  //typeof = getClass dans java
            {
                sb.AppendFormat("{0} => {1}", pi.Name, pi.GetValue(this));
            }

            return sb.ToString();
        }

    }
}