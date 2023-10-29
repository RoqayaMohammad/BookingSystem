using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class BookingController : Controller
    {
        public readonly IConfiguration _configuration;
        public BookingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllBooking()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository(_configuration);
                HttpResponseMessage response = await serviceObj.GetResponse("api/Booking");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                List<BookingDto> rooms = JsonConvert.DeserializeObject<List<BookingDto>>(content);

                ViewBag.Title = "All Bookins";
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
            HttpResponseMessage response = await serviceObj.GetResponse("api/Booking/" + id.ToString());
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            BookingDto room = JsonConvert.DeserializeObject<BookingDto>(content);

            ViewBag.Title = "All Bookings";
            return View(room);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BookingDto booking)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.PostResponse("api/Booking", booking);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBooking"); 
        }


        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {

            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.GetResponse($"api/Booking/{id}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            BookingDto room = JsonConvert.DeserializeObject<BookingDto>(content);

            ViewBag.Title = "All Rooms";
            return View(room);
        }
        [HttpPost]
        public async Task<ActionResult> Update(BookingDto room)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.PutResponse("api/Booking/" + room.Id, room);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBooking");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.DeleteResponse("api/Booking/" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBooking");
        }
    }
}
