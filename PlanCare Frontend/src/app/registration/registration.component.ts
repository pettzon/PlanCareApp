import { Component, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { MaterialModule } from '../material/material.module';
import { StateCode, Vehicle } from '../model/vehicle';
import { VehicleServiceService } from '../vehicle-service.service';
import { envPath } from '../env';
import { Observable, Subject } from 'rxjs';


@Component({
  selector: 'app-registration',
  imports: [MaterialModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss'
})
export class RegistrationComponent implements OnInit
{
  data: any = [];
  dataSource: Vehicle[] = [];
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

  constructor(private service: VehicleServiceService)
  {
  }

  async ngOnInit()
  {
    this.hubConnection.on('UpdateExpirationStatus', (v: Vehicle[]) => {console.log(v); this.UpdateExpirationStatus(v)});
    this.hubConnection.on('SendAllVehicleData', (v: Vehicle[]) => {console.log(v); this.dataSource = this.service.ConvertToEndUserFormat(v);});

    await this.hubConnection.start().then(() => console.log("Starting SignalR Connection")).catch(e => console.log(e));
    this.hubConnection.invoke('FetchVehicleData');
  }

  UpdateExpirationStatus(expiredVehicles: Vehicle[])
  {
    expiredVehicles = this.service.ConvertToEndUserFormat(expiredVehicles);
    
    expiredVehicles.forEach(ev => 
    {
      this.dataSource.forEach(v =>
      {
        if(ev.registrationNumber == v.registrationNumber)
        {
          v.vehicleStatus = ev.vehicleStatus;
        }
      })
    })
  }

      /* convert from enum values to strings. Sending byte-sized enums over the network is more efficient, and less data stored in a database */
    // for(var i in this.data)
    // {
    //     this.data[i].registrationState = StateCode[this.data[i].registrationState];
    // }
  
    // this.dataSource = this.data;
    // }
}
