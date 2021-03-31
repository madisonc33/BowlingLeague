using System;
namespace BowlingLeague.Models.ViewModels
{
    //class to hold the info for pages that we need to create the dynamic page buttons at the bottom of the page
    public class PageNumberingInfo
    {
        public int PageSize { get; set; }

        public int TotalBowlers { get; set; }

        public int CurrentPage { get; set; }

        //turns num into a decimal, then do the division and turn it back into an integer
        public int TotalPages => (int)Math.Ceiling(((decimal)TotalBowlers / PageSize));
    }
}
