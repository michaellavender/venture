import { Component, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Http } from '@angular/http';

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

    constructor(private httpService: Http) { }

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

        this.vm.monsters.push({});
        console.log('monsters: ', this.vm.monsters);
    }
}
