import { Component, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Http } from '@angular/http';
import { MatDialog, MatDialogRef } from '@angular/material';

@Component({
    templateUrl: './monster-picker.component.html'
})
export class MonsterPickerComponent {
    allMonsters: any[] = [];
    monsters: any[] = [];
    title: string = "Pick Monster";
    selection: null;

    constructor(
        private dialogRef: MatDialogRef<MonsterPickerComponent>,
        private httpService: Http) {

    }

    ngOnInit() {
        this.load();
    }

    closeDialog() {
        this.dialogRef.close();
    }

    selectMonster() {
        this.dialogRef.close(this.selection);
    }

    filterMonsters(value) {
        let re = new RegExp(value, 'gi');
        this.monsters = this.allMonsters.filter(m => {
            return m.name != null && m.name.match(re);
        });
    }

    select(monster) {
        this.selection = monster;
        console.log(monster);
    }

    load() {
        this.httpService.get('/api/monsters').subscribe(values => {
            this.allMonsters = this.monsters = values.json();// as string[];
        });
    }

    getAbilityModifier(ability) {
        return Math.floor((ability - 10) / 2);
    }
}
