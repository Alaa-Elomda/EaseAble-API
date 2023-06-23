using AbilitySystem.BL;
using AbilitySystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AbilitySystem.API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersManager _ordersManager;

        public OrdersController(IOrdersManager ordersManager)
        {
            _ordersManager = ordersManager;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult<List<OrderReadDto>> GetAll()
        {
            return _ordersManager.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<OrderWithProductsReadDto> GetByIdWithProducts(int id)
        {
            var orderDto = _ordersManager.GetByIdWithProducts(id);
            if (orderDto is null)
            {
                return NotFound(new { Message = "No Order Found!!" });
            }
            return orderDto;
        }

        [HttpGet]
        [Route("user/{userId}")]
        //[Authorize(Roles = "User")] 
        public ActionResult<List<OrderByUserReadDto>> GetByUserWithProducts(string userId)
        {
            var orderDto = _ordersManager.GetByUserWithProducts(userId);
            if (orderDto is null)
            {
                return Ok(new { Message = "No Order Found!!" });
            }
            return orderDto;
        }

        [HttpPost]
        //[Authorize(Roles = "User")]
        public ActionResult Add(OrderAddDto orderDto)
        {
            _ordersManager.Add(orderDto);
            return CreatedAtAction(
                actionName: nameof(GetAll),
                value: new { Message = "Added Successfully" });
        }


        [HttpPatch]
        [Route("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, OrderEditDto orderDto)
        {
            if (orderDto.OrderId != id) return Ok(new { Message = "No Order Found!!" });

            _ordersManager.Edit(id, orderDto);
            return CreatedAtAction(
                actionName: nameof(GetByIdWithProducts),
                routeValues: new { id = orderDto.OrderId },
                value: new { message = "Updated Successfully" } );
        }


        [HttpDelete]
        [Route("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {

            bool isDeleted = _ordersManager.Delete(id);
            if (isDeleted)
            {
                return CreatedAtAction(
                    actionName: nameof(GetAll),
                    value: new { message = "Deleted Successfully" });
            }
            else
            {
                return Ok(new { message = "Entity not found" });
            }

        }

        [HttpGet]
        [Route("countall")]
        public float CountAll()
        {
            return _ordersManager.CountAll();
        }

        [HttpGet]
        [Route("countaccepted")]
        public float CountAccepted()
        {
            return _ordersManager.CountAccepted();
        }

        [HttpGet]
        [Route("countrejected")]
        public float CountRejected()
        {
            return _ordersManager.CountRejected();
        }

        [HttpGet]
        [Route("countpending")]
        public float CountPending()
        {
            return _ordersManager.CountPending();
        }
    }
}
