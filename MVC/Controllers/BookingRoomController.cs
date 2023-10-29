using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class BookingRoomController : Controller
    {
        public readonly IConfiguration _configuration;
        public BookingRoomController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllBookedRooms()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository(_configuration);
                HttpResponseMessage response = await serviceObj.GetResponse("api/BookingRoom");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                List<BookingRoomDto> rooms = JsonConvert.DeserializeObject<List<BookingRoomDto>>(content);

                ViewBag.Title = "All Booked Rooms";
                return View(rooms);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetById(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.GetResponse("api/BookingRoom/" + id.ToString());
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            BookingRoomDto room = JsonConvert.DeserializeObject<BookingRoomDto>(content);

            ViewBag.Title = "All Booked Rooms";
            return View(room);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BookingRoomDto booking)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.PostResponse("api/BookingRoom", booking);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBookedRooms");
        }


        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {

            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.GetResponse($"api/BookingRoom/{id}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            BookingRoomDto room = JsonConvert.DeserializeObject<BookingRoomDto>(content);

            ViewBag.Title = "All Booked Rooms";
            return View(room);
        }
        [HttpPost]
        public async Task<ActionResult> Update(BookingDto room)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.PutResponse("api/BookingRoom/" + room.Id, room);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBookedRooms");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.DeleteResponse("api/BookingRoom/" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBookedRooms");
        }
    }
}
