import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Router, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { MatToolbarModule, MatSidenavModule, MatButtonModule, MatIconModule, MatCheckboxModule } from '@angular/material';

import { AppComponent } from './app.component';
import { SecurityModule } from './security/security.module';
import { SystemModule } from './system/system.module';
import { MonstersModule } from './monsters/monsters.module';

import { APP_BASE_HREF } from '@angular/common';
export class MyHammerConfig extends HammerGestureConfig {
    overrides = <any>{
        'swipe': { velocity: 0.4, threshold: 20 } // override default settings
    }
}

const routes: Routes = [
    //{ path: '', pathMatch: 'full', redirectTo: 'first' },
    { path: 'security', loadChildren: './security/security.module#SecurityModule' },
    { path: 'system', loadChildren: './system/system.module#SystemModule' },
    { path: 'monsters', loadChildren: './monsters/monsters.module#MonstersModule' },
];

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes),//, { initialNavigation: false }), -- Put this back once routes are loaded asynch following authentication
        FormsModule,
        HttpModule,
        BrowserAnimationsModule,
        MatToolbarModule,
        MatSidenavModule,
        MatButtonModule,
        MatIconModule,
        MatCheckboxModule,
        SecurityModule,
        SystemModule,
        MonstersModule
    ],
    providers: [
        { provide: APP_BASE_HREF, useValue: '/' },
        { provide: HAMMER_GESTURE_CONFIG, useClass: MyHammerConfig }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
