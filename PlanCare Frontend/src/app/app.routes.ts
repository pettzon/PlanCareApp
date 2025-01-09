import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { RegistrationComponent } from './registration/registration.component';
import { MainComponent } from './main/main.component';

export const routes: Routes = 
[
    {path: '', component: MainComponent},
    {path: 'registration', component: RegistrationComponent}
];
