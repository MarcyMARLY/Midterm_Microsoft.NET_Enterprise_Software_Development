﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Midterm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Managers.SystemR.GetAllRoutesFromFile();
            Managers.SystemR.GetAllCategoriesFromFile();
            Managers.SystemR.GetAllOrdersFromFile();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
