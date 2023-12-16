using MeetingManagement.Model.DataTransferObjects;
using MeetingManagement.Model.Entities;
using MeetingManagement.Repository;

namespace MeetingManagement.Service
{
    public interface IMeetingService
    {
        IEnumerable<Meeting> MeetingsByType(MeetingType type);
        long SaveMeeting(Meeting meeting, IEnumerable<long> meetingItemIdentifiers);
        long AttachMeetingItem(Meeting meeting, MeetingItem meetingItem);
        IEnumerable<Meeting> AllMeetings();
        Meeting? MeetingById(long id);
        IEnumerable<MeetingItem> AllMeetingItems();
        long SaveMeetingItem(MeetingItem meetingItem);

        IEnumerable<MeetingDto> GetAllMeetings();
    }
    public class MeetingService : IMeetingService
    {
        public IMeetingRepository MeetingRepository { get; }

        public IMeetingItemRepository MeetingItemRepository { get; }

        public IMeetingItemsRepository MeetingItemsRepository { get; }
        public MeetingService(
            IMeetingRepository meetingRepo,
            IMeetingItemRepository meetingItemRepo,
            IMeetingItemsRepository meetingItemsRepo)
        {
            MeetingItemRepository = meetingItemRepo;
            MeetingRepository = meetingRepo;
            MeetingItemsRepository = meetingItemsRepo;
        }

        public long SaveMeetingItem(MeetingItem meetingItem)
        {
            try
            {
                if(meetingItem.Id > 0)
                {
                    return this.MeetingItemRepository.Update(meetingItem);
                }
                else
                {
                    return this.MeetingItemRepository.Add(meetingItem);
                }
            }
            catch(Exception ex)
            {
                //log exception
                return 0;

            }
        }

        public IEnumerable<MeetingDto> GetAllMeetings()
        {
            return this.AllMeetings().ToList().Select(meeting =>
            {
                var meetingItemIds = this.MeetingItemsRepository
                .GetQueryable(x => x.MeetingId == meeting.Id).Select(x => x.MeetingItemId).ToList();
                return new MeetingDto
                {
                    Id = meeting.Id,
                    Items = meetingItemIds,
                    Notes = meeting.Notes,
                    Number = meeting.Number,
                    Type = meeting.Type
                };
            });
        }

        public IEnumerable<MeetingItem> AllMeetingItems()
        {
            try
            {
                return this.MeetingItemRepository.GetQueryable(x => true).AsEnumerable();
            }
            catch(Exception ex) 
            {
                //log exception
                return Enumerable.Empty<MeetingItem>();
            }
           
        }
        public IEnumerable<Meeting> AllMeetings()
        {
            try
            {
                return MeetingRepository.GetQueryable(x => true).AsEnumerable();
            }
            catch (Exception ex)
            {
                //log exception
                return Enumerable.Empty<Meeting>();
            }
        }

        public long AttachMeetingItem(Meeting meeting, MeetingItem meetingItem)
        {
            try
            {
                if(meeting.Id > 0 && meetingItem.Id > 0)
                {
                    var addedMeetingItemLink = new MeetingItems { MeetingId = meeting.Id, MeetingItemId = meetingItem.Id };
                    this.MeetingItemsRepository.Add(addedMeetingItemLink);
                    return addedMeetingItemLink.Id;
                }
                else
                {
                    throw new Exception("Invalid Argument(s) passed, both meeting ID and meeting item ID should be greater than zero");
                    
                }
            }
            catch(Exception ex)
            {
                //log exception
                return 0;
            }
        }

        public IEnumerable<Meeting> MeetingsByType(MeetingType type)
        {
            try
            {
                return this.MeetingRepository.GetQueryable(x => x.Type == type)
                .AsEnumerable();
            }
            catch(Exception ex)
            {
                //log exception
                return Enumerable.Empty<Meeting>();
            }
        }

        public long SaveMeeting(Meeting meeting, IEnumerable<long> meetingItemIdentifiers)
        {
            try
            {
                var meetingId = meeting.Id;
                if (meeting.Id == 0)
                {
                    meetingId = this.MeetingRepository.Add(meeting);
                }
                else
                {
                    this.MeetingRepository.Update(meeting);
                }

                if (meetingItemIdentifiers.Any())
                {
                    this.MeetingItemsRepository
                        .GetQueryable(x => x.MeetingId == meeting.Id)
                        .ToList()
                        .ForEach(removed =>
                        {
                            this.MeetingItemsRepository.Delete(removed.Id);
                        });
                    meetingItemIdentifiers.ToList().ForEach(id =>
                    {
                        this.MeetingItemsRepository.Add(new MeetingItems { MeetingId = meeting.Id, MeetingItemId = id });
                    });
                }
                return meetingId;
            }
            catch(Exception ex)
            {
                //log execption
                return 0;
            }
        }

        public Meeting? MeetingById(long id)
        {
            try
            {
                return this.MeetingRepository.GetQueryable(x => x.Id == id).FirstOrDefault();
            }
            catch(Exception ex)
            {
                //log exception
                return null;
            }
        }
    }
}
