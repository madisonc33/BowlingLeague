using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private BowlingLeagueContext context { get; set; }

        //creates a variable to store how many items will be displayed on each page
        public int ItemsPerPage { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext con)
        {
            _logger = logger;
            context = con;
        }

        public IActionResult Index(string team, long? teamid, int pagenum = 0)
        {
            //sets the items that will be showed on each page
            ItemsPerPage = 5;

            //creates and returns new view model tht will hold all the needed information
            return View(new IndexViewModel
            {
                //gets all bowlers if no team is specified, if team is specified it gets all the players on that team
                Bowlers = context.Bowlers
                    .Where(x => x.TeamId == teamid || teamid == null)
                    .OrderBy(x => x.BowlerLastName)
                    .Skip((pagenum - 1) * ItemsPerPage)
                    .Take(ItemsPerPage)
                    .ToList(),

                //sets team name. If no team name is specified, sets it to null
                TeamName = team,

                //sets paging information based on info in the DB
                PageInfo = new PageNumberingInfo
                {
                    PageSize = ItemsPerPage,

                    //if no team has been selected, then get the full count. Otherwise only count the number from the team that has been selected
                    TotalBowlers = (teamid == null ? context.Bowlers.Count() :
                        context.Bowlers
                            .Where(x => x.TeamId == teamid)
                            .Count()),
                    CurrentPage = pagenum
                }
            }) ;
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
