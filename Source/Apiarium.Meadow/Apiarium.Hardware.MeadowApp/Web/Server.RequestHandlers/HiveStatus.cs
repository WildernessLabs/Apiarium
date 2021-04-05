using System;
using Meadow.Foundation.Web.Maple.Server;
using Meadow.Foundation.Web.Maple.Server.Routing;

namespace Apiarium.Hardware.MeadowApp.Web.Server.RequestHandlers
{
    public class HiveMonitorRequestHandlers : RequestHandlerBase
    {
        [HttpGet]
        public void Hello()
        {
            Console.WriteLine("GET::Hello");

            this.Context.Response.ContentType = ContentTypes.Application_Text;
            this.Context.Response.StatusCode = 200;
            this.Send("hello world").Wait();
        }

        [HttpGet]
        public void HiveStatus()
        {
            Console.WriteLine("GET::HiveStatus");

            this.Context.Response.ContentType = ContentTypes.Application_Text;
            this.Context.Response.StatusCode = 200;
            this.Send(HiveMonitor.Instance.LastKnownHiveStatus).Wait();
        }

    }
}
