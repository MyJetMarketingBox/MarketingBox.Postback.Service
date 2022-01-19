using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace MarketingBox.Postback.Service.Settings
{
    public class SettingsModel
    {
        [YamlProperty("PostbackService.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("PostbackService.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("PostbackService.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        [YamlProperty("PostbackService.PostgresConnectionString")]
        public string PostgresConnectionString { get; set; }
    }
}
