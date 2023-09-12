using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_51702017.Model;
using WebAPI_51702017.Respor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_51702017.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        // GET: api/<Products>
        [HttpGet]
        public ActionResult<IEnumerable<ProductsModel>> Get()
        {
            var result = ProductsRes.GetAllProduct();
            return new JsonResult(result);
            //return new string[] { "value1", "value2" };
        }

        [Route("type/{id}")]
        [HttpGet]
        public ActionResult<IEnumerable<ProductsModel>> GetWithType(string id)
        //public IEnumerable<string> GetWithType(string id)
        {
            var result = ProductsRes.getProductWithType(id);
            return new JsonResult(result);

        }

        // GET api/<Products>/5
        [HttpGet("{id}")]
        public ActionResult<ProductsModel> Get(string id)
        {
            var result = ProductsRes.getDetail(id);
            return new JsonResult(result);
        }

        // POST api/<Products>
        [Authorize]
        [HttpPost]
        public ActionResult<ProductsModel> Post(ProductsModel pro)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization);
            messageSend mess = new messageSend();
            if(userId == 10000 || userId == 11107)
            {
                var result = ProductsRes.CreateProducts(pro);
                if (result.ProductID != "")
                {
                    mess.message = "Thêm sản phẩm thành công";
                    mess.codeRes = 200;
                    mess.statusRes = true;
                    mess.objAcc = pro;
                    return new JsonResult(mess);
                }
                else
                {
                    mess.message = "Thêm sản phẩm thất bại";
                    mess.codeRes = 500;
                    mess.statusRes = false;
                    mess.objAcc = null;
                    return new JsonResult(mess);
                }

            }
            mess.message = "Bạn Không thể thêm sản phẩm";
            mess.codeRes = 404;
            mess.statusRes = false;
            mess.objAcc = null;
            return new JsonResult(mess);
        }

        // PUT api/<Products>/5
        [Authorize]
        [HttpPut("{id}")]

        public ActionResult<ProductsModel> Put( string id , ProductsModel pro)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization);
            messageSend mess = new messageSend();
            if (userId == 10000 || userId == 11107)
            {
                pro.ProductID = id;
                var result = ProductsRes.EditProducts(pro);
                if (result == true)
                {
                    mess.message = "Cập nhật sản phẩm thành công";
                    mess.codeRes = 200;
                    mess.statusRes = true;
                    mess.objAcc = pro;
                    return new JsonResult(mess);
                }
                mess.message = "Cập nhật sản phẩm thất bại";
                mess.codeRes = 500;
                mess.statusRes = false;
                return new JsonResult(mess);
            }
            mess.message = "Bạn Không thể sửa sản phẩm";
            mess.codeRes = 404;
            mess.statusRes = false;
            mess.objAcc = null;
            return new JsonResult(mess);
        }

        // DELETE api/<Products>/5
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<ProductsModel> Delete(string id)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization);
            messageSend mess = new messageSend();
            if (userId == 10000 || userId == 11107)
            {
                var result = ProductsRes.DeleteProducts(id);
                if (result == true)
                {
                    mess.message = "Xóa sản phẩm thành công";
                    mess.codeRes = 200;
                    mess.statusRes = true;
                    return new JsonResult(mess);
                }
                mess.message = "Xóa sản phẩm thất bại";
                mess.statusRes = false;
                mess.codeRes = 500;
                return new JsonResult(mess);
            }
            mess.message = "Bạn Không thể xóa sản phẩm";
            mess.codeRes = 404;
            mess.statusRes = false;
            mess.objAcc = null;
            return new JsonResult(mess);
        }
    }
}
