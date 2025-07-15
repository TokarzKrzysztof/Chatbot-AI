using Azure;
using Backend.Database;
using Backend.Infrastructure.Helpers;
using Backend.Models.Entities;
using Backend.Utilities;
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
        private Message? _pendingMessage = null;

        private readonly IServiceProvider _serviceProvider;
        public BackgroundGeneratorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public bool HasPendingMessage()
        {
            return _pendingMessage != null;
        }

        public void Cancel()
        {
            _cts.Cancel();
        }

        public async IAsyncEnumerable<string?> ListenResponseGeneration(CancellationToken cancellationToken)
        {
            while (_pendingMessage != null)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _cts.Cancel();
                }

                yield return _pendingMessage.Text;
                await Task.Delay(50);
            }
        }

        private readonly Locker _locker = new();
        public async Task GenerateAndSaveResponse()
        {
            // application is assuming that only one user is using it, in real life we need some kind of sessionIds etc, and dictionary of generated messages
            if (_pendingMessage != null)
            {
                // cancel previous generation when new message is sent
                _cts.Cancel();
            }

            await _locker.Run(async () =>
            {
                try
                {
                    await GenerateAndSaveResponseThreadSafe();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception during GenerateAndSaveResponseThreadSafe: " + ex.Message);
                }

                _pendingMessage = null;
            });
        }

        private async Task GenerateAndSaveResponseThreadSafe()
        {
            _cts = new();
            _pendingMessage = new Message()
            {
                IsAnswer = true
            };

            await foreach (var current in AIGenerator.Generate(_cts.Token))
            {
                _pendingMessage.Text = current;
            }

            if (!string.IsNullOrWhiteSpace(_pendingMessage.Text))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Messages.Add(_pendingMessage);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
