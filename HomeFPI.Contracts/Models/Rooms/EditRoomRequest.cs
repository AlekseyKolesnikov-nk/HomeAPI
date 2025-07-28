namespace HomeAPI.Contracts.Models.Rooms;

public class EditRoomRequest // добавлено в рамках задания 34.8.3
{
    public string NewName { get; set; }
    public int NewVoltage { get; set; }
    public bool NewGasConnected { get; set; }
}