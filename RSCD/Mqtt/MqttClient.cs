using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using Microsoft.Extensions.Hosting;
using System.Threading;
using MQTTnet.Client.Options;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Receiving;
using Microsoft.Extensions.Options;
using RSCD.Model.Configration;

namespace RSCD.MQTT
{
    public class MqttClient : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        [Obsolete]
        private readonly IApplicationLifetime _appLifetime;

        protected IMqttClient Client { get; set; }
        private IMqttClientOptions ClientOptions { get; set; }

        [Obsolete]
        public MqttClient(IApplicationLifetime appLifetime, IServiceProvider serviceProvider, Mqtt_Settings settings)
        {
            _appLifetime = appLifetime;
            _serviceProvider = serviceProvider;
            RegisterMqtt(settings);
        }

        private void RegisterMqtt(Mqtt_Settings settings)
        {
            var factory = new MqttFactory();
            Client = factory.CreateMqttClient();
            ClientOptions = new MqttClientOptionsBuilder()
                .WithClientId(settings.ClientId)
                .WithTcpServer(settings.Host)
                .Build();

            var PublishOptions = new MqttApplicationMessageBuilder()
                .WithTopic("MyTopic")
                .WithPayload("Hello World")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            Client.ConnectedHandler = new MqttClientConnectedHandlerDelegate(e => {
                Client.SubscribeAsync(new TopicFilterBuilder().WithTopic(settings.SuscribeTopic).Build());
            });
        }

        //func to connect with the broker
        public void ConnectMqqt()
        {
            Task.Run(async () => { await Client.ConnectAsync(ClientOptions); });
        }

        //hosted service cancellation token
        [Obsolete]
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }
        

        private void OnStopped()
        {   
            Client.DisconnectAsync();
        }

        private void OnStopping()
        {
            
        }

        private void OnStarted()
        {
            Client.ConnectAsync(ClientOptions);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
