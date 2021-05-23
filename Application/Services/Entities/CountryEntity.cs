using Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Entities
{
    public class CountryEntity
    {
        public PassengerEntity Passenger { get; set; }
        public Guid UniqueCountryId { get; set; }
        public Guid UniquePassengerId { get; set; }
        public CountryType CountryType { get; set; }
    }
}
