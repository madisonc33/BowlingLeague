using System;
using System.Collections.Generic;
namespace BowlingLeague.Models.ViewModels
{
    public class IndexViewModel
    {
        //class that will get all the info needed for the index page in an organized way
        public List<Bowler> Bowlers { get; set; }

        public string TeamName { get; set; }

        public PageNumberingInfo PageInfo { get; set; }
    }
}
