using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppPizza.Models
{


    public class PizzaViewModel
    {
        public Pizza Pizza { get; set; }
       

        public PizzaViewModel()
        {

        }

        public decimal PriceTTC()
        {
            return this.Pizza.PriceHT * 1.2m;
        }

        

    }
}
