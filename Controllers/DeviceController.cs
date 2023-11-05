using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text;
using VideoConference.Data;
using VideoConference.Entities;

namespace VideoConference.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        public static DeviceDbContext _context;
        public DeviceController(DeviceDbContext deviceDbContext)
        {
            _context = deviceDbContext;
        }

        [HttpGet]
        [Route("GetDevice/{id}")]
        public async Task<ActionResult<Device>> GetDevice(string id)
        {
            try
            {
                var item = await _context.Devices.FindAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddDevice")]
        public async Task<ActionResult<Device>> AddDevice([FromBody] Device device)
        {
            try
            {
                string id = device.Id;

                if (id.Length != 6)
                {
                    throw new Exception("The device id is not correct");
                }

                string literals = id.Substring(0,2);
                string digits = id.Substring(2,4);

                foreach (var literal in literals)
                {
                    if(!Char.IsLetter(literal))
                    {
                        throw new Exception("The device id is not correct");
                    }
                }

                foreach (var digit in digits)
                {
                    if (!Char.IsDigit(digit))
                    {
                        throw new Exception("The device id is not correct");
                    }
                }

                device.Id = literals.ToUpper() + digits;

                StringBuilder sb = new StringBuilder();
                sb = sb.Append(DateTime.Now.Year);
                sb = sb.Append(DateTime.Now.Month);
                sb = sb.Append(DateTime.Now.Day);

                device.DateAdded = sb.ToString();

                var newItem = _context.Devices.AddAsync(device);

                _context.SaveChanges();

                var item = await _context.Devices.FindAsync(device.Id);

                if (item == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve device (Id:({device.Id})");
                }

                return CreatedAtAction(nameof(GetDevice), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveDevice/{id}")]
        public async Task<ActionResult<Device>> RemoveDevice(string id)
        {
            try
            {
                var item = await _context.Devices.FindAsync(id);

                if (item != null)
                {
                    _context.Devices.Remove(item);
                    await _context.SaveChangesAsync();
                }

                return Ok();

            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


    }
}
