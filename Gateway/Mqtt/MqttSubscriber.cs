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
using Gateway.BusinessLogic;

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
                    if (topic == "RSCD/Message/Registration/userCreated")
                    {
                        using (IServiceScope scope = serviceProvider.CreateScope())
                        {
                            // pass it to the handler class
                            var bl = scope.ServiceProvider.GetRequiredService<Login_BL>();
                            var userData = JsonConvert.DeserializeObject<RSCD.Model.Message.UserDetailMessage>(data);
                            var result = bl.CreateAsync(userData);
                        }
                    }
                    if (topic == "RSCD/Message/Registration/userUpdated")
                    {
                        using (IServiceScope scope = serviceProvider.CreateScope())
                        {
                            // pass it to the handler class
                            var bl = scope.ServiceProvider.GetRequiredService<Login_BL>();
                            //var disasterdData = JsonConvert.DeserializeObject<NewUser>(data);
                            var result = bl.UpdateDocumentAsync("");
                        } 
                    }
                    if (topic == "RSCD/Message/Registration/userDeleted")
                    {
                        using (IServiceScope scope = serviceProvider.CreateScope())
                        {
                            // pass it to the handler class
                            var bl = scope.ServiceProvider.GetRequiredService<Login_BL>();
                            //var disasterdData = JsonConvert.DeserializeObject<NewUser>(data);
                            var result = bl.DeleteDocumentAsync("");
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
