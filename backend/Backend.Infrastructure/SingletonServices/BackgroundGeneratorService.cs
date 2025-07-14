using Backend.Database;
using Backend.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Infrastructure.SingletonServices
{
    public class BackgroundGeneratorService
    {
        private CancellationTokenSource _cts = new();
        private StringBuilder _sb = new();
        private bool _inProgress = false;

        private readonly IServiceProvider _serviceProvider;
        public BackgroundGeneratorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async IAsyncEnumerable<string> ListenResponseGeneration(CancellationToken cancellationToken)
        {
            while (_inProgress)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _cts.Cancel();
                }

                yield return _sb.ToString();
                await Task.Delay(200);
            }

            // return once again to make sure that full message is generated
            yield return _sb.ToString();
        }

        public async Task GenerateAndSaveResponse()
        {
            // application is assuming that only one user is using it, in real life we need some kind of sessionIds etc, and dictionary of generated messages
            if (_sb.Length > 0)
            {
                // cancel previous generation when new message is sent
                _cts.Cancel();
            }
            // create new instance for new generation
            _cts = new CancellationTokenSource();
            _sb.Clear();
            _inProgress = true;

            string chatResponse = "123456789abcdefghijk3456789abclmnopr3456789abcstuowc3456789abcdefgh3456789abci3456789abcjklmnoprstuow3456789abccdefghijklmnoprstuow";

            int i = 0;
            while (!_cts.Token.IsCancellationRequested)
            {
                if (i == chatResponse.Length) break;

                _sb.Append(chatResponse[i]);
                await Task.Delay(50);

                i++;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Messages.Add(new Message()
                {
                    Text = _sb.ToString(),
                    IsAnswer = true
                });
                await dbContext.SaveChangesAsync();
            }

            _inProgress = false;
        }
    }
}
