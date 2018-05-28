import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { Routes, RouterModule } from '@angular/router';
import { MatButtonModule, MatCheckboxModule, MatInputModule, MatRadioModule, MatSelectModule, MatSlideToggleModule } from '@angular/material';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { SignInComponent } from './sign-in.component';

const routes: Routes = [
    { path: '', redirectTo: '../', pathMatch: 'full' },
    { path: 'sign-in', component: SignInComponent }
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        RouterModule.forChild(routes),
        MatButtonModule, MatCheckboxModule, MatInputModule, MatRadioModule, MatSelectModule, MatSlideToggleModule,
        MatSnackBarModule
    ],
    declarations: [
        SignInComponent
    ],
    exports: [],
    providers: [
    ]
})
export class SecurityModule { }
