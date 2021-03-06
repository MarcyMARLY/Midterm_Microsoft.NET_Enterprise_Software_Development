﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Midterm.Managers;
using Midterm.Models;

namespace Midterm.Controllers
{
    public class Border
    {
        [DataType(DataType.Date)]
        public DateTime date1 { get; set; }
        [DataType(DataType.Date)]
        public DateTime date2 { get; set; }
    }
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
                error = "Error with amount";
                TempData["error"] = error;
                return RedirectToAction("Index", "Home");
            }else if (!SystemR.orderManager.IsDate(date, route))
            {
                TempData["error"] = "Error with date";
                return RedirectToAction("Index", "Home");
            }
            SystemR.orderManager.CreateOrder(route, category, amount, date);
            return RedirectToAction("OrderInProcess","Home");
        }

        public IActionResult OrderInProcess()
        {
            ViewData["error"] = TempData["error"];
            return View(SystemR.orderManager.GetNotSavedOrders());
        }

        public IActionResult SaveOrder(int id)
        {
            if (SystemR.orderManager.orderTemporaryCollection.Where(x => x.id == id).First().status != Status.Proceed)
            {
                SystemR.orderManager.SaveOrder(id);

                return RedirectToAction("OrderInProcess", "Home");
            }
            else
            {
                TempData["error"] = "Cannot save orders with active status";
                return RedirectToAction("OrderInProcess", "Home");
            }
        }
        public IActionResult DeleteOrder(int id)
        {
            SystemR.orderManager.DeleteOrder(id);
            return RedirectToAction("OrderInProcess", "Home");
        }
        public IActionResult ChangeOrder(int id)
        {
            ViewData["id"] = id;
            return View();
        }
        public IActionResult ChangeStatus(int id, Status status)
        {
            SystemR.orderManager.ChangeStatus(id, status);
            return RedirectToAction("OrderInProcess", "Home");
        }
        public IActionResult FinishedOrders()
        {
            
            return View(SystemR.systemManager.GetRoutes());
        }
        public IActionResult ShowReportChoice(string routeName)
        {
            ViewData["routeName"] = routeName;
            SystemR.GetAllOrdersFromFile();

            ViewData["totalNumberOFOrders"] = SystemR.systemManager.GetOrdersByRoute(routeName).Count;
            ViewData["numberOfCanceled"] = SystemR.systemManager.GetOrdersByRoute(routeName).Where(x => x.status == Status.Canceled).ToList().Count;
            ViewData["numberOfNotOnTime"] = SystemR.systemManager.GetOrdersByRoute(routeName).Where(x => x.status == Status.NotOnTime).ToList().Count;
            double success = Convert.ToDouble(SystemR.systemManager.GetOrdersByRoute(routeName).Where(x => x.status == Status.Delivered).ToList().Count.ToString());
            double total = Convert.ToDouble(SystemR.systemManager.GetOrdersByRoute(routeName).ToList().Count.ToString());
            double percent = success / total * 100;
            Console.WriteLine(percent);
            ViewData["percent"] = percent;
            return View(SystemR.systemManager.GetOrdersByRoute(routeName));
        }
        public IActionResult Sorting(int parameter, string routeName)
        {
            ViewData["routeName"] = routeName;
            SystemR.GetAllOrdersFromFile();

            ViewData["totalNumberOFOrders"] = SystemR.systemManager.GetOrdersByRoute(routeName).Count;
            ViewData["numberOfCanceled"] = SystemR.systemManager.GetOrdersByRoute(routeName).Where(x => x.status == Status.Canceled).ToList().Count;
            ViewData["numberOfNotOnTime"] = SystemR.systemManager.GetOrdersByRoute(routeName).Where(x => x.status == Status.NotOnTime).ToList().Count;
            double success = Convert.ToDouble(SystemR.systemManager.GetOrdersByRoute(routeName).Where(x => x.status == Status.Delivered).ToList().Count.ToString());
            double total = Convert.ToDouble(SystemR.systemManager.GetOrdersByRoute(routeName).ToList().Count.ToString());
            double percent = success / total * 100;
            Console.WriteLine(percent);
            ViewData["percent"] = percent;
            switch (parameter)
            {
                case 1:
                    return View(SystemR.systemManager.GetOrdersByRoute(routeName).OrderBy(x => x.category));
                    break;
                case 2:
                    return View(SystemR.systemManager.GetOrdersByRoute(routeName).OrderBy(x => x.amount));
                    break;
                case 3:
                    return View(SystemR.systemManager.GetOrdersByRoute(routeName).OrderBy(x => x.status));
                    break;
                case 4:
                    return View(SystemR.systemManager.GetOrdersByRoute(routeName).OrderBy(x => x.date));
                    break;
                case 5:
                    return View(SystemR.systemManager.GetOrdersByRoute(routeName).OrderBy(x => x.sum));
                    break;
                default:
                    return View(SystemR.systemManager.GetOrdersByRoute(routeName));
                    break;
            }
            

        }
        public IActionResult Filter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FilterResult(Border b)
        {
            var orders = SystemR.systemManager.orderList.Where(x => ((x.date > b.date1 && x.date < b.date2)|| (x.date < b.date1 && x.date > b.date2)));
            return View(orders);
        }
        public IActionResult Routes()
        {
            return View(SystemR.systemManager.GetRoutes());
        }
        public IActionResult Categories()
        {
            return View(SystemR.systemManager.GetCategories());
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
