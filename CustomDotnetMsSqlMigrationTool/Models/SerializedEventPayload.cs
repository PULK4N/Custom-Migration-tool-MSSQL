using Newtonsoft.Json.Linq;

namespace Models
{
    public class SerializedEventPayload
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public Guid AggregateId { get; set; }
        public string EventName { get; set; }
        public string DataJson { get; set; }
        public Guid EventExecutor { get; set; }
        public DateTime Timestamp { get; set; }
        public string StateMachineId { get; set; }

        public static EventPayload DeserializeEventPayload(
            SerializedEventPayload serializedEventPayload
        )
        {
            var payload = new EventPayload();
            payload.OrderNumber = serializedEventPayload.OrderNumber;
            payload.AggregateId = serializedEventPayload.AggregateId;
            payload.EventName = serializedEventPayload.EventName;
            payload.Data = JObject.Parse(serializedEventPayload.DataJson);
            payload.EventExecutor = serializedEventPayload.EventExecutor;
            payload.Timestamp = DateTime.SpecifyKind(
                serializedEventPayload.Timestamp,
                DateTimeKind.Utc
            );
            payload.StateMachineId = serializedEventPayload.StateMachineId;

            return payload;
        }
    }
}
