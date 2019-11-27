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

namespace RSCD.Mqtt
{
    public class MqttPublisher
    {
        protected IMqttClient Client { get; set; }
        private IMqttClientOptions ClientOptions { get; set; }
        private MqttApplicationMessage PublishOptions { get; set; }


        public MqttPublisher(IOptions<Mqtt_Settings> options)
        {
            RegisterMqtt(options.Value);
        }

        private void RegisterMqtt(Mqtt_Settings settings)
        {
            var factory = new MqttFactory();
            Client = factory.CreateMqttClient();
            ClientOptions = new MqttClientOptionsBuilder()
                .WithClientId((settings.ClientId + "S"))
                .WithTcpServer(settings.Host)
                .Build();

            Client.ConnectedHandler = new MqttClientConnectedHandlerDelegate(async e => {

                await Client.PublishAsync(PublishOptions);
                DisconnectMqtt();
            });
        }

        public void ConnectMqqt()
        {
            Task.Run(async () => { await Client.ConnectAsync(ClientOptions); });
        }

        public void DisconnectMqtt()
        {
            Task.Run(async () => { await Client.DisconnectAsync(); });
        }

        public void MqttPublish(string topic,string data)
        {
            PublishOptions = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(data)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            ConnectMqqt();
        }


    }


}
