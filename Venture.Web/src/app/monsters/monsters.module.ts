import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';
import { MatButtonModule, MatCheckboxModule, MatInputModule, MatRadioModule, MatSelectModule, MatSlideToggleModule } from '@angular/material';

import { MonsterListComponent } from './monster-list.component';

const routes: Routes = [
    //{ path: '', redirectTo: '../', pathMatch: 'full' },
    { path: '', component: MonsterListComponent }
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
        MonsterListComponent
    ],
    exports: [],
    providers: [
    ]
})
export class MonstersModule { }
