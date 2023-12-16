using MeetingManagement.DAL;
using MeetingManagement.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManagement.Repository
{
    public interface IMeetingItemRepository: IRepository<MeetingItem>
    {

    }
    public class MeetingItemRepository: GenericRepository<MeetingItem>, IMeetingItemRepository 
    {
        public MeetingItemRepository(WebDbContext dbContext): base(dbContext) { }
    }
}
