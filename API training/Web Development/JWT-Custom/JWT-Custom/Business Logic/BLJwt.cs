using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Custom.Business_Logic
{
    /// <summary>
    /// Class containing methods for generating and verifying JSON Web Tokens (JWTs).
    /// </summary>
    public class BLJwt
    {
       
        #region Private Member
        private readonly string _key = "sefiwhnekkin@1!(s@&*&#*h!(JHJeahjsdhfj";
        #endregion

        #region Public Method
        /// <summary>
        /// Generates a JWT token containing the specified name as a claim.
        /// </summary>
        /// <param name="name">The name to include in the token.</param>
        /// <returns>The generated JWT token.</returns>
        public string GenerateTWT(string name)
        {
            string issuer = "https://localhost:44375/";

            // key converted into utf8 format
            SymmetricSecurityKey objSymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            // convert utf8 into hashing format
            SigningCredentials objSigningCredentials = new SigningCredentials(objSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // create claim
            List<Claim> lstClaim = new List<Claim>()
            {
                new Claim("Name",name),
            };

            // create token
            JwtSecurityToken token = new JwtSecurityToken(issuer,
                issuer,
                lstClaim,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:objSigningCredentials);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        /// <summary>
        /// Verifies a JWT token and returns the value of the "Name" claim if valid, otherwise null.
        /// </summary>
        /// <param name="jwtToken">The JWT token to verify.</param>
        /// <returns>The value of the "Name" claim if the token is valid, otherwise null.</returns>
        public string VerifyToken(string jwtToken)
        {
            JwtSecurityTokenHandler objTokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters objTokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = "https://localhost:44375/",
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
            };

            try
            {
                ClaimsPrincipal principal = objTokenHandler.ValidateToken(jwtToken, objTokenValidationParameters,out SecurityToken validatedToken);
                return principal.FindFirst("Name")?.Value;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}