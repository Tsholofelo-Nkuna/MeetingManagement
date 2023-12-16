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
    public interface IMeetingItemsRepository: IRepository<MeetingItems>
    {

    }
    public class MeetingItemsRepository: GenericRepository<MeetingItems>, IMeetingItemsRepository
    {
        public MeetingItemsRepository(WebDbContext dbContext): base(dbContext)  { }
    }
}
