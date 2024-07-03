using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Filter.Business_Logic
{
    /// <summary>
    /// manage the authorization
    /// </summary>
    public class BLAuth
    {
        #region Private Member
        private readonly string _key = "thisisasecretkeythatuserscannotchangebythemselves";
        #endregion

        #region Public Method

        /// <summary>
        /// Generates a JWT token based on user information.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <param name="name">User name.</param>
        /// <param name="email">User email.</param>
        /// <param name="role">User role.</param>
        /// <returns>Generated JWT token.</returns>
        public string GenerateJWT(int id, string name, string role)
        {
            string issuer = "https://localhost:7290";

            // Key converted into UTF8 format
            SymmetricSecurityKey objSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            // Convert UTF8 into hashing format
            SigningCredentials objSigningCredentials = new SigningCredentials(objSecurityKey, SecurityAlgorithms.HmacSha256);

            // Create claims
            List<Claim> lstClaims = new List<Claim>
            {
                new Claim("Id",id.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
            };

            // Create token
            JwtSecurityToken token = new JwtSecurityToken(issuer,
                            issuer,
                            lstClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: objSigningCredentials);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }


        #endregion
    }

}
