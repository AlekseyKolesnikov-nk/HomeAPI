namespace HomeAPI.Data.Queries;

public class UpdateRoomQuery // доабвлено в рамках задания 34.8.3
{
    public string NewName { get; set; }
    public int NewVoltage { get; set; }
    public bool NewGasConnected { get; set; }

    public UpdateRoomQuery(string newName = null, int newVoltage = 0, bool newGasConnected = false)
    {
        NewName = newName;
        NewVoltage = newVoltage;
        NewGasConnected = newGasConnected;
    }
}