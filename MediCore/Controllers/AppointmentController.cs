using Application.Data_TransferModels.AppointmentDTO;
using Application.Interface.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Patient,Doctor,Receptionist")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _appointmentService;
        public AppointmentController(IAppointment appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Route("RegisterAppointment")]
        public async Task<ActionResult> CreateAppointment([FromBody] RegisterAppointmentDTO model)
        {
            var result = await _appointmentService.BookAppointment(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllAppointment")]
        public async Task<ActionResult> GetAllAppointment()
        {
            var result = await _appointmentService.GetAllAppointment();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAppointmentbyId/{id:int}")]
        public async Task<ActionResult> GetAppointmentbyId(int id)
        {
            var result = await _appointmentService.GetAppointmentbyId(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateStatusofAppointment/{id:int}")]
        public async Task<ActionResult> UpdateStatusOfAppointment(int id, [FromBody] AppointmentStatusUpdateDTO model)
        {
            var result = await _appointmentService.UpdateStatusofAppointment(id, model);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteAppointment/{id:int}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            var result = await _appointmentService.CancelAppointment(id);
            return Ok(result);
        }
    }
}
