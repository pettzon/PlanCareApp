import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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

  GetVehicleData(args: any = {}): Observable<any>
  {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    const options = args.value ? {params: new HttpParams().set(args.param, args.value), headers: header} : {headers: header};
    return this.client.get(this.path + 'Backend/GetVehicles', options);
  }
}
