using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_51702017.Respor;
using WebAPI_51702017.Model;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_51702017.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        // GET: api/<Account>
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET api/<Account>/5
        [Route("changePass")]
        [HttpPut()]
        public IActionResult Put(ChangepassModel model)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            var id = AccountRes.getIdToken(authorization);
            model.accountId = id;
            var result = AccountRes.changePass(model);
            if (result == true)
            {
                messageSend mess = new messageSend();
                mess.statusRes = true;
                mess.message = "Đổi Mật Khẩu Thành công";
                mess.codeRes = 200;
                mess.objAcc = null;
                return new JsonResult(mess);
            }
            else
            {
                messageSend mess = new messageSend();
                mess.message = "Đổi Mật Khẩu Thật Bại";
                mess.statusRes = false;
                mess.codeRes = 500;
                mess.objAcc = null;
                return new JsonResult(mess);
            }

        }

        // POST api/<Account>
        //[HttpPost]
        //public IActionResult Post(AccountModel user)
        //{
        //    messageSend mess = new messageSend();
        //    var result = AccountRes.Register(user);
        //    string token = 
        //    if (result.accountId != 0)
        //    {
        //        mess.message = "Đăng ký Thành công";
        //        mess.codeRes = 200;
        //        mess.objAcc = result;
        //        return new JsonResult(mess);
        //    }
        //    mess.message = "Đăng ký Thật Bại";
        //    mess.codeRes = 404;
        //    mess.objAcc = null;
        //    return new JsonResult(mess);
        //}

        // PUT api/<Account>/5
        [HttpPut]
        public IActionResult Put(AccountModel user)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            var id  = AccountRes.getIdToken(authorization);
            messageSend mess = new messageSend();
            user.accountId = id;
            var result = AccountRes.EditAccount(user);
            if(result.accountId != 0)
            {
                mess.codeRes = 200;
                mess.message = "Cập nhật thành công";
                mess.objAcc = result;
                mess.statusRes = true;
                return new JsonResult(mess);
            }
            else
            {
                mess.codeRes = 500;
                mess.statusRes = false;
                mess.message = "Cập nhật thất bại";
                return new JsonResult(mess);
            }

        }

        // DELETE api/<Account>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
