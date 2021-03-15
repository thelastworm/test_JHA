import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TasksComponent } from './tasks/tasks.component';
import {DetailsComponent} from './details/details.component'


const appRoutes: Routes = [
  { path: 'tasks', component: TasksComponent },
  { path: 'task/:id', component: DetailsComponent },
  { path: '',   redirectTo: '/tasks', pathMatch: 'full' },
  { path: '**', component: TasksComponent }
   
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {}