using Application.Data_TransferModels.PatientDTO;
using Application.Interface.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Doctor,Receptionist")]
    public class PatientController : ControllerBase
    {
        private readonly IPatient _patientServices;
        public PatientController(IPatient patientServices)
        {
            _patientServices = patientServices;
        }

        [HttpPost]
        [Route("CreatePatient")]
        public async Task<ActionResult> CreatePatient([FromBody] RegisterPatientDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _patientServices.CreatePatient(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllPatient")]
        public async Task<ActionResult> GetAllPatient()
        {
            var result = await _patientServices.GetAllPatient();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetPatientbyId/{id:int}")]
        public async Task<ActionResult> GetPatientbyId(int id)
        {
            var result = await _patientServices.GetPatientbyId(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdatePatient/{id:int}")]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] RegisterPatientDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _patientServices.UpdatePatient(id, model);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeltePatient")]
        public async Task<ActionResult> DeltePatient(int id)
        {
            var result = await _patientServices.DeletePatient(id);
            return Ok(result);
        }
    }
}
