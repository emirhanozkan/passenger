using Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model.Dto.Request
{
    public class PassengerRequestDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public GenderType GenderType { get; set; }
        public DocumentType DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public DateTime IssueDate { get; set; }
        public StatusType? StatusType { get; set; }
        public CountryType? CountryType { get; set; }
    }
}
