import { RouterModule, Routes } from '@angular/router';
import { Authentication } from './components/pages/authentication/authentication';
import { AuthGuard } from './shared/guards/auth.guard';
import { Landing } from './components/pages/landing/landing';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';

export const routes: Routes = [
    {
        path: 'login',
        component: Authentication
    },
    {
        path: '',
        canActivate: [AuthGuard],
        component: Landing
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
        FormsModule,
        ReactiveFormsModule,
        MatDialogModule,
    ],
    declarations: [],
    exports: [
        RouterModule
    ],
    providers: [AuthGuard]
})
export class RegisterRouter { }