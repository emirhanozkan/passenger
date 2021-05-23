using Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model.Dto.Response
{
    public class PassengerResponseDto
    {
        public PassengerResponseDto()
        {
            Statuses = new List<StatusResponseDto>();
            Countries = new List<CountryResponseDto>();
        }
        public Guid UniquePassengerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender => GenderType.ToString();
        public GenderType GenderType { get; set; }
        public string Document => DocumentType.ToString();
        public DocumentType DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public string IssueDate { get; set; }
        public List<StatusResponseDto> Statuses { get; set; }
        public List<CountryResponseDto> Countries { get; set; }
    }
}
