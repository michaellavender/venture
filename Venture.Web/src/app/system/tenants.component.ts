import { Component, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { Http } from '@angular/http';

@Component({
    templateUrl: './tenants.component.html'
})
export class TenantsComponent {
    tenants: any[] = [];

    constructor(private httpService: Http) { }

    ngOnInit() {
        this.load();
    }

    addItem() {
        this.httpService.post('/api/system', {}).subscribe(() => {
            this.load();
        });
    }

    load() {
        this.httpService.get('/api/system').subscribe(values => {
            this.tenants = values.json();// as string[];
        });
    }
}
