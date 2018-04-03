using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using WepApiUserRegistrationAngular.Models;

namespace WepApiUserRegistrationAngular.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(AccountModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            manager.PasswordValidator = new PasswordValidator { RequiredLength = 3 };
            IdentityResult result = manager.Create(user, model.Password);
            return result;
        }

        [HttpGet]
        [Route("api/GetUserClaims")]
        public AccountModel GetUserClaims()
        {
            var identityClass = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClass.Claims;
            AccountModel model = new AccountModel()
            {
                UserName = identityClass.FindFirst("Username").Value,
                Email = identityClass.FindFirst("Email").Value,
                FirstName = identityClass.FindFirst("FirstName").Value,
                LastName = identityClass.FindFirst("LastName").Value,
                LoggedOn = identityClass.FindFirst("LoggedOn").Value,
            };
            return model;
        }
    }
}
