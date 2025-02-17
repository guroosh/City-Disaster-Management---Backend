﻿
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Client.Connecting;
using RescueTeam.BusinessLogic;
using RescueTeam.Model.API;
using RSCD.Model.Configration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTTnet.Client.Receiving;
using RSCD.MQTT;
using Microsoft.Extensions.Hosting;
using RSCD.Model.Message;

namespace RescueTeam.Mqtt
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
                    if (topic == "RSCD/Message/Disaster/Verified")
                    {
                        using (IServiceScope scope = serviceProvider.CreateScope())
                        {
                            // pass it to the handler class
                            var bl = scope.ServiceProvider.GetRequiredService<RescueTeam_BL>();
                            var rescueTeamData = Newtonsoft.Json.JsonConvert.DeserializeObject<VerifiedDisasterMessage>(data);
                            bl.ResourceAllocationAsync(rescueTeamData);
                        }
                    }
                    else if (topic == "RSCD/Message/Registration/userCreated")
                    {
                        using (IServiceScope scope = serviceProvider.CreateScope())
                        {
                            // pass it to the handler class
                            var bl = scope.ServiceProvider.GetRequiredService<RescueTeam_BL>();
                            var rescueTeamData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetailMessage>(data);
                            var result = bl.CreateAsync(rescueTeamData);
                        }
                    }
                    //RSCD / Message / Registration / userCreated
                }
                catch
                {

                }

            });

            //ConnectMqqt();
        }
    }
}

