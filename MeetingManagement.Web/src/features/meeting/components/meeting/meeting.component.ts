import { Component, OnInit, inject } from '@angular/core';
import { MeetingDto, MeetingType } from '../../models/meeting.dto';
import { MeetingConfig } from './meeting.config';
import { ConnectedPosition } from '@angular/cdk/overlay';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MeetingItemDto } from '../../models/meeting-item.dto';
import { MeetingService } from '../../services/meeting.service';
import { catchError, forkJoin, of } from 'rxjs';

export type FormType = 'meeting' | 'item';

@Component({
  selector: 'app-meeting',
  templateUrl: './meeting.component.html',
  styleUrls: ['./meeting.component.scss']
})
export class MeetingComponent implements OnInit {

  meetings: Array<MeetingDto> = [];
  meetingItems: Array<MeetingItemDto> = [];
  meetingsClone: Array<MeetingDto> = [];
  meetingItemsClone: Array<MeetingItemDto> = [];
  newMeeting = new MeetingDto();
  config = new MeetingConfig();
  overlayIsOpen = false;
  overlayPositions: ConnectedPosition[] = [
    {originX: 'start', overlayX: 'center', originY: 'center', overlayY: 'center'}
  ]
  itemOverlayIsOpen = false;

  formBuilder = inject(FormBuilder);
  newMeetingForm: FormGroup;
  itemForm: FormGroup;
  editIndex = -1;
  private _meetingService = inject(MeetingService);
  constructor() {
    var formConfig=  this.config.columnConfig.reduce((carry, next) => {

     const partialObj =  {[next.key]: new FormControl(null) }
     return {...carry, ...partialObj}
      }, {} as {[k: string]: FormControl});
      this.newMeetingForm = this.formBuilder.group(formConfig);

     const itemFormConfig = this.config.meetingItemFieldConfig.reduce((carry, next) => {
       return ({...carry, ...{[next.key]: new FormControl(null)}})
     }, {} as {[k: string]: FormControl});
     this.itemForm = this.formBuilder.group(itemFormConfig);
  }

  ngOnInit(): void {
    this.getData();
  }

  getControl(controlKey: string, formType: FormType){
   return  (formType === 'meeting' ? this.newMeetingForm.get(controlKey) : this.itemForm.get(controlKey)) as FormControl<any>;
  }

  saveNew(formType: FormType){
    if(formType === 'meeting'){
      let meetingToBeSaved = {...this.newMeetingForm.value, id:0} as MeetingDto;
      this._meetingService.updateOrCreateMeeting(meetingToBeSaved)
      .pipe(catchError(err => {
        alert("Failed To Create Meeting");
        return of(0);
      }))
      .subscribe(response=> {
        if(response > 0){
          alert('Meeting Created!');
          this.overlayIsOpen = false;
          this.newMeetingForm.reset();
          this.getData();
        }
      })
    }
    else if(formType === 'item'){
        this._meetingService.updateOrCreateItem({...this.itemForm.value, id:0} as MeetingItemDto)
        .pipe(catchError(err => {
          alert('Failed To Create Item!');
          return of(0);
        })).subscribe(response => {
          alert('Item Created!');
          this.itemOverlayIsOpen = false;
          this.itemForm.reset();
          this.getData();
        })
    }
  }

  editRecord(recordType: FormType, record: MeetingDto | MeetingItemDto){
    if(recordType === 'meeting'){
       this._meetingService.updateOrCreateMeeting(record as MeetingDto)
      .pipe(catchError(err => {
        alert("Meeting Updated Failed!");
        return of(0);
      }))
      .subscribe(response=> {
        if(response > 0){
          alert('Meeting Updated!');
          this.editIndex = -1;
          this.getData();
        }
      })
    }
    else{
      this._meetingService.updateOrCreateItem(record as MeetingItemDto)
        .pipe(catchError(err => {
          alert('Failed To Update Item!');
          return of(0);
        })).subscribe(response => {
          alert('Item Updated!');
          this.editIndex = -1;
          this.getData();
        })
    }
  }

  display(meeting: MeetingDto, key: keyof MeetingDto){
    if(key === 'type'){
      switch(meeting[key]){
        case MeetingType.MANCO:
        {
          return 'MANCO';
        }
        case MeetingType.Finance:{
          return 'Finance';
        }
        case MeetingType.PTL: {
          return 'PTL'
        }
        default: {
          return '';
        }
      }
    }
    else if(key === 'items'){
     return meeting[key].map(itemId => this.meetingItemsClone.find(items => items.id === itemId)?.name ?? '');
    }
    else{
        return meeting[key];
    }
  }

  getData(){
    forkJoin([this._meetingService.getMeetings(), this._meetingService.getItems()])
    .subscribe(([meetings,items]) => {
      if(items){
        this.meetingItems = items;
        this.meetingItemsClone = this.meetingItems.map(x => ({...x}));
      }
     if(meetings){
        this.meetings = meetings;
        this.meetingsClone = this.meetings.map(x => ({...x}));
      }
    }); 
  }

}
