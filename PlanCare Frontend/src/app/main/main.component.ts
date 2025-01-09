import { Component, inject, OnInit } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { VehicleServiceService } from '../vehicle-service.service';
import { Vehicle } from '../model/vehicle';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

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
    this.service.GetVehicleData().subscribe({
      next: (res :any) => 
      {
        this.data = res;
        console.log(res);
      },
      error: (error: any) =>
      {
        console.log(error);
      }
    })
  }
}
