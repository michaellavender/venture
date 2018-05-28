import { Component, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Http } from '@angular/http';

@Component({
    templateUrl: './monster-list.component.html'
})
export class MonsterListComponent {
    monsters: any[] = [];

    constructor(private httpService: Http) { }

    ngOnInit() {
        this.load();
    }

    load() {
        this.httpService.get('/api/monsters').subscribe(values => {
            this.monsters = values.json();// as string[];
        });
    }
}
