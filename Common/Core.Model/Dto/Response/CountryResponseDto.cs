using Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model.Dto.Response
{
    public class CountryResponseDto
    {
        public Guid UniqueCountryId { get; set; }
        public Guid UniquePassengerId { get; set; }
        public string Country => CountryType.ToString();
        public CountryType CountryType { get; set; }
    }
}
