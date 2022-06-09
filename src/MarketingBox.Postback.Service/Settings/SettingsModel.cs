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

        [YamlProperty("PostbackService.MarketingBoxServiceBusHostPort")]
        public string MarketingBoxServiceBusHostPort { get; set; }

        [YamlProperty("PostbackService.MyNoSqlReaderHostPort")]
        public string MyNoSqlReaderHostPort { get; set; }
        
        [YamlProperty("PostbackService.AffiliateServiceUrl")]
        public string AffiliateServiceUrl { get; set; }
        
        [YamlProperty("PostbackService.ReportingServiceUrl")]
        public string ReportingServiceUrl { get; set; }
        
        [YamlProperty("PostbackService.RegistrationServiceUrl")]
        public string RegistrationServiceUrl { get; set; }
    }
}
