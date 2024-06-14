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
        /// <param name="id">User ID.</param>
        /// <param name="name">User name.</param>
        /// <param name="email">User email.</param>
        /// <param name="role">User role.</param>
        /// <returns>Generated JWT token.</returns>
        public string GenerateJWT(int id, string name, string email, string role)
        {
            // Key converted into UTF8 format
            SymmetricSecurityKey objSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Convert UTF8 into hashing format
            SigningCredentials objSigningCredentials = new SigningCredentials(objSecurityKey, SecurityAlgorithms.HmacSha256);

            // Create claims
            List<Claim> lstClaims = new List<Claim>
            {
                new Claim("Id",id.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role),
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
