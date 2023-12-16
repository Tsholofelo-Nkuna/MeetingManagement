using MeetingManagement.Model.DataTransferObjects;
using MeetingManagement.Model.Entities;
using MeetingManagement.Service;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetingManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private IMeetingService _meetingService;
        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }
        // GET: api/<MeetingController>
        [HttpGet]
        public IEnumerable<MeetingDto> Get()
        {
           return _meetingService.GetAllMeetings();
        }

        // GET api/<MeetingController>/5
        [HttpGet("{id}")]
        public Meeting? Get(int id)
        {
            return _meetingService.MeetingById(id);
        }

        // POST api/<MeetingController>
        //Create or update meeting
        [HttpPost]
        public long Post([FromBody] MeetingDto meeting)
        {
            return this._meetingService.SaveMeeting(
                new Meeting { Id = meeting.Id, Notes = meeting.Notes, Number = meeting.Number, Type = meeting.Type }, 
                meeting.Items);
        }

        // PUT api/<MeetingController>/5/AddItem
        [HttpPost("{id}/AddItem")]
        public long Post(int id, [FromBody] MeetingItem item)
        {
            var meeting = this._meetingService.MeetingById(id);
            if(meeting != null)
            {
                return this._meetingService.AttachMeetingItem(meeting, item);
            }
            else
            {
                return 0;
            }
        }

        [HttpGet("Item")]
        public IEnumerable<MeetingItem> GetAllMeetingItems()
        {
            return this._meetingService.AllMeetingItems();
        }

        [HttpPost("Item")]
        public long SaveMeetingItem([FromBody] MeetingItem meetingItem)
        {
            return this._meetingService.SaveMeetingItem((meetingItem));
        }

        // DELETE api/<MeetingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
