using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_51702017.Model;
using WebAPI_51702017.Respor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_51702017.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order : ControllerBase
    {
        // GET: api/<Order>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        
        // GET api/<Order>/5
        [HttpGet("orderID/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<OrderModel>> Get(string id)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization);
            var result = OrdersRes.getOrder(id, userId);
            return new JsonResult(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<OrderModel>> Get()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization);;
            var result = OrdersRes.getOrderUserID(userId);
            return new JsonResult(result);
        }

        // POST api/<Order>
        [HttpPost]
        [Authorize]
        public ActionResult<OrderModel> Post(OrderModel order)
        {
            messageSend mess = new messageSend();
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization);
            order.accountId = userId;
            var result = OrdersRes.CreateOrders(order);
            if (result == true)
            {
                var result1 = OrdersRes.getOrder(order.orderId, userId);
                mess.codeRes = 200;
                mess.statusRes = true;
                mess.message = "Thêm hóa đơn thành công";
                mess.objAcc = result1;
                return new JsonResult(mess);
            }
            mess.codeRes = 500;
            mess.message = "Thêm hóa đơn thất bại";
            mess.statusRes = false;
            return new JsonResult(mess);
        }

        // PUT api/<Order>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<Order>/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<OrderModel> Delete(string id)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization); 
            var result = OrdersRes.DeleteOrders(id, userId);
            messageSend mess = new messageSend();
            if (result == true)
            {
                mess.codeRes = 200;
                mess.message = "Xóa hóa đơn thành công";
                mess.statusRes = true;
                mess.objAcc = null;
                return new JsonResult(mess);
            }
            mess.codeRes = 500;
            mess.statusRes = false;
            mess.message = "Xóa hóa đơn thất bại";
            return new JsonResult(mess);
        }

        [HttpDelete("{id}/product/{idPro}")]
        [Authorize]
        public ActionResult<OrderModel> DeleteProduct(string id, string idPro)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization);
            var result = OrdersRes.DeleteProductInOrder(id, userId, idPro);
            messageSend mess = new messageSend();
            if (result == true)
            {
                mess.codeRes = 200;
                mess.message = "Xóa sản phẩm trong hóa đơn thành công";
                mess.objAcc = null;
                mess.statusRes = true;
                return new JsonResult(mess);
            }
            mess.codeRes = 500;
            mess.statusRes = false;
            mess.message = "Xóa sản phẩm trong hóa đơn thất bại";
            return new JsonResult(mess);
        }
    }
}
