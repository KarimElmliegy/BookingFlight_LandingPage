import { Routes } from '@angular/router';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { MainLayout } from './layouts/main-layout/main-layout';
import { Login } from './Pages/auth/login/login/login';
import { Register } from './Pages/auth/register/register/register';
import { HomePage } from './Pages/Home/home';
import { authGuard } from '../Core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    component: AuthLayout,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: Login },
      { path: 'register', component: Register },
    ],
  },
  {
    path: 'home',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      { path: '', component: HomePage },
    ],
  },
];
