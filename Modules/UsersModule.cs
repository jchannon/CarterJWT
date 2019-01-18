using Carter;
using Carter.ModelBinding;
using Carter.Response;
using CarterJWT.Entities;
using CarterJWT.Services;

namespace CarterJWT.Modules
{
    public class UsersModule : CarterModule
    {
        public UsersModule(IUserService userService)
        {
            this.RequiresAuthentication();

            this.Get("/", ctx => ctx.Response.AsJson(userService.GetAll()));
        }
    }

    public class AuthenticateModule : CarterModule
    {
        public AuthenticateModule(IUserService userService)
        {
            this.Post("/authenticate", ctx =>
            {
                var userParam = ctx.Request.Bind<User>();

                var user = userService.Authenticate(userParam.Username, userParam.Password);

                if (user == null)
                {
                    ctx.Response.StatusCode = 400;
                    return ctx.Response.AsJson(new {message = "Username or password is incorrect"});
                }

                return ctx.Response.AsJson(user);
            });
        }
    }
}