using Goldies.Data;
using Goldies.Services;
using Goldies.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Goldies.Contollers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IGoldiesRepository _repository;

        public AppController(IMailService mailService, IGoldiesRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }
        
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TO DO: finish writing code to send the email
                _mailService.SendMessage("dbh@goldies.org", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Thank you. Your message has been sent.";
                ModelState.Clear();
            }
            else
            {
                //TO DO: add code to show the errors
            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";

            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts();

            return View(results.ToList());
        }
    }
}
