using System.Net;
using System.Text;
using System.Threading;
using MakoIoT.Device.Services.ConfigurationManager;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.Server.Extensions;
using MakoIoT.Device.Services.Server.WebServer;
using MakoIoT.Device.Utilities.String.Extensions;
using Microsoft.Extensions.Logging;
using nanoFramework.Json;

namespace MakoIoT.Device.Services.ConfigurationApi.Controllers
{
    public class ConfigController
    {
        private readonly ILogger _logger;
        private readonly IConfigManager _configManager;
        private readonly IConfigurationService _configService;  

        public ConfigController(ILogger logger, IConfigManager configManager, IConfigurationService configService)
        {
            _logger = logger;
            _configManager = configManager;
            _configService = configService;
        }

        [Route("config/sections")]
        [Method("GET")]
        public void GetSections(WebServerEventArgs e)
        {
            var sections = _configService.GetSections();
            var s = JsonConvert.SerializeObject(sections);
            e.Context.Response.ContentType = "application/json";
            e.Context.Response.StatusCode = (int)HttpStatusCode.OK;
            e.Context.Response.AddCors();
            MakoWebServer.OutPutStream(e.Context.Response, s);
        }

        [Route("config/section/{name}")]
        [Method("GET")]
        public void GetSection(string name, WebServerEventArgs e)
        {
            var section = _configService.LoadConfigSection(name);
            if (section == null)
            {
                MakoWebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.NotFound);
                return;
            }

            _logger.LogTrace($"{section.EscapeForInterpolation()}");

            e.Context.Response.ContentType = "application/json";
            e.Context.Response.StatusCode = (int)HttpStatusCode.OK;
            e.Context.Response.AddCors();
            MakoWebServer.OutPutStream(e.Context.Response, section);
        }

        [Route("config/section/{name}")]
        [Method("POST")]
        public void PostSection(string name, WebServerEventArgs e)
        {
            byte[] buffer = new byte[e.Context.Request.ContentLength64];
            e.Context.Request.InputStream.Read(buffer, 0, buffer.Length);
            string section = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

            _logger.LogTrace($"{section.EscapeForInterpolation()}");

            e.Context.Response.AddCors();
            if (_configService.UpdateConfigSectionString(name, section))
                MakoWebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.OK);
            else
                MakoWebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.InternalServerError);
        }

        [Route("config/section/{name}")]
        [Method("OPTIONS")]
        public void OptionsSection(string name, WebServerEventArgs e)
        {
            e.Context.Response.AddCors();
            MakoWebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.NoContent);
        }


        [Route("config/exit")]
        [Method("GET")]
        public void Exit(WebServerEventArgs e)
        {
            e.Context.Response.AddCors();
            MakoWebServer.OutputHttpCode(e.Context.Response, HttpStatusCode.OK);

            new Thread(() =>
            {
                Thread.Sleep(5000);
                _configManager.StopConfigMode(); 
            }).Start();
        }
    }
}
