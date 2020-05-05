using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBookingWebApi.Controllers
{
    
    // /hotelcontroller
    public class HotelController : ApiController
    {
        HotelExternalController hotelControl = new HotelExternalController();


        // Get                                                         api/hotels
        [Route("api/hotels")]
        [HttpGet]
        public IEnumerable<Hotel> Get()
        {
            return hotelControl.getHotelList();
        }

         //  Get                                                     api/hotels/{id}
         [Route("api/hotels/{id:int}")]
         [HttpGet]
        public Hotel Get(int id)
        {
            return hotelControl.findHotel(id);
        }
        //Get                                                       api/hotels/{name}
        [Route("api/hotels/{name}")]
        [HttpGet]
        public Hotel GetHotel(String name)
        {
            return hotelControl.findHotel(name);
        }

        //GET                                                       api/hotel/{name}/room
        [Route("api/hotels/{name}/rooms")]
        [HttpGet]
        public List<Room> GetRoomsFromHotel(String name)
        {
            return hotelControl.getRoomFromHotel(name.ToLower());
        }
       
        //POST                                                      api/hotels/search
       [Route("api/hotels/search")]
       [HttpPost]
        public List<Hotel> PostSearchHotel([FromBody]ClientBookingInformation hotelInfo) 
        {
            return  hotelControl.hotelSearch(hotelInfo.HotelTown, hotelInfo.MinPrice.Value, hotelInfo.MaxPrice.Value, hotelInfo.NumberOfStars.Value);//.Value initialises a nullable variable
        }

        //PUT                                                  api/hotels/name/rooms/id/
        [Route("api/hotels/{name}/rooms/{id:int}")]
        [HttpPut]
        public String BookRoom(int id,String name, [FromBody] ClientBookingInformation bookingInfo) {

          //  return hotelControl.Booking(name,id , bookingInfo.ClientFirstName, bookingInfo.ClientLastName, bookingInfo.ClientCardInfo);

            return "success";

        
           
        }              

    }
}
