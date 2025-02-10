using Application.Data_TransferModels.MedicalRecordDTO;
using Application.Interface.MedicalRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor,Admin")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecord _medicalRecordServices;
        public MedicalRecordController(IMedicalRecord medicalRecordServices)
        {
            _medicalRecordServices = medicalRecordServices;
        }

        [HttpPost]
        [Route("CreateMedicalRecord")]
        public async Task<ActionResult> CreateMedicalRecord([FromBody] RegisterMedicalRecordDTO record)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _medicalRecordServices.CreateMedicalRecord(record);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetRecordofPatient/{id:int}")]
        public async Task<ActionResult> GetRecordofPatient(int id)
        {
            var result = await _medicalRecordServices.GetRecordofPatient(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetSpecificRecord/{id:int}")]
        public async Task<ActionResult> GetSpecificRecord(int id)
        {
            var result = await _medicalRecordServices.GetSpecificRecord(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateRecord/{id:int}")]
        public async Task<ActionResult> UpdateRecord(int id, [FromBody] UpdateMedicalRecordDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _medicalRecordServices.UpdateRecord(id, model);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteRecord/{id:int}")]
        public async Task<ActionResult> DeleteRecord(int id)
        {
            var result = await _medicalRecordServices.DeleteRecord(id);
            return Ok(result);
        }
    }
}
