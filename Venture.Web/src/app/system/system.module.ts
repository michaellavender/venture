import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';
import { MatButtonModule, MatCheckboxModule, MatInputModule, MatRadioModule, MatSelectModule, MatSlideToggleModule } from '@angular/material';

import { TenantsComponent } from './tenants.component';

const routes: Routes = [
    { path: '', redirectTo: '../', pathMatch: 'full' },
    { path: 'tenants', component: TenantsComponent }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        RouterModule.forChild(routes),
        MatButtonModule, MatCheckboxModule, MatInputModule, MatRadioModule, MatSelectModule, MatSlideToggleModule
    ],
    declarations: [
        TenantsComponent
    ],
    exports: [],
    providers: [
    ]
})
export class SystemModule { }
