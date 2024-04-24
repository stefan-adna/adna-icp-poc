using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Standards.AssetCanister;
using EdjCase.ICP.Candid.Models;

namespace console_test_app
{
    internal class ICPConnector : IICPConnector
    {
        private readonly Settings _settings;
        public ICPConnector(Settings settings)
        {
            _settings = settings;
        }

        public async Task<byte[]> DownloadDocument(string identifier)
        {
            var client = CreateClient();
             var result = await client.DownloadAssetAsync(identifier);
            return result.Asset;
        }

        public async Task<bool> UploadDocument(string identifier, string filePath)
        {
            var client = CreateClient();
            using var contentStream = File.OpenRead(filePath);
            var bytes = File.ReadAllBytes(filePath);

            await client.UploadAssetChunkedAsync(
                key: identifier,
                contentType: "text/plain",
                contentEncoding: "br",
                contentStream: contentStream,
                sha256: null
            );

            return true;
        }

        private AssetCanisterApiClient CreateClient()
        {
            IIdentity identity = IdentityUtil.FromPemFile(@"c:\temp\dfinity\adna_admin.pem", "poc"); ;
            var agent = new HttpAgent(identity, new Uri(_settings.BaseUrl));
            var canisterId = Principal.FromText(_settings.CannisterId);

            var client = new AssetCanisterApiClient(agent, canisterId);

            return client;
        }
    }
}
