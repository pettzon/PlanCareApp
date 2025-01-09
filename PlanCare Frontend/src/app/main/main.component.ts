import { Component, inject, OnInit } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { VehicleServiceService } from '../vehicle-service.service';
import { Vehicle, StateCode } from '../model/vehicle';

@Component({
  selector: 'app-main',
  imports: [MaterialModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit
{
  data: any = [];
  columnData: string[] =
  [
    'make',
    'registrationNumber',
    'registrationState',
    'registrationDate',
    'expirationDate'
  ];

  ngOnInit(): void 
  {
    this.GetVehicleData();
  }

  constructor(private service: VehicleServiceService)
  {
    
  }

  GetVehicleData()
  {
    this.service.GetVehicleData().subscribe(
    {
      next: (res :any) => 
      {
        this.data = res;
        console.log(this.data);
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
  }
}
