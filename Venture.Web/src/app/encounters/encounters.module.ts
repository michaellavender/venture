import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';
import { MatButtonModule, MatCheckboxModule, MatInputModule, MatListModule, MatRadioModule, MatSelectModule, MatIconModule, MatSlideToggleModule, MatDialogModule } from '@angular/material';
import { MonstersModule } from '../monsters/monsters.module'

import { EncounterBuildComponent } from './encounter-build.component';

const routes: Routes = [
    //{ path: '', redirectTo: '../', pathMatch: 'full' },
    { path: '', component: EncounterBuildComponent },
    { path: 'new', component: EncounterBuildComponent }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        RouterModule.forChild(routes),
        MatButtonModule, MatCheckboxModule, MatListModule, MatInputModule, MatRadioModule, MatSelectModule, MatIconModule, MatSlideToggleModule, MatDialogModule,
        MonstersModule
    ],
    declarations: [
        EncounterBuildComponent
    ],
    exports: [],
    providers: [
    ]
})
export class EncountersModule { }
