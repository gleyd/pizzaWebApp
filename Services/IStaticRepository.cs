using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppPizza.Models;

namespace WebAppPizza.Services
{
    public interface IStaticRepository
    {
        List<Pizza> Pizzas { get; set; }
    }
}
