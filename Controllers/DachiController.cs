using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

namespace Dojodachi
{
    public class DachiController : Controller
    {
        int Fullness = 20;
        int Happiness = 20;
        int Energy = 50;
        int Meals = 3;
        string Message = "Lets Play!!!";

        [HttpGet("dojodachi")]
        public ViewResult Dojodachi()
        {
            if (HttpContext.Session.GetInt32("Fullness") == null)
            {
              HttpContext.Session.SetInt32("Fullness", Fullness);
              HttpContext.Session.SetInt32("Happiness", Happiness);
              HttpContext.Session.SetInt32("Energy", Energy);
              HttpContext.Session.SetInt32("Meals", Meals);
              HttpContext.Session.SetString("Message", Message);
            }
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Message = HttpContext.Session.GetString("Message");
            return View("Dojodachi");
        }

        [HttpGet("restart")]
         public RedirectToActionResult Restart()
      {
          HttpContext.Session.Clear();
          return RedirectToAction("Dojodachi");
      }

      [HttpGet("feed")]
      public RedirectToActionResult Feed()
      {
          if (HttpContext.Session.GetInt32("Meals") >= 1)
          {
              Random rand = new Random();
              int? MealCount = HttpContext.Session.GetInt32("Meals");
              int? FullCount = HttpContext.Session.GetInt32("Fullness");
              MealCount--;
              int? full = rand.Next(5, 11);
              FullCount += full;
              string msg = "You fed your Dojodachi! Meals -1, Fullness + " + full;
              HttpContext.Session.SetString("Message", msg);
              HttpContext.Session.SetInt32("Meals", (int)MealCount);
              HttpContext.Session.SetInt32("Fullness", (int)FullCount);
          }
          return RedirectToAction("Dojodachi");
      }

      [HttpGet("play")]
      public RedirectToActionResult Play()
      {
          Random rand = new Random();
          if((HttpContext.Session.GetInt32("Energy") >= 1))
          {
            int? EnergyCount = HttpContext.Session.GetInt32("Energy");
            int? HappyCount =  HttpContext.Session.GetInt32("Happiness");
            EnergyCount -= 5;
            int? happy = rand.Next(5,11);
            HappyCount += happy;
            string msg = "You played with your Dojodachi! Happiness + " + happy + ", Energy -5";
            HttpContext.Session.SetString("Message", msg);
            if (HttpContext.Session.GetInt32("Fullness") <= 0 || HttpContext.Session.GetInt32("Happiness") <= 0){
                return RedirectToAction("Lose");
            }
            else if (HttpContext.Session.GetInt32("Fullness") >= 100 && HttpContext.Session.GetInt32("Happiness") >= 100 && HttpContext.Session.GetInt32("Energy") >= 100)
            {
                return RedirectToAction("Win");
            }
            else {
                HttpContext.Session.SetInt32("Energy", (int)EnergyCount);
                HttpContext.Session.SetInt32("Happiness", (int)HappyCount);
                return RedirectToAction("Dojodachi");
            }
          }
          return RedirectToAction("Dojodachi");
      }

        [HttpGet("work")]
        public RedirectToActionResult Work()
        {
            if((HttpContext.Session.GetInt32("Energy") >= 1))
            {
                Random rand = new Random();
                int? EnergyCount = HttpContext.Session.GetInt32("Energy");
                int? MealCount =  HttpContext.Session.GetInt32("Meals");
                EnergyCount -= 5;
                int? meal = rand.Next(1,4);
                MealCount += meal;
                string msg = "You worked your Dojodachi! Meals + " + meal + ", Energy -5";
                HttpContext.Session.SetString("Message", msg);
                if (HttpContext.Session.GetInt32("Fullness") <= 0 || HttpContext.Session.GetInt32("Happiness") <= 0){
                    return RedirectToAction("Lose");
                }
                else if (HttpContext.Session.GetInt32("Fullness") >= 100 && HttpContext.Session.GetInt32("Happiness") >= 100 && HttpContext.Session.GetInt32("Energy") >= 100)
                {
                    return RedirectToAction("Win");
                }
                else {
                    HttpContext.Session.SetInt32("Energy", (int)EnergyCount);
                    HttpContext.Session.SetInt32("Meals", (int)MealCount);
                    return RedirectToAction("Dojodachi");
                }
            }
            return RedirectToAction("Dojodachi");
        }

        [HttpGet("sleep")]
        public RedirectToActionResult Sleep()
        {
            if((HttpContext.Session.GetInt32("Fullness") >= 1 && (HttpContext.Session.GetInt32("Happiness") >= 1)))
            {
                Random rand = new Random();
                int? EnergyCount = HttpContext.Session.GetInt32("Energy");
                int? FullCount =  HttpContext.Session.GetInt32("Fullness");
                int? HappyCount =  HttpContext.Session.GetInt32("Happiness");
                EnergyCount += 15;
                FullCount -= 5;
                HappyCount -= 5;
                string msg = "Your Dojodachi is rested now! Energy +5, Fullness -5, Happiness -5";
                HttpContext.Session.SetString("Message", msg);
                if (HttpContext.Session.GetInt32("Fullness") >= 100 && HttpContext.Session.GetInt32("Happiness") >= 100 && HttpContext.Session.GetInt32("Energy") >= 100)
                {
                    return RedirectToAction("Win");
                }
                else {
                    HttpContext.Session.SetInt32("Energy", (int)EnergyCount);
                    HttpContext.Session.SetInt32("Fullness", (int)FullCount);
                    HttpContext.Session.SetInt32("Happiness", (int)HappyCount);
                    return RedirectToAction("Dojodachi");
                }
            }
            else if(HttpContext.Session.GetInt32("Fullness") <= 0 || HttpContext.Session.GetInt32("Happiness") <= 0)
            {
                return RedirectToAction("Lose");
            }
            return RedirectToAction("Dojodachi");
        }


      [HttpGet("win")]
      public ViewResult Win()
      {
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            return View();
      }

      [HttpGet("lose")]
      public ViewResult Lose()
      {
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            return View();
      }
    }
}