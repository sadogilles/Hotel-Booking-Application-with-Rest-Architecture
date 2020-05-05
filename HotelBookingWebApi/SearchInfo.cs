using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBookingWebApi
{
    public class SearchInfo
    {
        public String HotelTown
        {
            get;
            set;
        }
        public int MinPrice
        {
            get;
            set;
        }
        public int MaxPrice
        {
            get;
            set;
        }
        public int NumberOfStars
        {
            get;
            set;
        }
        public SearchInfo()
        {

        }


    }
}