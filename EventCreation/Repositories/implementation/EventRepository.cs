using Dapper;
using EventCreation.Models.Entities;
using EventCreation.Repositories.interfaces;

namespace EventCreation.Repositories.implementation;
public class EventRepository : IEventRepository
{
    private readonly ConnectionFactory.ConnectionFactory _connection;

    public EventRepository(ConnectionFactory.ConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Event> GetEventById(int id)
    {
        using var connection = _connection.CreateConnection();
        connection.Open();

        var query = @"SELECT * FROM Events WHERE Id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<Event>(query, new { Id = id });

        return result!;
    }

    public async Task<IEnumerable<Event>> GetAllEvents()
    {
        using var connection = _connection.CreateConnection();
        connection.Open();

        var query = @"SELECT * FROM Events";
        var results = await connection.QueryAsync<Event>(query);

        return results;
    }

    public async Task<Event> CreateEvent(Event evt)
    {
        using var connection = _connection.CreateConnection();
        connection.Open();

        var query = @"
            INSERT INTO Events 
            (name, description, start_time, end_time, status, created_at, updated_at, created_By)
            VALUES 
            (@Name, @Description, @StartTime, @EndTime, @Status, @CreatedAt, @UpdatedAt, @CreatedBy)
            RETURNING *";

        var result = await connection.QuerySingleOrDefaultAsync<Event>(query, evt);

        return result!;
    }

    public async Task<Event> UpdateEvent(Event evt, int id)
    {
        using var connection = _connection.CreateConnection();
        connection.Open();

        var query = @"
            UPDATE Events
            SET 
                name = @Name,
                Description = @Description,
                start_time = @StartTime,
                end_time = @EndTime,
                status = @Status,
                updated_at = @UpdatedAt
            WHERE Id = @Id
            RETURNING *";

        var result = await connection.QuerySingleOrDefaultAsync<Event>(query, new
        {
            Id = id,
            evt.Name,
            evt.Description,
            evt.StartTime,
            evt.EndTime,
            evt.Status,
            evt.UpdatedAt
        });

        return result!;
    }

    public async Task<Event> DeleteEvent(int id)
    {
        using var connection = _connection.CreateConnection();
        connection.Open();

        var query = @"
            DELETE FROM Events
            WHERE Id = @Id
            RETURNING *";

        var result = await connection.QuerySingleOrDefaultAsync<Event>(query, new { Id = id });

        return result!;
    }
}
