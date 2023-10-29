using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class CustomerController : Controller
    {
        public readonly IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository(_configuration);
                HttpResponseMessage response = await serviceObj.GetResponse("api/Customer");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                List<CustomerDto> rooms = JsonConvert.DeserializeObject<List<CustomerDto>>(content);

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
            HttpResponseMessage response = await serviceObj.GetResponse("api/Customer/" + id.ToString());
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            CustomerDto room = JsonConvert.DeserializeObject<CustomerDto>(content);

            ViewBag.Title = "All Booked Rooms";
            return View(room);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CustomerDto booking)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.PostResponse("api/Customer", booking);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllCustomers");
        }


        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {

            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.GetResponse($"api/Customer/{id}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            CustomerDto room = JsonConvert.DeserializeObject<CustomerDto>(content);

            ViewBag.Title = "All Booked Rooms";
            return View(room);
        }
        [HttpPost]
        public async Task<ActionResult> Update(BookingDto room)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.PutResponse("api/Customer/" + room.Id, room);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllCustomers");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.DeleteResponse("api/Customer/" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllCustomers");
        }
    }
}
