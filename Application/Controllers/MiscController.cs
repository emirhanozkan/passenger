using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Model.Enum;
using System.IO;
using Core.Model.Dto.Request;
using Core.Model.Dto.Response;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MiscController : ControllerBase
    {
        private readonly ILogger<MiscController> _logger;
        public MiscController(ILogger<MiscController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("enums")]
        public List<KeyValuePair<string, List<EnumResponse>>> DocumentTypes([FromBody] MiscRequestDto dto)
        {
            var result = new List<KeyValuePair<string, List<EnumResponse>>>();
            var assemblies = GetSolutionAssemblies().Where(x => x.FullName.StartsWith("Core") && x.GetTypes().Any(y => y.IsEnum && y.IsPublic));
            foreach (var name in dto.Names)
            {
                var enumType = assemblies.SelectMany(x => x.GetTypes()).Where(x => x.Name == name).SingleOrDefault();
                if (enumType == null) continue;
                var values = enumType.GetEnumValues().Cast<Enum>();
                result.Add(new KeyValuePair<string, List<EnumResponse>>(name, values.Select(x => new EnumResponse { Id = Convert.ToInt32(x), Name = x.ToString() }).ToList()));
            }
            return result;
        }

        private static Assembly[] GetSolutionAssemblies() =>
            Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x))).ToArray();
    }
}
