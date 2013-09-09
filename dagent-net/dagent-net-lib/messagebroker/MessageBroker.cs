﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
namespace dagent_net_lib.messagebroker
{
    public class MessageBroker
    {
        public MessageBroker() : this("127.0.0.1","guest","guest",5672,System.Guid.NewGuid().ToString())
        {
        }
        public MessageBroker(String hostname, String username, String password, int port, string UUID)
        {
            this.Hostname = hostname;
            this.Username = username;
            this.Password = password;
            this.Port = port;
            this.UUID = UUID;
            this.Init();
        }
        public String Hostname;
        public String Password;
        public String Username;
        public String UUID;
        public int Port;
        public MessageBrokerChannel NewChannel()
        {
            MessageBrokerChannel channel = new MessageBrokerChannel(this._connection.CreateModel());
            return channel;
        }

        private IConnection _connection;
        public Boolean Init()
        {

            this.Hostname = Util.checkregstring("HKLM", @"SOFTWARE\dagent\messagebroker", "hostname", this.Hostname);
            this.Password = Util.checkregstring("HKLM", @"SOFTWARE\dagent\messagebroker", "password", this.Password);
            this.Username = Util.checkregstring("HKLM", @"SOFTWARE\dagent\messagebroker", "username", this.Username);
            this.UUID = Util.checkregstring("HKLM", @"SOFTWARE\dagent\messagebroker", "uuid", this.UUID);
            this.Port = Util.checkregint("HKLM", @"SOFTWARE\dagent\messagebroker", "port", this.Port);
            var factory = new ConnectionFactory()
            {
                HostName = this.Hostname,
                Password = this.Password,
                UserName = this.Username,
                Port = this.Port
            };
            this._connection = factory.CreateConnection();

            return true;
        }
    }
}
