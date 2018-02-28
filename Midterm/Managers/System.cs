using Midterm.FileManipulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Midterm.Managers
{
    public class System
    {
        static readonly string categoriesPath = "AppData/categories.csv";
        static readonly string ordersPath = "AppData/orders.csv";
        static readonly string routesPath = "AppData/routes.csv";

        public static OrderFileManipulator orderStore = new OrderFileManipulator { Path = ordersPath };
        public static CategoryFileManipulator categoryStore = new CategoryFileManipulator { Path = categoriesPath };
        public static RouteFileManipulator routeStore = new RouteFileManipulator { Path = routesPath };

        public static SystemManager systemManager = new SystemManager();

        public static void GetAllRoutesFromFile()
        {
            var routesCollection = routeStore.GetCollection();
            foreach(var item in routesCollection)
            {
                systemManager.AddRoute(item);
            }
        }

        public static void GetAllCategoriesFromFile()
        {
            var categoriesCollection = categoryStore.GetCollection();
            foreach (var item in categoriesCollection)
            {
                systemManager.AddCategory(item);
            }
        }
        public static void GetAllOrdersFromFile()
        {
            var ordersCollection = orderStore.GetCollection();
            foreach(var item in ordersCollection)
            {
                systemManager.AddOrder(item);
            }
        }
    }
}
