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
    }
}
