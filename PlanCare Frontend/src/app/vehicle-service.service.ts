import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { envPath } from './env';

@Injectable({
  providedIn: 'root'
})
export class VehicleServiceService 
{

  private path = envPath.apiUrl;

  constructor(private client: HttpClient) 
  { 

  }

  GetVehicleData(): Observable<any>
  {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.client.get(this.path + 'Backend/GetVehicles', {headers: header});
  }
}
