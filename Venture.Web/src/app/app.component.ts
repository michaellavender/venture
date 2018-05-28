import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'venture-app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    windowSize: string;

    constructor(private httpService: Http) { }

    ngOnInit() {
        window.onresize = () => {
            this.setWindowSize();
        };

        this.setWindowSize();
    }

    setWindowSize() {
        this.windowSize = window.innerWidth <= 640 ? 'small' : window.innerWidth <= 1007 ? 'medium' : 'large';
    }
}
