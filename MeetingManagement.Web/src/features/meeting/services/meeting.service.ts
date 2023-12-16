import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MeetingDto } from '../models/meeting.dto';
import { MeetingItemDto } from '../models/meeting-item.dto';

@Injectable()
export class MeetingService {

  apiEndPoint = 'https://localhost:7076/api/Meeting';
  constructor(private _httpClient: HttpClient) { }

  getMeetings(){
    return this._httpClient.get<MeetingDto[]>(this.apiEndPoint)
  }

  updateOrCreateMeeting(meeting: MeetingDto){
   return this._httpClient.post<number>(this.apiEndPoint, meeting);
  }

  getItems(){
    return this._httpClient.get<Array<MeetingItemDto>>(`${this.apiEndPoint}/item`);
  }

  updateOrCreateItem(item: MeetingItemDto){
   return this._httpClient.post<number>(`${this.apiEndPoint}/item`, item);
  }
}
