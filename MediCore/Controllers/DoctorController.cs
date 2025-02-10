using Application.Data_TransferModels.DoctorDTO;
using Application.Interface.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _doctorServices;
        public DoctorController(IDoctor doctorServices)
        {
            _doctorServices = doctorServices;
        }

        [HttpPost]
        [Route("CreateDoctor")]
        public async Task<ActionResult> CreateDoctor([FromBody] RegisterDoctorDTO model)
        {
            var result = await _doctorServices.RegisterDoctor(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllDoctors")]
        public async Task<ActionResult> GetAllDoctors()
        {
            var result = await _doctorServices.GetAllDoctors();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDoctorbyId")]
        public async Task<ActionResult> GetDoctorbyId(int id)
        {
            var result = await _doctorServices.GetDoctorbyID(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateDoctor/{id:int}")]
        public async Task<ActionResult> UpdateDoctor(int id, [FromBody] DoctorUpdateDTO model)
        {
            var result = await _doctorServices.UpdateDoctor(id, model);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteDoctor/{id:int}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var result = await _doctorServices.DeleteDoctor(id);
            return Ok(result);
        }
    }
}
