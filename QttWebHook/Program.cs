﻿using System;
using MQTTnet.Server;
using MQTTnet;
using MQTTnet.Protocol;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;



namespace QttWebHook
{
    class Program
    {

        static Config.Model Conf = new Config.Provider().FromENV();
        static void Main(string[] args)
        {
            Run();
        }


        public static async void Run() {
                          // Configure MQTT server.
            // Configure MQTT server.
            var optionsBuilder = new MqttServerOptionsBuilder()
                .WithConnectionBacklog(100)
                .WithDefaultEndpointPort(Conf.Port);
            var options = new MqttServerOptions();
            options.ConnectionValidator = c => 
            {
                // if (c.ClientId.Length  10)
                // {
                //     c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                //     return;
                // }

                // TODO: Adds WebHook to auth
                if (c.Username != Conf.DefaultUser)
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    return;
                }

                if (c.Password != Conf.DefaultPass)
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    return;
                }
                c.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
            };


            var mqttServer = new MqttFactory().CreateMqttServer();
                mqttServer.ApplicationMessageReceived += (s, e) => {
                    // TODO: Adds Webhook to sending message ..
                    Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                    Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                    Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                    Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                    Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                    Console.WriteLine();
            };

            options.SubscriptionInterceptor = ctx => {
                Console.WriteLine("ClientID={0}", ctx.ClientId);
            };  
            await  mqttServer.StartAsync(options);
            
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
            await mqttServer.StopAsync();
        }       

    }    
}
