using AutoMapper;
using HomeAPI.Contracts.Models.Rooms;
using HomeAPI.Data.Models;
using HomeAPI.Data.Queries;
using HomeAPI.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private IRoomRepository _roomRepository;
    private IMapper _mapper;

    public RoomsController(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
    {
        var existongRoom = await _roomRepository.GetRoomByName(request.Name);
        if (existongRoom == null)
        {
            var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
            await _roomRepository.AddRoom(newRoom);
            return StatusCode(201, $"Комната {request.Name} успешно создана");
        }
        return StatusCode(409, $"Комната {request.Name} уже существует");
    }
}