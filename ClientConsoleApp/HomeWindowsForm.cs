using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

//using System.Json;

namespace ClientConsoleApp
{
    public partial class HomeWindowsForm : Form
    {
        ClientBookingInformation client;

        HttpClient restClient;

        int numberOfClick;

        List<Hotel> hotels;
        List<Room> rooms;

        public HomeWindowsForm()
        {
            InitializeComponent();
            //During initialisation only the 2 primary buttons are visible (find hotel and find all hotel)
            notification.Visible = true;
            tableHotelInformation.Visible = false;
            listHotelGrid.Visible = false;
            ClientInformationLayoutPanel.Visible = false;
            roomDataGridView.Visible = false;
            notification.Text = "Notification Here!";
            client = new ClientBookingInformation();
            numberOfClick = 0;
            hotels = new List<Hotel>();
            rooms = new List<Room>();
            restClient = new HttpClient();
            restClient.BaseAddress = new Uri("http://localhost:59312/");

        }

        private async void listHotelButton_Click(object sender, EventArgs e)
        {
            notification.Visible = true;
            listHotelGrid.Visible = true;
            tableHotelInformation.Visible = false;
            ClientInformationLayoutPanel.Visible = false;
            roomDataGridView.Visible = false;

            notification.Text = "All Hotel Found!";

            HttpResponseMessage message = await restClient.GetAsync("api/hotels");
            message.EnsureSuccessStatusCode();
            String result = await message.Content.ReadAsStringAsync();

           hotels = JsonConvert.DeserializeObject<List<Hotel>>(result);

            listHotelGrid.DataSource = null;
            listHotelGrid.Rows.Clear();

            //populate grid
            notification.Visible = true;
         

            foreach (Hotel h in hotels)
            {

                //contains the number of rows
                int row = listHotelGrid.Rows.Add();

                //load values from service
                listHotelGrid["name", row].Value = h.Name;
                listHotelGrid["numberOfStars", row].Value = h.Rating.ToString();
                listHotelGrid["numberOfBed", row].Value = h.NumberOfBed.ToString();
                listHotelGrid["address", row].Value = h.Address.ToString();
                listHotelGrid["phoneNumber", row].Value = h.PhoneNumber.ToString();
                listHotelGrid["town", row].Value = h.Town.ToString();
                listHotelGrid["country", row].Value = h.Country.ToString();
                Console.WriteLine("Hotel Rating!");
                Console.WriteLine(h.Rating);


                List<int> prices = new List<int>();

                //compute price
                foreach (Room r in h.Rooms)
                {

                    prices.Add(r.Price);

                    Console.WriteLine("price:", r.Price);

                }

                var minPrice = prices.Min();
                var maxPrice = prices.Max();

                String priceRange = minPrice.ToString() + "-" + maxPrice.ToString();

                listHotelGrid["priceRange", row].Value = priceRange;

            }

            numberOfClick++;

        }

        private void findHotelButton_Click(object sender, EventArgs e)
        {
            notification.Visible = true;
            tableHotelInformation.Visible = true;

            listHotelGrid.Visible = false;
            roomDataGridView.Visible = false;
            ClientInformationLayoutPanel.Visible = false;
            notification.Text = "Enter Your Search criteria!";

         

        }

        private void title_Click(object sender, EventArgs e)
        {
            notification.Visible = true;
            tableHotelInformation.Visible = false;
            listHotelGrid.Visible = false;
            ClientInformationLayoutPanel.Visible = false;
            roomDataGridView.Visible = false;
            notification.Text = "Click a Button!";
        }

        private  async void submitButton_Click(object sender, EventArgs e)
        {
            notification.Visible = true;
            listHotelGrid.Visible = true;
            tableHotelInformation.Visible = false;
            ClientInformationLayoutPanel.Visible = false;
            roomDataGridView.Visible = false;

            notification.Text = "Hotel Found!";
            //check for empty field
            if (String.IsNullOrEmpty(townTextBox.Text) || String.IsNullOrEmpty(ArrivalDateTextBox.Text) || String.IsNullOrEmpty(depatureDateTextBox.Text) || String.IsNullOrEmpty(minimumPriceTextBox.Text) || String.IsNullOrEmpty(maximumPriceTextBox.Text) || String.IsNullOrEmpty(numberOfStarsTextBox.Text) || String.IsNullOrEmpty(numberOfPersonToHostTextBox.Text))
            {
                notification.Text = "Some Field are empty!";

                listHotelGrid.Visible = false;
                tableHotelInformation.Visible = true;

            }
            else
            {
                client.HotelTown = townTextBox.Text.ToLower();
                Console.WriteLine(client.HotelName);
                client.ArrivalDate = ArrivalDateTextBox.Text;
                client.DepartureDate = depatureDateTextBox.Text;
                client.MinPrice = Int32.Parse(minimumPriceTextBox.Text);
                client.MaxPrice = Int32.Parse(maximumPriceTextBox.Text);
                client.NumberOfStars = Int32.Parse(numberOfStarsTextBox.Text);
                client.NumberOfPersonToHost = Int32.Parse(numberOfPersonToHostTextBox.Text);
                client.NumberOfStars = Int32.Parse(numberOfStarsTextBox.Text);

                //convert search to json
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new HotelSearchInfo(client.HotelTown,client.MinPrice,client.MaxPrice,client.NumberOfStars));
                Console.WriteLine(json);

                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
       
                HttpResponseMessage message = await restClient.PostAsync("api/hotels/search", data);


                message.EnsureSuccessStatusCode();
                String result = await message.Content.ReadAsStringAsync();

                hotels = JsonConvert.DeserializeObject<List<Hotel>>(result);

                //clear the grid
                listHotelGrid.DataSource = null;
                listHotelGrid.Rows.Clear();

                //populate grid with hotels found
                foreach (Hotel h in hotels)
                {
                    //contains the number of rows
                    int row = listHotelGrid.Rows.Add();

                    //load values from service
                    listHotelGrid["name", row].Value = h.Name;
                    listHotelGrid["numberOfStars", row].Value = h.Rating.ToString();
                    listHotelGrid["numberOfBed", row].Value = h.NumberOfBed.ToString();
                    listHotelGrid["address", row].Value = h.Address.ToString();
                    listHotelGrid["phoneNumber", row].Value = h.PhoneNumber.ToString();
                    listHotelGrid["town", row].Value = h.Town.ToString();
                    listHotelGrid["country", row].Value = h.Country.ToString();
                    listHotelGrid["chooseHotel", row].Value = submitButton;


                    List<int> prices = new List<int>();
                   
                    //compute price
                    foreach (Room r in h.Rooms)
                    {

                        prices.Add(r.Price);

                        Console.WriteLine("price:",r.Price);

                    }

                    var minPrice = prices.Min();
                    var maxPrice = prices.Max();

                    String priceRange = minPrice.ToString() + "-" + maxPrice.ToString();

                    listHotelGrid["priceRange", row].Value = priceRange;
                }

            }
        }

