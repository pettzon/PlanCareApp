import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { envPath } from './env';
import { StateCode, Vehicle, VehicleStatus } from './model/vehicle';

@Injectable({
  providedIn: 'root'
})
export class VehicleServiceService 
{

  private path = envPath.apiUrl;

  constructor(private client: HttpClient) 
  { 

  }

  GetVehicleData(args: any = {}): Observable<any>
  {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    const options = args.value ? {params: new HttpParams().set(args.param, args.value), headers: header} : {headers: header};
    return this.client.get(this.path + 'Backend/GetVehicles', options);
  }

  ConvertToEndUserFormat(vehicles: Vehicle[])
  {
    for(let i in vehicles)
    {
      vehicles[i].registrationState = StateCode[+vehicles[i].registrationState];
      vehicles[i].vehicleStatus = VehicleStatus[+vehicles[i].vehicleStatus];
    }

    return vehicles;
  }
}
