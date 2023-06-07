using Confluent.Kafka;

namespace MeetUp.Kafka.Configurations
{
    public class ProducerKafkaConfiguration<TKey, TValue> : ProducerConfig
    {
        public string? Topic { get; set; }
    }
}
