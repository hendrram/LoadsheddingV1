using LoadsheddingV1.Migrations;
using LoadsheddingV1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LoadsheddingV1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LoadSheddingContext _context;

        public HomeController(ILogger<HomeController> logger, LoadSheddingContext context)
        {
            _logger = logger;
            _context = context;
        }



    public IActionResult Index()
    {
        var loadSheddingEvents = _context.LoadSheddingEvents.ToList(); // Fetch data from the context

        return View(loadSheddingEvents); // Pass the data to the view
    }

            public IActionResult Privacy()
            {
                return View();
            }

            public IActionResult Labs()
            {
                var uniqueLabs = _context.Labs
                                  .GroupBy(l => l.LabName)
                                  .Select(g => g.First())
                                  .ToList();

                return View(uniqueLabs);
            }

            [HttpPost]
            public IActionResult UpdateLab(List<Labs> labs)
            {
                if (labs != null && labs.Any())
                {
                    foreach (var lab in labs)
                    {
                        var labsToUpdate = _context.Labs.Where(l => l.LabName == lab.LabName).ToList();
                        foreach (var labToUpdate in labsToUpdate)
                        {
                            labToUpdate.OnOff = lab.OnOff; // Update the lab's status
                        }
                    }

                    _context.SaveChanges(); // Save changes to the database
                }

                return RedirectToAction("Labs");
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }