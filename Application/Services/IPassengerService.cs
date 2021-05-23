using Core.Model.Dto.Request;
using Core.Model.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPassengerService
    {
        Guid InsertPassenger(PassengerRequestDto dto);
        Guid UpdatePassenger(Guid passengerId, PassengerRequestDto dto);
        void DeletePassenger(Guid passengerId);
        int GetPassengerCount();
        PassengerResponseDto GetPassenger(Guid passengerId);
        List<PassengerResponseDto> GetPassengers();
        List<PassengerResponseDto> GetCountryPassengers();
        List<PassengerResponseDto> GetStatusPassengers();
    }
}
