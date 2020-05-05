using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBookingWebApi
{
    public class HotelExternalController
    {
        List<Hotel> hotelSearched;
        List<Hotel> hotels;
        HotelExternalModel hotelModel;
        Hotel hotel;
        Room room;
        List<Room> rooms;
        ClientBookingInformation clientInfo;
        // 8\Booking booking;

        public HotelExternalController()
        {
            hotelModel = new HotelExternalModel();
            hotelSearched = new List<Hotel>();
            hotels = hotelModel.hotelList();
            hotel = new Hotel();
            room = new Room();
            rooms = new List<Room>();
            clientInfo = new ClientBookingInformation();
            // booking = new Booking();
        }
        //simple search
        public List<Hotel> hotelSearch(String _town, int _minPrice, int _maxPrice, int _numberOfStars)
        {



            foreach (Hotel hotel in hotels)
            {
                if (hotel.Town.ToLower().Equals(_town.ToLower()) && hotel.Rating.Equals(_numberOfStars))
                {

                    foreach (Room room in hotel.Rooms)
                    {
                        if (room.Price > _minPrice && room.Price < _maxPrice)
                        {
                            //room within price exist
                            hotelSearched.Add(hotel);
                            break; //go to to next hotel

                        }
                        else
                        {
                            //do nothing 
                        }


                    }

                }

            }


            return hotelSearched; //no hotel found 
        }

        public List<Hotel> getHotelList()
        {
            return hotelModel.hotelList();
        }
        //public String Booking(String _firstName,String _lastName,String _cardInfo) {
        //    return book.AddBooking(_firstName, _lastName, _cardInfo);
        //}

        public String Booking(String _hotelName, int _room_id, String _firstName, String _lastName, String _cardInfo)
        {
            //return booking.AddBooking(_hotel_id, _room_id, _client_id, _firstName, _lastName, _cardInfo);


            //find the hotel
            foreach (Hotel h in hotels)
            {
                if (_hotelName.ToLower().Equals(h.Name.ToLower()))
                {
                    hotel = h;
                    //find the room
                    foreach (Room r in h.Rooms)
                    {
                        if (r.Id.Equals(_room_id))
                        {
                            room = r;
                        }

                    }

                }

                //change state of room
                room.State = true;
                //reduce the number of rooms
                room.NumberOfBed = room.NumberOfBed--;
                return "Booking successful! Thanks";
            }
            return "Error";
        }

        public List<Room> getRoomFromHotel(String _hotelName)
        {
            foreach (Hotel h in hotels)
            {

                //search hotel
                if (h.Name.ToLower().Equals(_hotelName.ToLower()))
                {
                    hotel = h;

                    foreach (Room r in hotel.Rooms)
                    {
                        rooms.Add(r);
                    }
                }



            }

            return rooms;
        }

        public Hotel findHotel(int _id) {

            return hotels.Where(h => h.ID == _id).FirstOrDefault();
        }
        public Hotel findHotel(String _hotelName)
        {
            return hotels.Where(h => h.Name.ToLower() == _hotelName.ToLower()).FirstOrDefault();
        }
        
    }
}