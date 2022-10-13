using MakoIoT.Device.Services.ConfigurationApi.Controllers;
using MakoIoT.Device.Services.Server;

namespace MakoIoT.Device.Services.ConfigurationApi.Extensions
{
    public static class WebServerOptionsExtension
    {
        /// <summary>
        /// Registers ConfigurationApi controllers.
        /// </summary>
        /// <param name="options"></param>
        public static void AddConfigurationApi(this WebServerOptions options)
        {
            options.AddController(typeof(ConfigController));
            options.AddController(typeof(ConfigMetadataController));
            options.AddController(typeof(DeviceController));
        }
    }
}
