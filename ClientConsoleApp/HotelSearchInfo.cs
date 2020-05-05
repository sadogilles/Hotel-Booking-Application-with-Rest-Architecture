using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsoleApp
{
    class HotelSearchInfo
    {

        public String HotelTown
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
        public Nullable<int> NumberOfStars
        {
            get;
            set;
        }
        public HotelSearchInfo() {

        }
        public HotelSearchInfo(String _hotelTown,int _minPrice,int _maxPrice,int _numberOfStars)
        {
            HotelTown = _hotelTown;
            MinPrice = _minPrice;
            MaxPrice = _maxPrice;
            NumberOfStars = _numberOfStars;
        }
    }
}
