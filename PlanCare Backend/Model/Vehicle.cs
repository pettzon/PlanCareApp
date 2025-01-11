using PlanCare_Backend.Data;

namespace PlanCare_Backend.Model;

//baseline vehicle class, meant to be abstract and extended based on vehicle models, but left in a basic state for now
public class Vehicle
{
    public string Make { get; set; }
    public string RegistrationNumber { get; set; }
    public StateCode RegistrationState { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public VehicleStatus VehicleStatus { get; set; }

    public Vehicle(string make, string registrationNumber, StateCode registrationState, DateTime registrationDate, DateTime expirationDate)
    {
        Make = make;
        RegistrationNumber = registrationNumber;
        RegistrationState = registrationState;
        RegistrationDate = registrationDate;
        ExpirationDate = expirationDate;

        EvaluateStatus(); // Do an initial status evaluation to set some intiial value on entry creation;
    }

    // This would be done on the SQL side of things normally
    public VehicleStatus EvaluateStatus()
    {
        VehicleStatus = DateTime.Compare(DateTime.Now, ExpirationDate) >= 0 ? VehicleStatus.EXPIRED : VehicleStatus.REGISTERED;
        return VehicleStatus;
    }
}