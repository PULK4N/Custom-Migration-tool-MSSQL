using System.Data;
using Configuration;
using Dapper;
using Microsoft.Data.SqlClient;
using Models;

namespace Database
{
    public class DapperWriteEventStore
    {
        private const string READ_QUERY =
            @"
            SELECT TOP (@BatchSize)
            *
            FROM EventPayloads
            WHERE Timestamp >= @SinceTime
            ORDER BY Timestamp ASC;
        ";

        private const string READ_BY_ID_QUERY =
            @"
            SELECT
            *
            FROM EventPayloads
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY;
        ";

        /*
         * Used when migrating the whole database for faster performance
         */
        public async Task<List<EventPayload>> ReadFrom(DateTime initialSinceTime, int batchSize)
        {
            var sinceTime = initialSinceTime;
            using var connection = new SqlConnection(
                ConfigurationDetails.ReadDatabaseConnectionString
            );

            var batch = await connection.QueryAsync<SerializedEventPayload>(
                READ_QUERY,
                new { SinceTime = sinceTime, BatchSize = batchSize }
            );

            return batch.Select(SerializedEventPayload.DeserializeEventPayload).ToList();
        }

        /*
         * Used when migrating the whole database for faster performance
         */
        public async Task<List<SerializedEventPayload>> ReadByIdAsync(int skip, int take)
        {
            using var connection = new SqlConnection(
                ConfigurationDetails.ReadDatabaseConnectionString
            );
            var batch = await connection.QueryAsync<SerializedEventPayload>(
                READ_BY_ID_QUERY,
                new { Skip = skip, Take = take }
            );

            return batch.ToList();
        }
    }
}
