import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';
import { MatButtonModule, MatCheckboxModule, MatInputModule, MatListModule, MatRadioModule, MatSelectModule, MatSlideToggleModule } from '@angular/material';

import { EncounterBuildComponent } from './encounter-build.component';

const routes: Routes = [
    //{ path: '', redirectTo: '../', pathMatch: 'full' },
    { path: '', component: EncounterBuildComponent }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        RouterModule.forChild(routes),
        MatButtonModule, MatCheckboxModule, MatListModule, MatInputModule, MatRadioModule, MatSelectModule, MatSlideToggleModule
    ],
    declarations: [
        EncounterBuildComponent
    ],
    exports: [],
    providers: [
    ]
})
export class EncountersModule { }
