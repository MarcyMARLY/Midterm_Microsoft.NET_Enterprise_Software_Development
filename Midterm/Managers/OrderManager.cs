using Midterm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Midterm.Managers
{
    public class OrderManager
    {
        public List<Order> orderTemporaryCollection = new List<Order>();

        public Random rnd = new Random();

        public bool IsAmountInStock(int amount, string categoryId)
        {
            if(amount > SystemR.systemManager.categoryDictionary.Where(x => (x.id == categoryId)).First().totalAmount) {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool IsDate(DateTime date, string routeName)
        {
            TimeSpan ts = date - DateTime.Now ;
            if((ts.TotalDays > SystemR.systemManager.routeDictionary.Where(x => x.routeName == routeName).First().duration)|| date < DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public void CreateOrder(string route, string category,int amount,DateTime dateTime)
        {
            var status = Status.Proceed;
            var sum = amount * SystemR.systemManager.categoryDictionary.Where(x => x.id == category).First().pricePerKg +
    SystemR.systemManager.routeDictionary.Where(x => x.routeName == route).First().price;
            Order o = new Order
            {
                id = rnd.Next(100, 900),
                route = route,
                category = category,
                amount = amount,
                status = status,
                date = dateTime,
                sum = sum
            };

            orderTemporaryCollection.Add(o);
        }
        public void ChangeStatus(int id, Status status)
        {
            orderTemporaryCollection.Where(x => x.id == id).First().status = status;
        }
        public void DeleteOrder(int id)
        {
            orderTemporaryCollection.Remove(orderTemporaryCollection.Where(x => x.id ==id).First());   
        }
        public void SaveOrder(int id) {
            SystemR.orderStore.WriteToFile(orderTemporaryCollection.Where(x => x.id == id).First());
            DeleteOrder(id);
        }
        public List<Order> GetNotSavedOrders()
        {
            return orderTemporaryCollection;
        }

    }
}
