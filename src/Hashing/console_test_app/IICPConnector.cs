using console_test_app.Clients.HashingClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_test_app
{
    internal interface IICPConnector
    {
        public Task<bool> StoreRootHash(string rootHash, Transaction transaction);
        public Task<List<Transaction>> GetRootHash(string rootHash);
    }
}
