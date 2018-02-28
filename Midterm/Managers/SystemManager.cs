using Midterm.FileManipulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Midterm.Managers
{
    public class SystemManager
    {


        public List<Models.Route> routeDictionary = new List<Models.Route>();
        public List<Models.Category> categoryDictionary = new List<Models.Category>();
        public List<Models.Order> orderList = new List<Models.Order>();

        public SystemManager() { }

        public List<Models.Route> GetRoutes()
        {
            return routeDictionary;
        }
        public List<Models.Category> GetCategories()
        {
            return categoryDictionary;
        }
        public List<Models.Order> GetOrders()
        {
            return orderList;
        }
        public void AddRoute(Models.Route r)
        {
            routeDictionary.Add(r);
        }
        public void AddOrder(Models.Order o)
        {
            orderList.Add(o);
        }
        public void AddCategory(Models.Category c)
        {
            categoryDictionary.Add(c);
        }
        public List<String> GetRoutesNames()
        {
            return routeDictionary.Select(x => x.routeName).ToList();
        }
        public List<String> GetCategoriesIds()
        {
            return categoryDictionary.Select(x => x.id).ToList();
        }
        
    }
}
