using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPizza.Areas.Admin.Models
{        /* [Key] curseur apres y de key apuyer sur ctrl + ;  et appeler le namespace using System.ComponentModel.DataAnnotations;*/
    public class PizzaViewModel
    {
        
        public int IdPizza { get; set; }

        [Required] //atribute de validation qui vaut que pour la propriete du dessous
        [RegularExpression(@"^[A-Z-0-9]{1}[\ ôçà'a-z]{1,34}$")]
        [Display(Name ="Nom de la Pizza")]

        public string Title { get; set; }

        [RegularExpression(@"^[A-Z]{1}[\- .,'!éèàça-zA-Z]+",ErrorMessage = "Ne correspond pas au format attendu")]
        [MaxLength(300)]
        
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString ="{0:C2}")]
        [Display(Name ="Prix H.T.")]
        public decimal PriceHT { get; set; }

        public string Image { get; set; }

        public decimal PriceTTC { get { return this.PriceHT * 1.2m; } set { } }


        public IFormFile UploadImage{ get; set; }

        public bool addNewPizza { get; set; } = false;

       
    }
}
