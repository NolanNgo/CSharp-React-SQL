using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI_51702017.Model;
using WebAPI_51702017.Respor;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_51702017.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Token : ControllerBase
    {
        // GET: api/<Token>
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<AccountModel>> Get()
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            int userId = AccountRes.getIdToken(authorization);
            messageSend mess = new messageSend();
            if (userId == 10000 || userId == 11107)
            { 
                var result = AccountRes.getAllUser();
                return new JsonResult(result);
            }
            mess.message = "Bạn Không xem tài khoản người dùng ";
            mess.codeRes = 404;
            mess.statusRes = false;
            mess.objAcc = null;
            return new JsonResult(mess);
            //getAllUser
        }

        public IConfiguration _configuration;

        public Token(IConfiguration config)
        {
            _configuration = config;
        }
        public JwtSecurityToken createToken(string id , string name)
        {
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.NameId, id),
                    new Claim(JwtRegisteredClaimNames.Name, name),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
            return token;
        }

        [HttpPost]
        public IActionResult Post(AccountModel _userData)
        {
            var result = AccountRes.Login(_userData.userName, _userData.password);
            messageSend mess = new messageSend();
            //if (_userData.UserName == "admin" && _userData.Password == "123")
            if (!string.IsNullOrEmpty(result.userName))
            {
                string id = result.accountId.ToString();
                string name = result.userName.ToString();
                var token = createToken(id, name);
                mess.message = "Đăng nhập Thành công";
                mess.statusRes = true;
                mess.codeRes = 200;
                mess.objAcc = result;
                mess.jwtoken = new JwtSecurityTokenHandler().WriteToken(token);
                return new JsonResult(mess);
                //return Ok(new JwtSecurityTokenHandler().WriteToken(token));

            }
            else
            {
                mess.message = "Đăng Nhập Thất Bại";
                mess.statusRes = false;
                mess.codeRes = 500;
                return new JsonResult(mess);
            }
        }
        [Route("Register")]
        [HttpPost]
        public IActionResult PostRegister(AccountModel user)
        {
            messageSend mess = new messageSend();
            var result = AccountRes.Register(user);
            if (result.accountId != 0)
            {
                string a = result.accountId.ToString();
                string b = result.userName.ToString();
                var token = createToken(a, b);
                mess.message = "Đăng ký Thành công";
                mess.codeRes = 200;
                mess.objAcc = result;
                mess.statusRes = true;
                mess.jwtoken = new JwtSecurityTokenHandler().WriteToken(token);
                return new JsonResult(mess);
            }
            mess.message = "Đăng ký Thật Bại";
            mess.codeRes = 404;
            mess.statusRes = false;
            mess.objAcc = null;
            return new JsonResult(mess);
        }
    }
}
