import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { homeRoutes } from './core/routing/home-routes';


const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot(homeRoutes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
