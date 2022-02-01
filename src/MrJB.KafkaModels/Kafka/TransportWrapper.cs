using MrJB.KafkaModels.Types;

namespace MrJB.KafkaModels.Kafka
{
    public class TransportWrapper
    {
        public SystemType? SourceSystem { get; set; }

        public object Data { get; set; }
    }
}
