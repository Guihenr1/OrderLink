using EasyNetQ;

namespace OrderLink.Sync.Core.Models;

public class AttributeBasedConventions : Conventions
{
    private readonly ITypeNameSerializer _typeNameSerializer;

    public AttributeBasedConventions(ITypeNameSerializer typeNameSerializer)
        : base(typeNameSerializer)
    {
        if (typeNameSerializer == null)
            throw new ArgumentNullException("typeNameSerializer");

        _typeNameSerializer = typeNameSerializer;

        ExchangeNamingConvention = GenerateExchangeName;
        QueueNamingConvention = GenerateQueueName;
    }

    private string GenerateExchangeName(Type messageType)
    {
        var exchangeNameAtt = messageType.GetCustomAttributes(typeof(ExchangeNameAttribute), true).SingleOrDefault() as ExchangeNameAttribute;

        return exchangeNameAtt == null
            ? _typeNameSerializer.Serialize(messageType)
            : exchangeNameAtt.ExchangeName;
    }

    private string GenerateQueueName(Type messageType, string subscriptionId)
    {
        var queueNameAtt = messageType.GetCustomAttributes(typeof(QueueNameAttribute), true).SingleOrDefault() as QueueNameAttribute;

        if (queueNameAtt == null)
        {
            var typeName = _typeNameSerializer.Serialize(messageType);
            return string.Format("{0}_{1}", typeName, subscriptionId);
        }

        return string.IsNullOrEmpty(subscriptionId)
            ? queueNameAtt.QueueName
            : string.Concat(queueNameAtt.QueueName);
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExchangeNameAttribute : Attribute
    {
        public ExchangeNameAttribute(string exchangeName)
        {
            ExchangeName = exchangeName;
        }

        public string ExchangeName { get; set; }
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class QueueNameAttribute : Attribute
    {
        public QueueNameAttribute(string queueName)
        {
            QueueName = queueName;
        }

        public string QueueName { get; set; }
    }
}
