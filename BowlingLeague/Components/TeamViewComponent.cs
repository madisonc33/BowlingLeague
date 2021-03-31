using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BowlingLeague.Models;

namespace BowlingLeague.Components
{
    //creates Team View Component that will be used to display a menu of teams. It extends the ViewComponent class
    public class TeamViewComponent : ViewComponent
    {
        //creates a variable to store all the data from the database
        private BowlingLeagueContext context { get; set; }

        //gets data from the db
        public TeamViewComponent(BowlingLeagueContext con)
        {
            context = con;
        }

        //when the VC is called in the layout html, it will call this method that gets a list of team names from the DB
        public IViewComponentResult Invoke()
        {
            //gets this from the URL
            //allows us to change the css style if it was selected
            ViewBag.SelectedType = RouteData?.Values["team"];

            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
