import { WorkPlanComponent } from './work-plan/work-plan.component';
import { OcComponent } from './setting/oc/oc.component';
import { OcUserComponent } from './setting/oc-user/oc-user.component';
import { AccountComponent } from './setting/account/account.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TodolistComponent } from './todolist/todolist.component';
const routes: Routes = [
  {
    path: '',
    data: {
      title: 'ess',
      breadcrumb: 'Home'
    },
    children: [

      //setting
      {
        path: 'setting',
        data: {
          title: 'setting',
          breadcrumb: 'Setting'
        },
        children: [
          {
            path: 'account',
            component: AccountComponent,
            data: {
              title: 'account',
              breadcrumb: 'Account'
            }
          },
          {
            path: 'oc',
            component: OcComponent,
            data: {
              title: 'OC',
              breadcrumb: 'OC'
            }
          },
          {
            path: 'oc-user',
            component: OcUserComponent,
            data: {
              title: 'OC User',
              breadcrumb: 'OC User'
            }
          },
        ]
      },

      //ToDoList
      {
        path: 'todolist',
        data: {
          title: 'Todolist'
        },
        component: TodolistComponent,

      },

      //Work-Plan
      {
        path: 'work-plan',
        data: {
          title: 'Work Plan'
        },
        component: WorkPlanComponent,
      },

    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule {}
