using CAIT.SQLHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAPI_51702017.Const;
using WebAPI_51702017.Model;

namespace WebAPI_51702017.Respor
{
    public static class AccountRes
    {
        private const string EncryptionKey = "51702017NGOMINHHIEU";
        public static AccountModel Login(string username , string password)
        {
            var hashPass = Encrypt(password);
            object[] value = { username , hashPass };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Account_Login", value);
            if(connection.errorCode ==0 && result.Rows.Count >0)
            {
                AccountModel acc = new AccountModel();
                var dr = result.Rows[0];
                acc.accountId = string.IsNullOrEmpty(dr["accountID"].ToString()) ? 0 : int.Parse(dr["accountID"].ToString());
                acc.userName = dr["userName"].ToString();
                //acc.password = dr["password"].ToString();
                acc.name = dr["name"].ToString();
                acc.email = dr["email"].ToString();
                acc.cost = string.IsNullOrEmpty(dr["cost"].ToString()) ? 0 : int.Parse(dr["cost"].ToString());
                acc.phone = dr["phone"].ToString();
                acc.address = dr["address"].ToString();
                acc.role = string.IsNullOrEmpty(dr["role"].ToString()) ? 0 : int.Parse(dr["role"].ToString());
                return acc;
            }
            return new AccountModel();
        }
        public static List<AccountModel> getAllUser()
        {
            object[] value = { };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Account_getall", value);
            List<AccountModel> listAcc = new List<AccountModel>();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                foreach (DataRow dr in result.Rows)
                {
                    AccountModel acc = new AccountModel();
                    acc.accountId = string.IsNullOrEmpty(dr["accountID"].ToString()) ? 0 : int.Parse(dr["accountID"].ToString());
                    acc.userName = dr["userName"].ToString();
                    //acc.password = dr["password"].ToString();
                    acc.name = dr["name"].ToString();
                    acc.email = dr["email"].ToString();
                    acc.cost = string.IsNullOrEmpty(dr["cost"].ToString()) ? 0 : int.Parse(dr["cost"].ToString());
                    acc.phone = dr["phone"].ToString();
                    acc.address = dr["address"].ToString();
                    acc.role = string.IsNullOrEmpty(dr["role"].ToString()) ? 0 : int.Parse(dr["role"].ToString());
                    listAcc.Add(acc);
                }
            }
            return listAcc;
        }
        public static AccountModel EditAccount(AccountModel user)
        {
            object[] value = { user.accountId  ,user.name , user.address, user.phone};
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Account_editProfile", value);
            AccountModel acc = new AccountModel();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var dr = result.Rows[0];
                acc.accountId = string.IsNullOrEmpty(dr["accountID"].ToString()) ? 0 : int.Parse(dr["accountID"].ToString());
                acc.userName = dr["userName"].ToString();
                //acc.password = dr["password"].ToString();
                acc.name = dr["name"].ToString();
                acc.email = dr["email"].ToString();
                acc.cost = string.IsNullOrEmpty(dr["cost"].ToString()) ? 0 : int.Parse(dr["cost"].ToString());
                acc.phone = dr["phone"].ToString();
                acc.address = dr["address"].ToString();
                acc.role = string.IsNullOrEmpty(dr["role"].ToString()) ? 0 : int.Parse(dr["role"].ToString());
                return acc;
            } 
            return acc;
        }
        public static bool changePass(ChangepassModel model)
        {
            var hashOldPass = Encrypt(model.oldPassword);
            var hashNewPass = Encrypt(model.newPassword);
            object[] value = { model.accountId, hashOldPass, hashNewPass };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Account_changePass", value);
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                return true;
            }
            return false;
         }
        public static int getIdToken(string authorization)
        {
            int id = 0;
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                // we have a valid AuthenticationHeaderValue that has the following details:

                var scheme = headerValue.Scheme;
                var parameter = headerValue.Parameter;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(parameter);
                var name = jwtSecurityToken.Claims.First(claim => claim.Type == "name").Value;
                var nameid = jwtSecurityToken.Claims.First(claim => claim.Type == "nameid").Value;
                id = Int32.Parse(nameid);
                return id ;
                // scheme will be "Bearer"
                // parmameter will be the token itself.
            }
            return id;
        }

        public static AccountModel Register(AccountModel user)
        {
            string hasPass = Encrypt(user.password);
            object[] value = {user.userName, hasPass, user.name, user.email , user.cost , user.phone , user.address  , user.role };
            SQLCommand connection = new SQLCommand(constValue.ConnectionString);
            DataTable result = connection.Select("Account_createAccount", value);
            AccountModel acc = new AccountModel();
            if (connection.errorCode == 0 && result.Rows.Count > 0)
            {
                var dr = result.Rows[0];
                acc.accountId = string.IsNullOrEmpty(dr["accountID"].ToString()) ? 0 : int.Parse(dr["accountID"].ToString());
                acc.userName = dr["userName"].ToString();
                //acc.password = dr["password"].ToString();
                acc.name = dr["name"].ToString();
                acc.email = dr["email"].ToString();
                acc.cost = string.IsNullOrEmpty(dr["cost"].ToString()) ? 0 : int.Parse(dr["cost"].ToString());
                acc.phone = dr["phone"].ToString();
                acc.address = dr["address"].ToString();
                acc.role = string.IsNullOrEmpty(dr["role"].ToString()) ? 0 : int.Parse(dr["role"].ToString());
                return acc;
            }
            //return new AccountModel();
            return acc;
        }

        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                //Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
