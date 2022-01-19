using MyJetWallet.Sdk.Postgres;

namespace MarketingBox.Postback.Service.Postgres.DesignTime
{
    public class ContextFactory : MyDesignTimeContextFactory<DatabaseContext>
    {
        public ContextFactory() : base(options => new DatabaseContext(options))
        {

        }
    }
}