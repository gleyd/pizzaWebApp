using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAppPizza.Models;
using adm = WebAppPizza.Areas.Admin.Models;
namespace WebAppPizza.Controllers
{


    public class PizzaController : Controller
    {


        private IRepositoryable<Pizza> _pizzaRepository; // on limite au methode de IRepositoryable meme si on fait un new PizzaRepository
        private IConfiguration _configuration;
        private readonly int itemsPerPage;// const doit etre assigné desuite/   readonly permet d'affecter une valeur que dans le constructeur un byte peut entrer dans un int mais pas l'inverse


        public PizzaController(IRepositoryable<Pizza> pizzaRepository, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._pizzaRepository = pizzaRepository;
             itemsPerPage = Convert.ToByte(this._configuration.GetValue(typeof(byte), "itemsPerPage")); // on peut mettre un byte dans un int car le int a plus de mémoire

        }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>()
        {
            new Pizza(1,"Reine",10.40m, "Sauce tomate, Jambon, Champignons,...","Pizza_reine.jpg"),
            new Pizza(2,"Saumon",12, "Sauce tomate, Saumon,...","Pizza_saumon.jpg"),
            new Pizza(3,"Chorizo",11.50m, "Sauce tomate, Chorizo, Champignons,...","Pizza_chorizo.jpg"),
            new Pizza(4,"4 Fromages",10.40m, "Sauce tomate à l'Origan ou Crème Fraiche, mozzarella fraîche, fromage de chèvre, Emmental et Fourme d'Ambert AOP.","Pizza_reine.jpg"),
            new Pizza(5,"Hawaïenne Chicken",12.50m, "Sauce tomate à l'Origan, mozzarella fraîche, émincés de poulet halal et double ananas","Pizza_saumon.jpg"),
            new Pizza(6,"Provençale",14.50m, "Sauce tomate à l'Origan, mozzarella fraîche, thon, tomates fraîches, oignons frais et olives noires du Maroc.","Pizza_chorizo.jpg"),
            new Pizza(7,"Anchoix",12.40m, "Sauce tomate, Anchoix","Pizza_reine.jpg"),
            new Pizza(8,"Queen",8, "Sauce tomate à l'Origan, mozzarella fraîche, jambon et double champignons frais","Pizza_saumon.jpg"),

        };


        // GET : controller/action
        // [controller]/[action]/Page/2
        [HttpGet("[controller]/[action]/Page/{page?}")]
        public IActionResult Index(int page = 1)
        {
            var pizzasVM = new List<adm.PizzaViewModel>();

            //int itemsPerPage = Convert.ToInt16(_configuration.GetValue(typeof(Int16)), "itemsPerPage"));
            



            ViewBag.ItemsCount = Math.Ceiling((decimal)((PizzaRepository)this._pizzaRepository).Count() / itemsPerPage);
            ViewBag.CurrentPage = page;

            this._pizzaRepository.Read((page - 1) * itemsPerPage, itemsPerPage).ForEach(pizza =>
            {
                pizzasVM.Add(new adm.PizzaViewModel()
                {
                    Description = pizza.Description,
                    Image = pizza.Image,
                    PriceHT = pizza.PriceHT,
                    Title = pizza.Title,
                    IdPizza = pizza.IdPizza
                });
            });
            return View(pizzasVM);
            //var pizzasVM = new List<PizzaViewModel>();


            //ViewBag.ItemsCount = Math.Ceiling((decimal)Pizzas.Count() / itemsPerPage);
            //ViewBag.CurrentPage = page;
            ///*
            //Pizzas.Where(p => p.PriceHT < 15).ToList().ForEach(p =>
            //{
            //    pizzasVM.Add(new PizzaViewModel() { Pizza = p });


            //});*/ //LINQ  Language Interactive Native Query   requete colection, xml, entity, jointure entre xml et sql, c'est Natif    // Forme Fluent et il y a le décomposer
            //Pizzas.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList().ForEach(p =>
            //{

            //    pizzasVM.Add(new PizzaViewModel() { Pizza = p });


            //});


            //return View(pizzasVM);
        }


        public PartialViewResult Detail(int id)
        {
            //var pizza = this.Pizzas.Find(p => p.IdPizza == id);
            //var pizzaVM = new PizzaViewModel()
            //{
            //    Pizza = pizza
            //};
            var pizzaVM = new adm.PizzaViewModel();

            var pizza = this._pizzaRepository.ReadById(id);

            pizzaVM.IdPizza = pizza.IdPizza;
            pizzaVM.Description = pizza.Description;
            pizzaVM.Image = pizza.Image;
            pizzaVM.PriceHT = pizza.PriceHT;
            pizzaVM.Title = pizza.Title;
            pizzaVM.IdPizza = pizza.IdPizza;


            return PartialView("DetailPartial", pizzaVM);
        }


    }
}
