import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './shared/guards/auth.guard';
import { Landing } from './components/pages/landing/landing';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { Login } from './components/pages/authentication/login/login';
import { Register } from './components/pages/authentication/register/register';
import { Launches } from './components/pages/launches/launches';

export const routes: Routes = [
    {
        path: 'login',
        component: Login
    },
    {
        path: 'register',
        component: Register
    },
    {
        path: '',
        canActivate: [AuthGuard],
        component: Landing
    },
    {
        path: 'launches',
        canActivate: [AuthGuard],
        component: Launches
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes),
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