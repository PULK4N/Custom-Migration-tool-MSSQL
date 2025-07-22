using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Models;

namespace Database
{
    public class DapperReadEventStore
    {
        private readonly IDbConnection _connection;
        private readonly string _connString;
        private const string READ_QUERY =
            @"
            SELECT TOP (@BatchSize)
                Id,
                Timestamp,
                Payload
            FROM EventPayloads
            WHERE Timestamp >= @SinceTime
            ORDER BY Timestamp ASC;
        ";

        public async Task<List<SerializedEventPayload>> ReadFrom(
            DateTime initialSinceTime,
            int batchSize
        )
        {
            var sinceTime = initialSinceTime;
            using var connection = new SqlConnection(_connString);

            var batch = await connection.QueryAsync<SerializedEventPayload>(
                READ_QUERY,
                new { SinceTime = sinceTime, BatchSize = batchSize }
            );

            return batch.ToList();
        }
    }
}
