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
    public interface IMeetingRepository: IRepository<Meeting>
    {

    }
    public class MeetingRepository: GenericRepository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(WebDbContext dbContext): base(dbContext) { }
    }
}
