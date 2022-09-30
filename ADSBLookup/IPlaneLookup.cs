using Coordinator;

namespace ADSBLookup;

public interface IPlaneLookup
{
    List<IPlane> GetPlanesCenteredOn(Coordinate latitude, Coordinate longitude, float boxSideNM = 10);
}