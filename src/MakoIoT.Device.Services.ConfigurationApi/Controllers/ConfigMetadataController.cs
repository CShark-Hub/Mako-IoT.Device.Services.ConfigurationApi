using System.Net;
using MakoIoT.Device.Services.Configuration.Metadata.Services;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.Server.Extensions;
using MakoIoT.Device.Services.Server.WebServer;
using MakoIoT.Device.Utilities.String.Extensions;

namespace MakoIoT.Device.Services.ConfigurationApi.Controllers
{
    public class ConfigMetadataController
    {
        private readonly IConfigurationMetadataService _metadataService;
        private readonly ILog _logger;

        public ConfigMetadataController(IConfigurationMetadataService metadataService, ILog logger)
        {
            _metadataService = metadataService;
            _logger = logger;
        }

        [Route("config/metadata/{sectionName}")]
        [Method("GET")]
        public void GetSectionMetadata(string sectionName, WebServerEventArgs e)
        {
            e.Context.Response.AddCors();
            
            var metadata = _metadataService.GetSectionMetadata(sectionName);
            if (metadata == null)
            {
                e.Context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            _logger.Trace($"{metadata.EscapeForInterpolation()}");

            e.Context.Response.ContentType = "application/json";
            e.Context.Response.StatusCode = (int)HttpStatusCode.OK;
            MakoWebServer.OutPutStream(e.Context.Response, metadata);
        }
    }
}
