using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommunicationHubBackend.Public.NotGenerated
{
    internal abstract class CommunicationHubBackendCommandBase
    {
        protected async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();

            return msg;
        }
    }
}
