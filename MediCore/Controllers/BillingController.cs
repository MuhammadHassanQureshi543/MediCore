using Application.Data_TransferModels.BillingDTO;
using Application.Interface.Billing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Patient,Receptionist")]
    public class BillingController : ControllerBase
    {
        private readonly IBilling _billingServices;
        public BillingController(IBilling billingServices)
        {
            _billingServices = billingServices;
        }

        [HttpPost]
        [Route("GenerateBill")]
        public async Task<ActionResult> GenerateBill([FromBody] GenerateBillDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _billingServices.GenerateBill(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBills")]
        public async Task<ActionResult> GetAllBills()
        {
            var result = await _billingServices.GetAllBill();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetSpecificBill/{id:int}")]
        public async Task<ActionResult> GetSpecificBill(int id)
        {
            var result = await _billingServices.GetSpecificBill(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateStatusOfBill/{id:int}")]
        public async Task<ActionResult> UpdateStatusOfBill(int id, [FromBody] UpdateStatusBillDTO model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _billingServices.UpdateBillStatus(id, model);
            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteBill/{id:int}")]
        public async Task<ActionResult> DeleteBill(int id)
        {
            var result = await _billingServices.DeleteBill(id);
            return Ok(result);
        }
    }
}
