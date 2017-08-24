﻿using Autofac;
using Jambo.Domain.ServiceBus;
using Jambo.Infrastructure;
using Jambo.KafkaBus;

namespace Jambo.IoC
{
    public class ServiceBusModule : Module
    {
        private readonly string _connectionString;
        private readonly string _topic;
        private readonly ProcessDomainEventDelegate _proccessDomainEvent;

        public ServiceBusModule(string connectionString, string topic, ProcessDomainEventDelegate proccessDomainEvent)
        {
            _connectionString = connectionString;
            _topic = topic;
            _proccessDomainEvent = proccessDomainEvent;
        }

        protected override void Load(ContainerBuilder builder)
        {
            IServiceBus serviceBus = new ServiceBus(_connectionString, _topic);
            serviceBus.OnReceive = _proccessDomainEvent;
            serviceBus.Listen();

            builder.Register(c => serviceBus).As<IServiceBus>().SingleInstance();
        }
    }
}