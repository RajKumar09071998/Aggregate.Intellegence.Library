using Aggregate.Intellegence.Library.Web.Service.AppDBContext;
using Aggregate.Intellegence.Library.Web.Service.Helpers;
using Aggregate.Intellegence.Library.Web.Service.Interfaces;
using Aggregate.Intellegence.Library.Web.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aggregate.Intellegence.Library.Web.Service.Repository
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly ApplicationDBContext dBContext;
        public string usedGenaratesTokenKey { get; }
        public AuthenticateService(ApplicationDBContext dBContext, string usedGenaratesTokenKey)
        {
            this.dBContext = dBContext;
            this.usedGenaratesTokenKey = usedGenaratesTokenKey;
        }
        public async Task<bool> RegisterUser(RegisterUser registerUser)
        {
            if (registerUser.Id == 0 || registerUser.Id == null)
            {
                //create user 
                if (!string.IsNullOrEmpty(registerUser.Password))
                {
                    HashSalt hashSalt = HashSalt.GenerateSaltedHash(registerUser.Password);
                    User user = new User();

                    if (registerUser.Id.HasValue)
                        user.Id = registerUser.Id.Value;

                    user.FirstName = registerUser.FirstName;
                    user.LastName = registerUser.LastName;
                    user.RoleId = registerUser.RoleId;
                    user.Email = registerUser.Email;
                    user.Phone = registerUser.Phone;
                    user.PasswordHash = hashSalt.Hash;
                    user.PasswordSalt = hashSalt.Salt;
                    user.CreatedBy = -1;
                    user.CreatedOn = DateTimeOffset.Now;
                    user.ModifiedBy = -1;
                    user.ModifiedOn = DateTimeOffset.Now;
                    user.IsActive = true;
                    await dBContext.users.AddAsync(user);
                }
                else
                {
                    return false;
                }
            }
            else
            {

            }
            var responce = await dBContext.SaveChangesAsync();

            return responce == 1 ? true : false;
        }

        public async Task<AuthenticationResponce> AuthenticateUser(UserLoginVM login)
        {
            AuthenticationResponce responce = new AuthenticationResponce();

            if (login != null)
            {
                User dbUser = await dBContext.users.Where(x => x.Email.ToLower().Trim() == login.username.ToLower().Trim() || x.Phone == login.username.Trim()).FirstOrDefaultAsync();

                if (dbUser != null)
                {
                    if (dbUser.IsActive)
                    {
                        var isValidUserpassword = HashSalt.VerifyPassword(login.password, dbUser.PasswordHash, dbUser.PasswordSalt);
                        if (isValidUserpassword)
                        {
                            var jwttokenHandler = new JwtSecurityTokenHandler();

                            var tokenkey = Encoding.ASCII.GetBytes(usedGenaratesTokenKey);

                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                      new Claim(ClaimTypes.Name, login.username)

                                }),
                                Expires = DateTime.UtcNow.AddHours(1),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)
                            };

                            var token = jwttokenHandler.CreateToken(tokenDescriptor);

                            var writtoken = jwttokenHandler.WriteToken(token);

                            responce = new AuthenticationResponce();
                            responce.JwtToken = writtoken;
                            responce.IsActive = true;
                            responce.ValidUser = true;
                            responce.ValidPassword = true;
                            responce.StatusMessage = "";
                            responce.StatusCode = "200";
                        }
                        else
                        {
                            responce = new AuthenticationResponce();
                            responce.JwtToken = string.Empty;
                            responce.IsActive = false;
                            responce.ValidUser = true;
                            responce.ValidPassword = false;
                            responce.StatusMessage = "User given password wrong,Please check and try again";
                            responce.StatusCode = "Invalid password";
                        }
                    }
                    else
                    {
                        responce = new AuthenticationResponce();
                        responce.JwtToken = string.Empty;
                        responce.IsActive = false;
                        responce.ValidUser = false;
                        responce.ValidPassword = false;
                        responce.StatusMessage = "Given username is inactive,please contact admin or your manager";
                        responce.StatusCode = "user was inactive";
                    }
                }
                else
                {
                    responce = new AuthenticationResponce();
                    responce.JwtToken = string.Empty;
                    responce.IsActive = false;
                    responce.ValidUser = false;
                    responce.ValidPassword = false;
                    responce.StatusMessage = "User not found with given username and password,Please check and try again";
                    responce.StatusCode = "NOt found";
                }
            }
            return responce;
        }

        public async Task<ApplicationUser> GenarateUserClaims(AuthenticationResponce authenticationResponce)
        {
            var tokenkey = Encoding.ASCII.GetBytes(usedGenaratesTokenKey);
            var tokhand = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principle = tokhand.ValidateToken(authenticationResponce.JwtToken,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenkey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out securityToken);

            var jwttoken = securityToken as JwtSecurityToken;
            if (jwttoken != null && jwttoken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                string Username = principle.Identity.Name;
                User user = dBContext.users.Where(x => (x.Email == Username || x.Phone == Username) && x.IsActive).FirstOrDefault();
                if (user != null)
                {
                    ApplicationUser app = new ApplicationUser();
                    app.Id = user.Id;
                    app.Email = user.Email;
                    app.FirstName = user.FirstName;
                    app.LastName = user.LastName;
                    app.Phone = user.Phone;
                    app.RoleId = user.RoleId;
                    return app;
                }

                return null;

            }
            return null;
        }
        public async Task<List<User>> FetchAllUsers()
        {
            return await dBContext.users.Where(x => x.IsActive).ToListAsync();
        }
    }
}
