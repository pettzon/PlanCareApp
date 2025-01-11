import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MaterialModule } from '../material/material.module';
import { StateCode, Vehicle } from '../model/vehicle';
import { VehicleServiceService } from '../vehicle-service.service';
import { envPath } from '../env';
import { Subject } from 'rxjs';


@Component({
  selector: 'app-registration',
  imports: [MaterialModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss'
})
export class RegistrationComponent implements OnInit
{
  data: any = [];
  dataSource: any = [];
  columnData: string[] =
  [
    'make',
    'registrationNumber',
    'registrationState',
    'registrationDate',
    'expirationDate',
    'vehicleStatus'
  ];

  hubConnection: signalR.HubConnection = new signalR.HubConnectionBuilder().withUrl(envPath.vehicleExpirationHub, {skipNegotiation: true,transport: signalR.HttpTransportType.WebSockets}).build();
  UpdateExpirationStatus = new Subject<Vehicle[]>();

  constructor(private service: VehicleServiceService)
  {
  }

  async ngOnInit()
  {
    this.GetVehicleData();
    this.hubConnection.on('UpdateExpirationStatus', (v) => {console.log(v)});
    this.hubConnection.start().then(() => console.log("Starting SignalR Connection")).catch(e => console.log(e));
  }

  GetVehicleData(args: any = {})
  {
    this.service.GetVehicleData(args).subscribe(
    {
      next: (res :any) => 
      {
        this.data = res;
      },
      error: (error: any) =>
      {
        console.log(error);
      }
    })
  
      /* convert from enum values to strings. Sending byte-sized enums over the network is more efficient, and less data stored in a database */
    for(var i in this.data)
    {
        this.data[i].registrationState = StateCode[this.data[i].registrationState];
    }
  
    this.dataSource = this.data;
    }
}
