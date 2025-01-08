using PlanCare_Backend.Data;

namespace PlanCare_Backend.Model;

public class Vehicle
{
    public string RegistrationNumber { get; set; }
    public CountryState RegistrationState { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    public Vehicle(string registrationNumber, CountryState registrationState, DateTime registrationDate, DateTime expirationDate)
    {
        RegistrationNumber = registrationNumber;
        RegistrationState = registrationState;
        RegistrationDate = registrationDate;
        ExpirationDate = expirationDate;
    }
}