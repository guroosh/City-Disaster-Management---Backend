using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Receiving;
using RSCD.Model.Configration;
using RSCD.MQTT;

namespace Disaster.Mqtt
{
    public class MqttSubscriber : MqttClient
    {
        [Obsolete]
        public MqttSubscriber(IApplicationLifetime appLifetime, IServiceProvider serviceProvider, IOptions<Mqtt_Settings> options) : base(appLifetime, serviceProvider, options.Value)
        {
            Client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(e => {

                var data = e.ApplicationMessage.ConvertPayloadToString();
                Console.WriteLine($"Topic is {e.ApplicationMessage.Topic} \n{data}");
            });

            //ConnectMqqt();
        }
    }
}
