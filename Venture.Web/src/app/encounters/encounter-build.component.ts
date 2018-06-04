import { Component, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Http } from '@angular/http';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MonsterPickerComponent } from '../monsters/monster-picker.component';

class Encounter {
    id: number;
    name: string;
    monsters: any[];
}

@Component({
    templateUrl: './encounter-build.component.html'
})
export class EncounterBuildComponent {
    vm: Encounter = new Encounter();
    monsters: any[] = [];

    constructor(
        private httpService: Http,
        private dialog: MatDialog) { }

    ngOnInit() {
        this.load();
    }

    load() {
        this.httpService.get('/api/monsters').subscribe(values => {
            this.monsters = values.json();// as string[];
        });
    }

    addMonster() {
        if (this.vm.monsters == null)
            this.vm.monsters = [];

        let dialogRef = this.dialog.open(MonsterPickerComponent,
            {
            });
        dialogRef.afterClosed().subscribe(result => {
            if (result != null) {
                this.vm.monsters.push({ monster: result, count: 1 });
            }
        });
    }

    deleteMonster(del) {
        var index = this.vm.monsters.indexOf(del);
        if (index > -1) {
            this.vm.monsters.splice(index, 1);
        }
    }

    getChallengeRating() {
        if (this.vm.monsters == null) return 0;

        var total = 0;
        this.vm.monsters.forEach((em) => {
            var m = this.monsters.find(m => m.name === em.name);
            if (m) {
                total += (eval(m.challengeRating) * em.count);
            }
        });

        var wholeTotal = Math.floor(total);

        if (total === wholeTotal)
            return wholeTotal;

        if (total % 0.5 === 0) {
            return wholeTotal + " 1/2";
        }

        if (total % 0.25 === 0) {
            return wholeTotal + " " + ((total - wholeTotal) / 0.25) + "/4";
        }

        if (total % 0.125 === 0) {
            return wholeTotal + " " + ((total - wholeTotal) / 0.125) + "/8";
        }

        return total;
    }
}
