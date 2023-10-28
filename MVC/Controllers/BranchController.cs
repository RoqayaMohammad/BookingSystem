using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;


namespace MVC.Controllers
{
    public class BranchController : Controller
    {
        public readonly IConfiguration _configuration;
        public BranchController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllBranches()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository(_configuration);
                HttpResponseMessage response = await serviceObj.GetResponse("api/branch");
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                 List < BranchDto > branches = JsonConvert.DeserializeObject<List<BranchDto>>(content);
               // List<BranchDto> branches =  response.Content.ReadAsStringAsync<List<BranchDto>>().Result;
                ViewBag.Title = "All Branches";
                return View(branches);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.GetResponse("api/branch/" + id.ToString());
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            BranchDto branch = JsonConvert.DeserializeObject<BranchDto>(content);

            ViewBag.Title = "All Branches";
            return View(branch);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BranchDto branch)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.PostResponse("api/branch", branch);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBranches");
        }


        [HttpGet]
        public async Task<ActionResult> EditBranch(int id)
        {
            
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.GetResponse($"api/branch/{id}" );
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            BranchDto branch = JsonConvert.DeserializeObject<BranchDto>(content);

            ViewBag.Title = "All Branches";
            return View(branch);
        }
        [HttpPost]
        public async Task<ActionResult> EditBranch(BranchDto branch)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = await serviceObj.PutResponse("api/branch/"+ branch.Id, branch);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBranches");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository(_configuration);
            HttpResponseMessage response = serviceObj.DeleteResponse("api/branch/" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllBranches");
        }
    }
}
