using AutoMapper;
using HomeAPI.Contracts.Models.Devices;
using HomeAPI.Data.Models;
using HomeAPI.Data.Queries;
using HomeAPI.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DevicesController : ControllerBase
{
    private IDeviceRepository _deviceRepository;
    private IRoomRepository _roomRepository;
    private IMapper _mapper;

    public DevicesController(IMapper mapper, IRoomRepository rooms, IDeviceRepository devices)
    {
        _deviceRepository = devices;
        _mapper = mapper;
        _roomRepository = rooms;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetDevices()
    {
        var devices = await _deviceRepository.GetDevices();

        var resp = new GetDevicesResponse
        {
            DeviceAmount = devices.Length,
            Devices = _mapper.Map<Device[], DeviceView[]>(devices)
        };
        return StatusCode(200, resp);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Add(AddDeviceRequest request)
    {
        var room = await _roomRepository.GetRoomByName(request.Location);
        if (room == null)
            return StatusCode(400, $"Комната {request.Location} не подключена");

        var device = await _deviceRepository.GetDeviceByName(request.Name);
        if (device != null)
            return StatusCode(400, $"Устройство {request.Name} уже существует");

        var newDevice = _mapper.Map<AddDeviceRequest, Device>(request);
        await _deviceRepository.SaveDevice(newDevice, room);

        return StatusCode(200, $"Устройство {request.Name} успешно создано. ID - {newDevice.Id}");
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] EditDeviceRrequest request)
    {
        var room = await _roomRepository.GetRoomByName(request.NewRoom);
        if (room == null)
            return StatusCode(400, $"Комната {request.NewRoom} не подключена");

        var device = await _deviceRepository.GetDeviceById(id);
        if (device == null)
            return StatusCode(400, $"Устройство {id} не существует");

        var sameName = await _deviceRepository.GetDeviceByName(request.NewName);
        if (sameName != null)
            return StatusCode(400, $"Имя устройства {request.NewName} уже существует");

        await _deviceRepository.UpdateDevice(device, room, new UpdateDeviceQuery(request.NewName, request.NewSerial));

        return StatusCode(200, $"Устройство {device.Name} с серийным номером {device.SerialNumber} в {device.Room.Name} обновлено");
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deletableDevice = await _deviceRepository.GetDeviceById(id);

        await _deviceRepository.DeleteDevice(deletableDevice);
        
        return StatusCode(200, $"Устройство {deletableDevice.Name} {deletableDevice.Id} удалено из {deletableDevice.Location}");
    }
}