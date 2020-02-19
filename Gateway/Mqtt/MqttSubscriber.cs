using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client.Receiving;
using RSCD.Model.Configration;
using RSCD.MQTT;
using Newtonsoft.Json;

namespace Gateway.Mqtt
{
    public class MqttSubscriber : MqttClient
    {
        [Obsolete]
        public MqttSubscriber(IApplicationLifetime appLifetime, IServiceProvider serviceProvider, IOptions<Mqtt_Settings> options) : base(appLifetime, serviceProvider, options.Value)
        {
            Client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(e => {

                var data = e.ApplicationMessage.ConvertPayloadToString();
                var topic = e.ApplicationMessage.Topic;

                Console.WriteLine($"Topic is {e.ApplicationMessage.Topic} \n{data}");

                try
                {
                    if (topic == "RSCD/Registration/NewCommonUser")
                    {
                        using (IServiceScope scope = serviceProvider.CreateScope())
                        {
                            // pass it to the handler class
                        }
                    }
                }
                catch
                {

                }

            });

            //ConnectMqqt();
        }
    }
}
