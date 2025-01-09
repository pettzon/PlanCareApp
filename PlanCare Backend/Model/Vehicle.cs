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

    public Vehicle(string make, string registrationNumber, StateCode registrationState, DateTime registrationDate, DateTime expirationDate)
    {
        Make = make;
        RegistrationNumber = registrationNumber;
        RegistrationState = registrationState;
        RegistrationDate = registrationDate;
        ExpirationDate = expirationDate;
    }
}