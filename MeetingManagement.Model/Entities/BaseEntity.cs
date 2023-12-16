using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManagement.Model.Entities
{
    public class BaseEntity<TId> where TId: struct
    {
       public TId Id { get; set; }
    }
}
