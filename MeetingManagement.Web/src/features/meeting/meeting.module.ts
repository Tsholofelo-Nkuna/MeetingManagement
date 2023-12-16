import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { MeetingComponent } from './components/meeting/meeting.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { OverlayModule } from '@angular/cdk/overlay';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { MeetingService } from './services/meeting.service';
import { HttpClientModule } from '@angular/common/http';
let routes: Routes = [
  {
    path: '',
    component: MeetingComponent
  }
]

@NgModule({
  declarations: [
    MeetingComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    OverlayModule,
    MatTabsModule,
    MatSelectModule,
    HttpClientModule,
    RouterModule.forChild(routes)
  ],
  providers: [
    MeetingService
  ]
})
export class MeetingModule { }
