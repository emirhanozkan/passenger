using Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Entities
{
    public class StatusEntity
    {
        public PassengerEntity Passenger { get; set; }
        public Guid UniqueStatusId { get; set; }
        public Guid UniquePassengerId { get; set; }
        public StatusType StatusType { get; set; }
    }
}
