﻿using Microwin.Hosting.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SampleProject.Owin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            OwinService.Start(new HttpConfiguration());

            Console.ReadLine();
        }
    }
}
