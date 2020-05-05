using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBookingWebApi
{
    public class ClientBookingInformation
    {

        public String ClientFirstName
        {
            get;
            set;
        }

        public String ClientLastName
        {
            get;
            set;
        }

        public String ClientCardInfo
        {
            get;
            set;
        }
        public Nullable<int> RoomId
        {
            get;
            set;
        }
        public String HotelName
        {
            get;
            set;
        }
        public Nullable<int> NumberOfStars
        {
            get;
            set;
        }
        public Nullable<int> NumberOfPersonToHost
        {
            get;
            set;
        }
        public Nullable<int> MinPrice
        {
            get;
            set;
        }
        public Nullable<int> MaxPrice
        {
            get;
            set;
        }
        public String ArrivalDate
        {
            get;
            set;
        }
        public String DepartureDate
        {
            get;
            set;
        }

        public String HotelTown
        {
            get;
            set;
        }
        //public Nullable<int> HotelId
        //{
        //    get;
        //    set;
        //}

        public ClientBookingInformation()
        {
        }
    }
}