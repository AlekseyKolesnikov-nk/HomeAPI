using HomeAPI.Data.Models;
using HomeAPI.Data.Queries;

namespace HomeAPI.Data.Repos;

public interface IRoomRepository
{
    Task<Room> GetRoomByName(string roomName);
    Task<Room> GetRoomById(Guid id);
    Task AddRoom(Room room);
    Task<Room[]> GetAllRooms(); // добавлено в рамках задания 34.8.3
    Task UpdateRoom(Room room, UpdateRoomQuery query); // добавлено в рамках задания 34.8.3

}