using HomeAPI.Data.Models;
using HomeAPI.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace HomeAPI.Data.Repos;

public class RoomRepository : IRoomRepository
{
    private readonly HomeAPIContext _context;

    public RoomRepository(HomeAPIContext context)
    {
        _context = context;
    }

    public async Task<Room> GetRoomByName(string name)
    {
        return await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
    }

    public async Task<Room> GetRoomById(Guid id)
    {
        return await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddRoom(Room room)
    {
        var entry = _context.Entry(room);
        if (entry.State == EntityState.Detached)
            await _context.Rooms.AddAsync(room);

        await _context.SaveChangesAsync();
    }

    public async Task<Room[]> GetAllRooms() // добавлен метод для получения всех комнат в рамках задания 34.8.3
    {
        return await _context.Rooms.ToArrayAsync();
    }

    public async Task UpdateRoom(Room room, UpdateRoomQuery query) // добавлено в рамках задания 34.8.3
    {
        if (!string.IsNullOrEmpty(query.NewName))
            room.Name = query.NewName;
        if (query.NewVoltage != 0)
            room.Voltage = query.NewVoltage;
        room.GasConnected = query.NewGasConnected;

        var entry = _context.Entry(room);
        if (entry.State == EntityState.Detached)
            _context.Rooms.Update(room);

        await _context.SaveChangesAsync();
    }
}