import { Routes } from '@angular/router';

import { Home } from './pages/home/home';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Catalog } from './pages/catalog/catalog';
import { ProductDetail } from './pages/product-detail/product-detail';
import { Cart } from './pages/cart/cart';
import { Profile } from './pages/profile/profile';
import { AdminDashboard } from './pages/admin-dashboard/admin-dashboard';

import { adminGuard } from './core/guards/admin.guard';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    component: Login
  },

  {
    path: 'register',
    component: Register
  },

  {
    path: 'home',
    component: Home
  },

  {
    path: 'catalog',
    component: Catalog
  },

  {
    path: 'product/:id',
    component: ProductDetail
  },

  {
    path: 'cart',
    component: Cart
  },

  {
    path: 'profile',
    component: Profile,
    canActivate: [authGuard]
  },

  {
    path: 'admin',
    component: AdminDashboard,
    canActivate: [adminGuard]
  },

  {
    path: '**',
    redirectTo: 'login'
  }
];