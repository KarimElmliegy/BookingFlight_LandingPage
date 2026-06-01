import { Contact } from './Pages/contact/contact';
import { Routes } from '@angular/router';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { MainLayout } from './layouts/main-layout/main-layout';
import { Login } from './Pages/auth/login/login/login';
import { Register } from './Pages/auth/register/register/register';
import { HomePage } from './Pages/Home/home';
import { authGuard } from '../Core/guards/auth.guard';
import { MyTickets } from './Pages/MyTickets/my-tickets';
import { Checkout } from './Pages/checkout/checkout/checkout';

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
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      { path: 'home', component: HomePage },
      { path: 'contact', component: Contact },
      { path: 'My-Tickets', component: MyTickets },
      { path: 'checkout', component: Checkout },
    ],
  },
];
