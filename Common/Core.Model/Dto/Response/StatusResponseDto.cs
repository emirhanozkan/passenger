using Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model.Dto.Response
{
    public class StatusResponseDto
    {
        public Guid UniqueStatusId { get; set; }
        public Guid UniquePassengerId { get; set; }
        public string Status => StatusType.ToString();
        public StatusType StatusType { get; set; }
    }
}
