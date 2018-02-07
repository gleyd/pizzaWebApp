using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppPizza.Models;

namespace WebAppPizza.Services
{
    public class StaticRepository : IStaticRepository
    {
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>()
        {
            new Pizza(1, "Reine", 10.40m, "Sauce tomate, Jambon, Champignon,...","Pizza_636524680046754407.jpg")

        };
    }
}
