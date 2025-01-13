import { Component, inject, OnInit } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { VehicleServiceService } from '../vehicle-service.service';
import { Vehicle, StateCode } from '../model/vehicle';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-main',
  imports: [MaterialModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit
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
  ];

  ngOnInit(): void 
  {
    this.activatedRoute.queryParamMap.subscribe((p) => 
      {
        // Can be made better by reading p and iterating through an array of possible parameters rather than hard-coding "make" and sending the
        // matching ones as args to the HTTPparams . Used this way for simplicity currently

        const args = {param:"make", value:p.get('make') as string};
        this.GetVehicleData(args);
        console.log(args);
      });
  }

  constructor(private service: VehicleServiceService, private activatedRoute: ActivatedRoute)
  {

  }

  GetVehicleData(args: any = {})
  {
    // Unable to put enum->string conversion in event, as it switches between values for some reason
    // Would much rather have converted the data on the spot instead of moving it between arrays
    // Such as res[i].registrationState = StateCode[res[i].registrationState];
    // This would have elimninated the use of the extra array data (or dataSource) I would love to know how to do this properly

    this.service.GetVehicleData(args).subscribe(
    {
      next: (res :any) => 
      {
        this.data = res;
        //console.log(this.data);
      },
      error: (error: any) =>
      {
        console.log(error);
      }
    })

    this.dataSource = this.service.ConvertToEndUserFormat(this.data);
  }
}
