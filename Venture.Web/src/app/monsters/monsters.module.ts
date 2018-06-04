import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';
import { MatCardModule, MatDividerModule, MatButtonModule, MatCheckboxModule, MatInputModule, MatListModule, MatRadioModule, MatSelectModule, MatSlideToggleModule, MatDialogModule } from '@angular/material';

import { MonsterListComponent } from './monster-list.component';
import { MonsterPickerComponent } from './monster-picker.component';

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
        MatCardModule, MatDividerModule, MatButtonModule, MatCheckboxModule, MatListModule, MatInputModule, MatRadioModule, MatSelectModule, MatSlideToggleModule, MatDialogModule
    ],
    declarations: [
        MonsterListComponent,
        MonsterPickerComponent
    ],
    exports: [MonsterPickerComponent],
    providers: [
    ],
    entryComponents: [MonsterPickerComponent]
})
export class MonstersModule { }
