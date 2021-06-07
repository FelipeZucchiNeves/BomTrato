import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatComponent } from './chat/chat.component';
import { HomeComponent } from './navegacao/home/home.component';
import { NotFoundComponent } from './navegacao/not-found/not-found.component';

const routes: Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home',  component: HomeComponent},
  {
    path: 'conta',
    loadChildren: () => import('./account/account.module')
      .then(m => m.AccountModule)
  },
  {
    path: 'aprovadores',
    loadChildren: () => import('./aprover/aprover.module')
      .then(m => m.AproverModule)
  },
  {
    path: 'escritorios',
    loadChildren: () => import('./office/office.module')
      .then(m => m.OfficeModule)
  },
  {
    path: 'processos',
    loadChildren: () => import('./process/process.module')
      .then(m => m.ProcessModule)
  },
  {path: 'chat', component: ChatComponent},
  {path: '**', component: NotFoundComponent},
  {path: 'nao-encontrado', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
