import { Component, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material';

@Component({
    templateUrl: './sign-in.component.html'
})
export class SignInComponent {
    sbCounter: number = 0;

    constructor(private snackBar: MatSnackBar) { }

    onSignInClick() {
        this.snackBar.open('Clicked to sign in (' + this.sbCounter + ')', null,
            {
                duration: 3000,
                horizontalPosition: 'center',
                verticalPosition: 'bottom',
                extraClasses: null
            });

        this.sbCounter ++;
    }
}
