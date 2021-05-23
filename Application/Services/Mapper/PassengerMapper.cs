using Application.Services.Entities;
using Core.Model.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Mapper
{
    public static class PassengerMapper
    {
        public static PassengerResponseDto ToDto(this PassengerEntity passenger)
        {
            return new PassengerResponseDto
            {
                UniquePassengerId = passenger.UniquePassengerId,
                Name = passenger.Name,
                Surname = passenger.Surname,
                GenderType = passenger.GenderType,
                DocumentType = passenger.DocumentType,
                DocumentNo = passenger.DocumentNo,
                IssueDate = passenger.IssueDate.ToString("yyyy'-'MM'-'dd"),
                Countries = passenger.Countries.Select(x => new CountryResponseDto
                {
                    UniqueCountryId = x.UniqueCountryId,
                    UniquePassengerId = x.UniquePassengerId,
                    CountryType = x.CountryType
                }).ToList(),
                Statuses = passenger.Statuses.Select(x => new StatusResponseDto
                {
                    UniqueStatusId = x.UniqueStatusId,
                    UniquePassengerId = x.UniquePassengerId,
                    StatusType = x.StatusType
                }).ToList()
            };
        }
    }
}
