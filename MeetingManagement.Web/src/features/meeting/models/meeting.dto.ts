export class MeetingDto{
  type = MeetingType.MANCO;
  number = "";
  notes  = "";
  id = 0;
  items: Array<number> = []
}

export const enum MeetingType
{
    MANCO = 1,
    Finance = 2,
    PTL = 3
}