        private  async void listHotelGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //set room pane to foreground
            roomDataGridView.Visible = true;
            //hide the other panels
            tableHotelInformation.Visible = false;
            listHotelGrid.Visible = false;
            tableHotelInformation.Visible = false;
            ClientInformationLayoutPanel.Visible = false;
            notification.Visible = false;


            if (e.ColumnIndex == 8)
            {
                //load the rooms
                if (listHotelGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    listHotelGrid.CurrentRow.Selected = true;
                    //Console.WriteLine(listHotelGrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString());
                    //get the hotel name
                    client.HotelName = listHotelGrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();

                    
                    Console.WriteLine(listHotelGrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString());
                  
                    // get the hotel name
                   client.HotelName = listHotelGrid.Rows[e.RowIndex].Cells["name"].FormattedValue.ToString();

                    //get the rooms
                    String url = "api/hotels/" + client.HotelName + "/rooms";
                    Console.WriteLine(url);
                    HttpResponseMessage message = await restClient.GetAsync(url);
                    message.EnsureSuccessStatusCode();
                    String result = await message.Content.ReadAsStringAsync();

                    rooms = JsonConvert.DeserializeObject<List<Room>>(result);


                    //clear grid
                    roomDataGridView.DataSource = null;
                    roomDataGridView.Rows.Clear();

                   // the rooms to roomgrid
                   // populate the rooms to roomgrid

                    foreach (Room room in rooms)
                    {
                        int row = roomDataGridView.Rows.Add();
                        roomDataGridView["roomId", row].Value = room.Id;
                        roomDataGridView["roomPrice", row].Value = room.Price;
                        roomDataGridView["roomNumberOfBed", row].Value = room.NumberOfBed;
                        roomDataGridView["roomCapacity", row].Value = room.Capacity;
                        roomDataGridView["roomStatus", row].Value = room.State;
                    }
                }
            }
            //set notification visible
            notification.Visible = true;
            notification.Text = "Choose a Room!";
        }

        private void roomDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 5)
            {
                if (roomDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    roomDataGridView.CurrentRow.Selected = true;
                    
                    client.RoomId = Int32.Parse(roomDataGridView.Rows[e.RowIndex].Cells["roomId"].FormattedValue.ToString());


                }
            }
            else
            {
                notification.Text = "Click a Button";
            }

            roomDataGridView.Visible = false;
            ClientInformationLayoutPanel.Visible = true;
            notification.Text = "Enter your Credential To Book Room";
        }

        private  async void submitClientInformation_Click(object sender, EventArgs e)
        {
            //check if fill isnot empty
            if (String.IsNullOrEmpty(clientFirstNameTextBox.Text) || String.IsNullOrEmpty(clientLastNameTextBox.Text) || String.IsNullOrEmpty(clientCardInformationTextBox.Text))
            {
                notification.Text = "Some Field are Empty!";
                return;


            }
            else
            {
               // set the client objet
                client.ClientFirstName = clientFirstNameTextBox.Text;
                client.ClientLastName = clientLastNameTextBox.Text;
                client.ClientCardInfo = clientCardInformationTextBox.Text;
            }


            //make a booking
            //hotelService.getBookingInformaton(1, 1, 1, client.ClientFirstName, client.ClientLastName, client.ClientCardInfo);

            String url = "api/hotels/" + client.HotelName + "/rooms/"+client.RoomId;
            Console.WriteLine(url);
            HttpResponseMessage message = await restClient.GetAsync(url);
         //   message.EnsureSuccessStatusCode();
         //   String result = await message.Content.ReadAsStringAsync();

          //  var res = JsonConvert.DeserializeObject<String>(result);
            notification.Visible = true;

            // notification.Text = res;
            notification.Text = "Booking Successful Thanks";
            //hide notification panel
            var timer = new Timer();
            timer.Interval = 3000;

            timer.Tick += (s, d) => {
                notification.Visible = false;
                timer.Stop();
            };

            timer.Start();


            tableHotelInformation.Visible = false;
            listHotelGrid.Visible = false;
            ClientInformationLayoutPanel.Visible = false;
            roomDataGridView.Visible = false;

        }
    }

    }
