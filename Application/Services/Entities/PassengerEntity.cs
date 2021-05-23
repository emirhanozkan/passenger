using Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Entities
{
    public class PassengerEntity
    {
        public PassengerEntity()
        {
            Countries = new List<CountryEntity>();
            Statuses = new List<StatusEntity>();
        }

        public Guid UniquePassengerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public GenderType GenderType { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public DateTime IssueDate { get; set; }
        public List<CountryEntity> Countries { get; set; }
        public List<StatusEntity> Statuses { get; set; }
    }
}
