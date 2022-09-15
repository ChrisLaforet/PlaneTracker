namespace ADSBLookup.OpenNetwork.Model;

public class OpenNetworkAllResponse
{
    public int Time { get; set; }
    public IEnumerable<OpenNetworkState> States { get; set; }
}