using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationHubBackend.Core.Commands
{
    public class AnalyseMessageCommand : IRequest<string>
    {
        public string Message { get; set; }
    }
}
