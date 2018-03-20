using Lime.Protocol;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Take.Blip.Client;

namespace Lollapalooza.Services.Sender
{
    public class CustomSender : ISender
    {
        private readonly IConfiguration _configuration;
        private readonly IBlipClient _client;
        private bool _isStarted;

        public CustomSender(IConfiguration configuration)
        {
            _configuration = configuration;
            (string botIdentifier, string botAccessKey) = GetBotConfiguration();

            var builder = new BlipClientBuilder()
               .UsingAccessKey(botIdentifier, botAccessKey)
               .UsingRoutingRule(Lime.Messaging.Resources.RoutingRule.Instance); // Deve ser Instance, caso contrário poderá receber mensagens de clientes!!

            _client = builder.Build();
            _isStarted = false;
        }

        public async Task<Command> ProcessCommandAsync(Command requestCommand, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckStarted(cancellationToken);
            return await _client.ProcessCommandAsync(requestCommand, cancellationToken);
        }

        public async Task SendCommandAsync(Command command, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckStarted(cancellationToken);
            await _client.SendCommandAsync(command, cancellationToken);
        }

        public async Task SendMessageAsync(Message message, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckStarted(cancellationToken);
            await _client.SendMessageAsync(message, cancellationToken);
        }

        public async Task SendNotificationAsync(Notification notification, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckStarted(cancellationToken);
            await _client.SendNotificationAsync(notification, cancellationToken);
        }
        private async Task CheckStarted(CancellationToken cancellationToken)
        {
            if (!_isStarted)
            {
                await _client.StartAsync(cancellationToken);
                _isStarted = true;
            }
        }
        private (string botIdentifier, string botAccessKey) GetBotConfiguration()
        {
            var botIdentifier = _configuration.GetSection("BotIdentifier").Value;
            var botAccessKey = _configuration.GetSection("BotAccessKey").Value;

            return (botIdentifier,
                botAccessKey);
        }
    }
}
