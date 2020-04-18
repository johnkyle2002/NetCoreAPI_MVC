using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreMVC.Controllers
{
    public class TimeRecordController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}