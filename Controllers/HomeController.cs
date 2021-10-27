using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCDemo.Models;

using GlobalPayments.Api.Services;
using GlobalPayments.Api;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;

namespace SCDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ServicesContainer.ConfigureService(new PorticoConfig
            {
                SecretApiKey = "skapi_cert_MSbAAwAf7WcAmXFskjpdnc2hGaxeVPuNPsCI0JrLZw",
                DeveloperId = "002914",
                VersionNumber = "5037",
                ServiceUrl = "https://cert.api2.heartlandportico.com"
            });

            var card = new CreditCardData {
                Number = "4916455826285158",
                ExpMonth = 12,
                ExpYear = 2025,
                Cvn = "999"
            };

            var address = new Address
            {
                Name = "6860 Dallas Pkwy",
                PostalCode = "750241234"
            };

            decimal amount = 3.50m;

            var response = card.Charge(amount)
                .WithCurrency("USD")
                .WithAddress(address)
                .Execute();

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
