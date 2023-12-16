import { FieldConfig } from "../../../../shared/classes/field-config";
import { MeetingItemDto } from "../../models/meeting-item.dto";
import { MeetingDto, MeetingType } from "../../models/meeting.dto";

export class MeetingConfig{

  columnConfig: Array<FieldConfig<any, MeetingDto>> = [
    {label: 'Meeting No.', key: 'number', controlType: 'input'},
    {label: 'Type', key: 'type', controlType: 'select'},
    {label: 'Items', key: 'items', controlType: 'select'},
    {label: 'Notes', key: 'notes', controlType: 'input'}
  ];

  meetingItemFieldConfig: Array<FieldConfig<any, MeetingItemDto>> = [
    { label: 'Name', key: 'name', controlType: 'input'},
    { label: 'Action', key: 'action', controlType: 'input'},
    { label: 'Status', key: 'status', controlType: 'input'}
  ]

  meetingTypeOptions: {label: string, value: MeetingType}[] = [
    { label: 'MANCO', value: MeetingType.MANCO },
    { label: 'Finance', value: MeetingType.Finance},
    { label: 'PTL', value: MeetingType.PTL }
  ]
}
