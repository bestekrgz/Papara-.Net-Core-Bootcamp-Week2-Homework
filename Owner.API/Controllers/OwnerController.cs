using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Owner.API.Data;
using Owner.API.Model;


namespace Owner.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {

        // GET 
        [Route("All")]
        [HttpGet]
        public IActionResult GetOwners()
        {
            var ownersDatas = new OwnerData().GetAll(); // Get all owners from owner data
            return Ok(ownersDatas);
        }
        //POST
        [Route("Create")]
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CreateOwner(OwnerModel owner)
        {
            if (owner.Description.ToLower().Contains("hack"))
                throw new BadHttpRequestException("You cannot add a description containing hack keyword");  // Description check
            else
            {
                var ownersDatas = new OwnerData().GetAll();
                ownersDatas.Add(owner); // Create new owner 
                return Ok(ownersDatas);
            }
        }
        //PUT
        [HttpPut("{id:int}")]
        public IActionResult UpdateOwner(int id,OwnerModel owner)
        {
            if (id != owner.Id)
            {
                throw new BadHttpRequestException("Owner id did not match.");
            }
            else if (owner.Description.ToLower().Contains("hack"))
            {
                throw new BadHttpRequestException("You cannot update a description containing hack keyword"); // Description check
            }
            else
            {
                var ownersDatas = new OwnerData().GetAll();
                var ownerId = ownersDatas.FirstOrDefault(x => x.Id == id);
                ownerId.Name = owner.Name;    // Update owner properties
                ownerId.Surname = owner.Surname;
                ownerId.Description = owner.Description;
                ownerId.Date = owner.Date;
                ownerId.Type = owner.Type;
                return Ok(ownersDatas);
                
            }
           

        }

        //DELETE
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOwner(int id)
        {
            var ownersDatas = new OwnerData().GetAll();
            var ownerId = ownersDatas.FirstOrDefault(x => x.Id == id);
            if (ownerId==null) // id check
            {
                throw new BadHttpRequestException("Owner's id did not found.");

            }
            else
            {
                ownersDatas.Remove(ownerId);  // Deleted owner
                return Ok("Owner deleted successfully.");
            }
        }

    }
}

