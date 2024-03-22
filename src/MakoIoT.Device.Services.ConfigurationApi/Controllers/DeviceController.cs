using System.Net;
using MakoIoT.Device.Services.Configuration.Metadata.Services;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Services.Server.Extensions;
using MakoIoT.Device.Services.Server.WebServer;
using MakoIoT.Device.Utilities.String.Extensions;

namespace MakoIoT.Device.Services.ConfigurationApi.Controllers
{
    public class DeviceController
    {
        private readonly ILog _logger;
        private readonly IConfigurationMetadataService _metadataService;

        public DeviceController(ILog logger, IConfigurationMetadataService metadataService)
        {
            _logger = logger;
            _metadataService = metadataService;
        }

        [Route("device/metadata")]
        [Method("GET")]
        public void GetDeviceMetadata(WebServerEventArgs e)
        {
            e.Context.Response.AddCors();

            var metadata = _metadataService.GetDeviceMetadata();
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
