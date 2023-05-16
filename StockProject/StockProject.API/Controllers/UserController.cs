using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockProject.Entities.Entities;
using StockProject.Service.Abstract;
using System.Data;

namespace StockProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericService<User> service;

        public UserController(IGenericService<User> service)
        {
            this.service = service;
        }
        //GET: api/User/GetAllUsers
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(service.GetAll());
        }
        //GET: api/User/GetAllActive
        [HttpGet]

        public IActionResult GetAllActive()
        {
            return Ok(service.GetActive());
        }

        //GET: api/User/GetUserById
        [HttpGet("{id}")]

        public IActionResult GetUserById(int id)
        {
            return Ok(service.GetById(id));
        }

        //GET: api/User/GetUserByName
        [HttpGet("{name}")]

        public IActionResult GetUserByName(string name)
        {
            return Ok(service.GetDefault(x => x.FirstName.ToLower().Contains(name.ToLower()) == true).ToList());
        }
        [HttpPost]

        //POST: api/User/CreateUser
        public IActionResult CreateUser([FromQuery] User supplier)
        {
            service.Add(supplier);

            return CreatedAtAction("GetUserById", new { id = supplier.Id }, supplier);
        }

        //PUT: api/User/UpdateUser
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User supplier)
        {
            if (id != supplier.Id)
                return BadRequest("Idler eşleşmiyor.Kontrol edip tekrar deneyiniz!");

            try
            {
                service.Update(supplier);
                return Ok(supplier);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!service.Any(x => x.Id == id))
                {
                    return NotFound("Böyle bir supplier bulunmamaktadır!");
                }
            }
            return NoContent();

            #region MyRegion
            //else if(!service.Any(x => x.Id == id))
            //{
            //    return NotFound("Böyle bir supplier bulunmamaktadır!");
            //}
            //else
            //{
            //    service.Update(category);
            //    return Ok(category);
            //}
            #endregion



        }
        //Delete: api/User/DeleteUser
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var supplier = service.GetById(id);
            if (supplier == null)
                return NotFound();
            try
            {
                service.Remove(supplier);
                return Ok("Kategori silindi");
            }
            catch (DeletedRowInaccessibleException)
            {

                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public IActionResult AvtivateUser(int id)
        {
            var supplier = service.GetById(id);
            if (supplier == null)
                return NotFound();
            try
            {
                service.Activate(id);
                return Ok(supplier);
            }
            catch (DeletedRowInaccessibleException)
            {

                return BadRequest();
            }

        }
    }
}
