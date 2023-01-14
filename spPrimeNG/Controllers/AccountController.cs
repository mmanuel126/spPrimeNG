using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sportprofiles.Repository;
using Swashbuckle.AspNetCore.Annotations;
using sportprofiles.Models.Account;

namespace sportprofiles.Controllers
{
    [Route("services/[controller]")]
    [SwaggerTag("This is a list of interfaces containing member account functionalities such as registering and loging in users.")]
    public class AccountController : Controller
    {
        readonly IConfiguration configuration;
        readonly IMemberRepository _memberRepo;
        readonly ILoggingRepository _loggingRepo;

        public AccountController(IConfiguration configuration, IMemberRepository memberRepository,
                            ILoggingRepository loggingRepository)
        {
            this.configuration = configuration;
            _memberRepo = memberRepository;
            _loggingRepo = loggingRepository;
        }

        /// <summary>
        /// creates JWT token.
        /// </summary>
        /// <param name="loginModel">The login credentials data.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public UserModel CreateToken([FromBody] LoginModel loginModel)
        {
            UserModel user = new UserModel();
            if (ModelState.IsValid)
            {
                var loginResult = _memberRepo.AuthenticateUser(loginModel.Email, loginModel.Password, "", "");
                if (loginResult != "")
                {
                    user.Email = loginResult.Split("~")[1];
                    user.MemberID = loginResult.Split("~")[0];
                    user.PicturePath = loginResult.Split("~")[2];
                    var jwt = GetToken(user);
                    user.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
                    user.ExpiredDate = jwt.ValidTo;
                    user.Name = loginResult.Split("~")[3];
                    user.Title = loginResult.Split("~")[4];
                    user.CurrentStatus = loginResult.Split("~")[5];
                }
            }
            return user;
        }

        /// <summary>
        /// Login new registered user.
        /// </summary>
        /// <param name="body">The data model for the new registered user.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("loginNewRegisteredUser")]
        public UserModel LoginNewRegisteredUser([FromBody] NewRegisteredUser body)
        {
            UserModel user = new UserModel();
            if (ModelState.IsValid)
            {
                var loginResult = _memberRepo.AuthenticateNewRegisteredUser(body.Email, body.Code);
                if (loginResult != "")
                {
                    user.Email = loginResult.Split("~")[1];
                    user.MemberID = loginResult.Split("~")[0];
                    user.PicturePath = loginResult.Split("~")[2];
                    var jwt = GetToken(user);
                    user.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
                    user.ExpiredDate = jwt.ValidTo;
                    user.Name = loginResult.Split("~")[3];
                    user.Title = loginResult.Split("~")[4];
                    user.CurrentStatus = loginResult.Split("~")[5];
                }
            }
            return user;
        }

        /// <summary>
        /// Refreshes a given token.
        /// </summary>
        /// <param name="accessToken">the old token that is to be refreshed.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("refreshLogin")]
        public UserModel RefreshToken([FromQuery] string accessToken)
        {
            string token = accessToken;            
            var principal = GetPrincipalFromExpiredToken(token);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = new UserModel();
            var lst = _loggingRepo.FindByUniqueEmail(username);
            if (lst != null)
            {
                user.Email = lst[0].Email;
                user.MemberID = lst[0].MemberID;
                user.PicturePath = lst[0].Picturepath;
                var jwt = GetToken(user);
                user.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
                user.ExpiredDate = jwt.ValidTo;
            }
            return user;
        }

        /// <summary>
        /// Get principal from expired token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration.GetValue<String>("Jwt:Key"))),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="body">The user's information to register.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public String RegisterUser([FromBody] RegisterModel body, [FromQuery] string device)
        {
            return _memberRepo.RegisterUserToLG(body.FirstName, body.LastName, body.Email, body.Password, body.Day, body.Month, body.Year, body.Gender, body.ProfileType, device);
        }

        #region helper routines...

        private JwtSecurityToken GetToken(UserModel user)
        {
            var utcNow = DateTime.Now;
            var claims = new Claim[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.MemberID),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryVerySecretKey"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(this.configuration.GetValue<int>("Jwt:Lifetime")),
                audience: this.configuration.GetValue<String>("Jwt:Issuer"),
                issuer: this.configuration.GetValue<String>("Tokens:Issuer")
            );
            return jwt;
        }

        #endregion
        
    }
}
