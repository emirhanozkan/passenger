using Application.Services;
using Core.Model.Dto.Request;
using Core.Model.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengerController : ControllerBase
    {
        private readonly ILogger<PassengerController> _logger;
        private readonly IPassengerService _passengerService;
        public PassengerController(ILogger<PassengerController> logger, IPassengerService passengerService)
        {
            _logger = logger;
            _passengerService = passengerService;
        }

        [HttpPost]
        public void InsertPassenger([FromBody] PassengerRequestDto dto)
        {
            _passengerService.InsertPassenger(dto);
        }

        [HttpPut, Route("{passengerId}")]
        public void UpdatePassenger([FromRoute] Guid passengerId, [FromBody] PassengerRequestDto dto)
        {
            _passengerService.UpdatePassenger(passengerId, dto);
        }

        [HttpGet, Route("{passengerId}")]
        public PassengerResponseDto GetPassenger([FromRoute] Guid passengerId)
        {
            return _passengerService.GetPassenger(passengerId);
        }

        [HttpDelete, Route("{passengerId}")]
        public void DeletePassenger([FromRoute] Guid passengerId)
        {
            _passengerService.DeletePassenger(passengerId);
        }

        [HttpGet, Route("count")]
        public int GetPassengerCount()
        {
            return _passengerService.GetPassengerCount();
        }

        [HttpGet, Route("country")]
        public List<PassengerResponseDto> GetCountry()
        {
            return _passengerService.GetCountryPassengers();
        }
        [HttpGet, Route("status")]
        public List<PassengerResponseDto> GetStatus()
        {
            return _passengerService.GetStatusPassengers();
        }
    }
}
