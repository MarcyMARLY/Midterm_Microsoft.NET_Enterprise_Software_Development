﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Midterm.Managers;
using Midterm.Models;

namespace Midterm.Controllers
{
    public class HomeController : Controller
    {
        public string error;
        [HttpGet]
        public IActionResult Index()
        {
            var routeNamesCollection = SystemR.systemManager.GetRoutesNames();
            var categoriesIdCollection = SystemR.systemManager.GetCategoriesIds();

            
            SelectList rnC = new SelectList(routeNamesCollection);
            SelectList ciC = new SelectList(categoriesIdCollection);
            ViewData["rnC"] = new SelectList(routeNamesCollection);
            ViewData["ciC"] = ciC;
           
            ViewData["error"] = TempData["error"];
            return View();
        }
        [HttpPost]
        public IActionResult Index(Order o)
        {
          
            var route = o.route;
            var category = o.category;
            var amount = o.amount;
            var date = o.date;
            Console.WriteLine(date);
            Console.WriteLine((DateTime.Now - date).Days);
            if (!SystemR.orderManager.IsAmountInStock(amount, category))
            {
                //Console.WriteLine("Adsdsdsddddddddddddddddddddddddd");
                error = "Too big amount";
                TempData["error"] = error;
                return RedirectToAction("Index", "Home");
            }else if (!SystemR.orderManager.IsDate(date, route))
            {
                TempData["error"] = "Too big duration";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("About","Home");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
