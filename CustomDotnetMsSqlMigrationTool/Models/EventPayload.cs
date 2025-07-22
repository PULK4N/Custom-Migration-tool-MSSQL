using Newtonsoft.Json.Linq;

namespace Models
{
    public class EventPayload
    {
        public static string[] EVENTS_TO_IGNORE = { "NumberConfirmed" };

        public int OrderNumber { get; set; }
        public Guid AggregateId { get; set; }
        public string EventName { get; set; }
        public JObject Data { get; set; }
        public Guid EventExecutor { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string StateMachineId { get; set; }
    }
}
