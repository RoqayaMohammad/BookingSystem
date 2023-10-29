using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class RoomController : Controller
    {
        public readonly IConfiguration _configuration;
        public RoomController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllRooms()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository(_configuration);
                HttpResponseMessage response = await serviceObj.GetResponse("api/Room");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                List<RoomDto> rooms = JsonConvert.DeserializeObject<List<RoomDto>>(content);
               
                ViewBag.Title = "All Rooms";
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
            HttpResponseMessage response = await serviceObj.GetResponse("api/branch/" + id.ToString());
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            RoomDto room = JsonConvert.DeserializeObject<RoomDto>(content);

            ViewBag.Title = "All Rooms";
            return View(room);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoomDto room)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.PostResponse("api/Room", room);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllRooms");
        }


        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {

            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.GetResponse($"api/Room/{id}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            RoomDto room = JsonConvert.DeserializeObject<RoomDto>(content);

            ViewBag.Title = "All Rooms";
            return View(room);
        }
        [HttpPost]
        public async Task<ActionResult> Update(RoomDto room)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.PutResponse("api/branch/" + room.Id, room);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllRooms");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.DeleteResponse("api/Room/" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllRooms");
        }
    }
}
