export enum StateCode
{
    UNDEFINED = 0,
    NSW = 1,
    VIC = 2,
    QLD = 3,
    WA = 4,
    SA = 5,
    TA = 6
}

export enum VehicleStatus
{
    UNKNOWN = 0,
    REGISTERED = 1,
    EXPIRED = 2
}

export interface Vehicle
{
    make: string,
    registrationNumber: string,
    registrationState: string,
    registrationDate: string,
    expiryDate: string,
    vehicleStatus: string
}