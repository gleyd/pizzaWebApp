using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppPizza.Models;

namespace WebAppPizza.Controllers
{
    public class MentionController : Controller
    {
        public IActionResult Index() // je veux un model qui contient 3 paragraphe
        {
            var mention = new Mention {

                Title = "Mentions légale",
                Paragraphe1 = "diefs semouhfsef smouhuiesgohisg sMOhgsmohegrs seomihsrgihsgf ooihrghsergih moihegzfmhsegs moishefihrdgpihu esf rsg sRgf sef ",
                Paragraphe2 = "diefs semouhfsef smouhuiesgohisg sMOhgsmohegrs seomihsrgihsgf ooihrghsergih moihegzfmhsegs moishefihrdgpihu SEG SGRdrgESGfef EFSzef ",
                Paragraphe3 = "diefs semouhfsef smouhuiesgohisg sMOhgsmohegrs seomihsrgihsgf ooihrghsergih moihegzfmhsegs moishefihrdgpihu ESFESFSEFSEFSEfES F"
        };


            ViewBag.MonTitle = mention.Title;
            ViewData["MonTitle"] = mention.Title;
            return View(mention);
        }
    }
}