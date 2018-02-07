using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using adm = WebAppPizza.Areas.Admin.Models;
using WebAppPizza.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WebAppPizza.Services;
using Microsoft.Extensions.Configuration;

namespace WebAppPizza.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class PizzaController : Controller
    {
        private IHostingEnvironment _env;
        private IStaticRepository _staticRepository;
        private IRepositoryable<Pizza> _pizzaRepository; // on limite au methode de IRepositoryable meme si on fait un new PizzaRepository

        private IConfiguration _configuration;

        public PizzaController(IHostingEnvironment env, IStaticRepository staticRepository, IRepositoryable<Pizza> pizzaRepository, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._staticRepository = staticRepository;
            this._pizzaRepository = pizzaRepository;
            this._env = env;
        }

        [HttpGet("[Area]/[controller]/[action]/Page/{page?}")]
        public IActionResult Index(int page = 1)
        {
            var pizzasVM = new List<adm.PizzaViewModel>();

            //int itemsPerPage = Convert.ToInt16(_configuration.GetValue(typeof(Int16)), "itemsPerPage"));
            byte itemsPerPage = Convert.ToByte(this._configuration.GetValue(typeof(byte), "itemsPerPage"));

            

           ViewBag.ItemsCount = Math.Ceiling((decimal)((PizzaRepository)_pizzaRepository).Count() / itemsPerPage);
           
            ViewBag.CurrentPage = page;

            foreach (var pizza in _pizzaRepository.Read((page - 1) * itemsPerPage, itemsPerPage))
            {
                pizzasVM.Add(new adm.PizzaViewModel()
                {
                    Description = pizza.Description,
                    Image = pizza.Image,
                    PriceHT = pizza.PriceHT,
                    Title = pizza.Title,
                    IdPizza = pizza.IdPizza
                });
            }
            return View(pizzasVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(adm.PizzaViewModel pizzaVM) 
        {
            long size = pizzaVM.UploadImage?.Length ?? 0; //si elle est null ou vide alors 0   si et Si 
            // si il y a une longueur ou et qu'il n'est pas null
            var filename = String.Empty;
            IActionResult returnPage = null;
            if (ModelState.IsValid)
            {
                if(size > 0)
                {
                    filename = await CReateFileOnServerAsync(pizzaVM.UploadImage);
                    //TODO:Save Pizza in db
                    // _staticRepository.Pizzas.Add(pizza);
                    
                }

                var pizza = new Pizza()
                {
                    Description = pizzaVM.Description,
                    Image = filename,
                    PriceHT = pizzaVM.PriceHT,
                    Title = pizzaVM.Title

                };
                this._pizzaRepository.Create(pizza);
            }
            else
            {
                ModelState.AddModelError("Error Model", "Les données saisies ne sont pas valide veuillez vérifier celles-ci");
                returnPage = View(pizzaVM);
            }
            if (pizzaVM.addNewPizza)
            {
                returnPage = RedirectToAction("Create");
            }
            else
                returnPage = RedirectToAction("Index", "Pizza", new { area = "Admin" });

            return  returnPage;


        }

        private async Task<string> CReateFileOnServerAsync(IFormFile uploadImage) // il integre les nouveau patern du traitement asynchrone 
        {
            var webRoot = $"{_env.WebRootPath}/images/";    // ou $@"{_env.WebRootPath}\images\";
            var filename = $"Pizza_{DateTime.Now.Ticks}.jpg";
            var filePath = Path.Combine(webRoot, filename);

            using (var fs = new FileStream(filePath, FileMode.Create)) // path, Flux     le using permet de fermer les flux a la fin de l'accolade
            {
               await uploadImage.CopyToAsync(fs);
            }


            return filename;
            //using (var p = new Pizza()) // marche pas car la classe n'est pas Idisposable   Idisposable permet de vider la memoire tampon quand on sort des accolade
           // {
                  //GC manager ta ressource memoire       code manager           la user32 dll  sont non manager
           // }
              //  ;            throw new NotImplementedException();
        }
    }
}