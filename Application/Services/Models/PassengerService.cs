using Application.Services.Entities;
using Application.Services.Mapper;
using Core.Model.Dto.Request;
using Core.Model.Dto.Response;
using Core.Model.Enum;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Models
{
    public class PassengerService : IPassengerService
    {
        private List<PassengerEntity> passengerEntities { get; set; }
        private List<StatusEntity> statusEntities { get; set; }
        private List<CountryEntity> countryEntities { get; set; }
        private readonly ILogger<PassengerService> _logger;
        public PassengerService(ILogger<PassengerService> logger)
        {
            _logger = logger;
            passengerEntities = new List<PassengerEntity>();
            statusEntities = new List<StatusEntity>();
            countryEntities = new List<CountryEntity>();
        }

        public Guid InsertPassenger(PassengerRequestDto dto)
        {
            if (!dto.CountryType.HasValue && !dto.StatusType.HasValue)
                throw new Exception("Please choose a case type");
            Guid passengerId = Guid.NewGuid();
            passengerEntities.Add(new PassengerEntity
            {
                UniquePassengerId = passengerId,
                Name = dto.Name,
                Surname = dto.Surname,
                GenderType = dto.GenderType,
                DocumentType = dto.DocumentType,
                DocumentNo = dto.DocumentNo,
                IssueDate = dto.IssueDate
            });
            if (dto.CountryType.HasValue)
                InsertCountry(passengerId, dto.CountryType.Value);
            if (dto.StatusType.HasValue)
                InsertStatus(passengerId, dto.StatusType.Value);
            return passengerId;
        }
        private Guid InsertStatus(Guid passengerId, StatusType statusType)
        {
            var passenger = passengerEntities.FirstOrDefault(x => x.UniquePassengerId == passengerId);
            if (passenger is null) throw new Exception("Passenger not found");
            Guid uniqueStatusId = Guid.NewGuid();
            var entity = new StatusEntity
            {
                UniqueStatusId = uniqueStatusId,
                UniquePassengerId = passengerId,
                StatusType = statusType,
                Passenger = passenger
            };
            statusEntities.Add(entity);
            passenger.Statuses.Add(entity);
            return uniqueStatusId;
        }
        private Guid InsertCountry(Guid passengerId, CountryType countryType)
        {
            var passenger = passengerEntities.FirstOrDefault(x => x.UniquePassengerId == passengerId);
            if (passenger is null) throw new Exception("Passenger not found");
            Guid uniqueCountryId = Guid.NewGuid();
            var entity = new CountryEntity
            {
                UniqueCountryId = uniqueCountryId,
                UniquePassengerId = passengerId,
                CountryType = countryType,
                Passenger = passenger
            };
            countryEntities.Add(entity);
            passenger.Countries.Add(entity);
            return uniqueCountryId;
        }

        public Guid UpdatePassenger(Guid passengerId, PassengerRequestDto dto)
        {
            if (!dto.CountryType.HasValue && !dto.StatusType.HasValue)
                throw new Exception("Please choose a case type");
            var passenger = passengerEntities.FirstOrDefault(x => x.UniquePassengerId == passengerId);
            if (passenger is null) throw new Exception("Passenger not found");

            DeletePassenger(passengerId);

            passengerEntities.Add(new PassengerEntity
            {
                UniquePassengerId = passengerId,
                Name = dto.Name,
                Surname = dto.Surname,
                GenderType = dto.GenderType,
                DocumentType = dto.DocumentType,
                DocumentNo = dto.DocumentNo,
                IssueDate = dto.IssueDate
            });
            if (dto.CountryType.HasValue)
                InsertCountry(passengerId, dto.CountryType.Value);
            if (dto.StatusType.HasValue)
                InsertStatus(passengerId, dto.StatusType.Value);
            return passengerId;
        }

        public void DeletePassenger(Guid passengerId)
        {
            var passenger = passengerEntities.FirstOrDefault(x => x.UniquePassengerId == passengerId);
            if (passenger is null) throw new Exception("Passenger not found");
            foreach (var entity in countryEntities.Where(x => x.UniquePassengerId == passengerId).ToList())
                countryEntities.Remove(entity);
            foreach (var entity in statusEntities.Where(x => x.UniquePassengerId == passengerId).ToList())
                statusEntities.Remove(entity);
            passengerEntities.Remove(passenger);
        }

        public int GetPassengerCount() => passengerEntities.Count();

        public PassengerResponseDto GetPassenger(Guid passengerId)
        {
            var passenger = passengerEntities.FirstOrDefault(x => x.UniquePassengerId == passengerId);
            if (passenger is null) throw new Exception("Passenger not found");
            return passenger.ToDto();
        }

        public List<PassengerResponseDto> GetPassengers()
        {
            return passengerEntities.Select(x => x.ToDto()).ToList();
        }

        public List<PassengerResponseDto> GetCountryPassengers()
        {
            return passengerEntities.Where(x => x.Countries.Any()).Select(x => x.ToDto()).ToList();
        }

        public List<PassengerResponseDto> GetStatusPassengers()
        {
            return passengerEntities.Where(x => x.Statuses.Any()).Select(x => x.ToDto()).ToList();
        }
    }
}
