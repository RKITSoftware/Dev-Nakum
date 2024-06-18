using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMediaAPI.BL
{
    /// <summary>
    /// Business Logic class for authentication and authorization using JWT tokens.
    /// </summary>
    public class BLAuth
    {
        #region Private Member
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public BLAuth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Generates a JWT token based on user information.
        /// </summary>
        /// <param name="E01F01">User ID.</param>
        /// <param name="E01F02">User name.</param>
        /// <param name="E01F03">User email.</param>
        /// <param name="E01F07">User role.</param>
        /// <returns>Generated JWT token.</returns>
        public string GenerateJWT(int E01F01, string E01F02, string E01F03, string E01F07)
        {
            // Key converted into UTF8 format
            SymmetricSecurityKey objSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Convert UTF8 into hashing format
            SigningCredentials objSigningCredentials = new SigningCredentials(objSecurityKey, SecurityAlgorithms.HmacSha256);

            // Create claims
            List<Claim> lstClaims = new List<Claim>
            {
                new Claim("Id",E01F01.ToString()),
                new Claim(ClaimTypes.Name, E01F02),
                new Claim(ClaimTypes.Email, E01F03),
                new Claim(ClaimTypes.Role, E01F07),
            };  

            // Create token
            JwtSecurityToken token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                            _configuration["Jwt:Issuer"],
                            lstClaims,
                            expires: DateTime.Now.AddDays(7),
                            signingCredentials: objSigningCredentials);

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }

       
        #endregion
    }
}
