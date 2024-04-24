using console_test_app.Clients.HashingClient;
using console_test_app.Clients.HashingClient.Models;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
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

        public async Task<List<Transaction>> GetRootHash(string rootHash)
        {
            var client = CreateClient();
            var result = await client.GetRootHash(rootHash);
            return result.HasValue ? result.ValueOrDefault.ToList() : Enumerable.Empty<Transaction>().ToList();
        }

        public async Task<bool> StoreRootHash(string rootHash, Transaction transaction)
        {
            var client = CreateClient();
            await client.StoreRootHash(rootHash, transaction);
            return true;
        }

        private HashingClientApiClient CreateClient()
        {
            IIdentity identity = null;
            var agent = new HttpAgent(identity, new Uri(_settings.BaseUrl));
            var canisterId = Principal.FromText(_settings.CannisterId);

            var client = new HashingClientApiClient(agent, canisterId);

            return client;
        }
    }
}
