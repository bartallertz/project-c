using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;

namespace projectC.JWT
{
    public class JWTValidator
    {
        public static int TokenValidation(string TokenString)
        {
            while (TokenString.Length % 4 != 0)
            {
                TokenString = TokenString + "=";
            }
            
            byte[] data = Convert.FromBase64String(TokenString);
            string decodedString = Encoding.UTF8.GetString(data);
            int USER_EYE_DEE = Convert.ToInt32(decodedString.Split(',')[1].Split(':')[1].Replace("\"",""));

            return USER_EYE_DEE;
        }
    }
}