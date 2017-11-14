using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using Medusa.Service;

namespace Medusa.WebHost
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(new ServiceRoute("Skill", 
                new WebServiceHostFactory(), 
                typeof(SkillService)));
            RouteTable.Routes.Add(new ServiceRoute("Project",
                new WebServiceHostFactory(),
                typeof(ProjectService)));
            RouteTable.Routes.Add(new ServiceRoute("Server",
                new WebServiceHostFactory(),
                typeof(ServerService)));
            RouteTable.Routes.Add(new ServiceRoute("Member",
               new WebServiceHostFactory(),
               typeof(MemberService)));
            RouteTable.Routes.Add(new ServiceRoute("Login",
               new WebServiceHostFactory(),
               typeof(LoginService)));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}