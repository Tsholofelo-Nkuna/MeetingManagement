import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'meeting', loadChildren: () => import('../features/meeting/meeting.module').then(m => m.MeetingModule)},
  {path: '**', redirectTo: 'meeting'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
