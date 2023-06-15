using CommunicationHubBackend.Core.Commands;
using CommunicationHubBackend.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommunicationHubBackend.Core.Handlers
{
    public class AnalyseMessageHandler : IRequestHandler<AnalyseMessageCommand, string>
    {
        public async Task<string> Handle(AnalyseMessageCommand request, CancellationToken cancellationToken)
        {
            string[] allWordsInmessage = request.Message.Split(' ');
            foreach (var item in SensitiveWords.SensitiveWordsList)
            {
                for (int i = 0; i < allWordsInmessage.Length; i++)
                {
                    string cleanedWord = Regex.Replace(allWordsInmessage[i], @"[\p{P}]", "");
                    if (cleanedWord.Equals(item))
                    {
                        string punctuationMarks = Regex.Replace(allWordsInmessage[i], @"[\p{L}]+", "");
                        allWordsInmessage[i] = "****";
                        allWordsInmessage[i] += punctuationMarks;
                    }
                }
            }
            return string.Join(" ", allWordsInmessage);
        }
    }
}
