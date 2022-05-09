using CorujasDev.ReactGoogleCharts.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorujasDev.ReactGoogleCharts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
       
        private readonly ILogger<ChartsController> _logger;

        public ChartsController(ILogger<ChartsController> logger)
        {
             var rand = new Random();
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<GetChartUserRegisterbyMonthResponse> GetChartUserRegisterbyMonth()
        {
            var rand = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new GetChartUserRegisterbyMonthResponse
            {
                Mes = DateTime.Now.AddMonths(index).ToString("MMMM"),
                Biker = rand.Next(1, 10),
                Empresa = rand.Next(1, 10),
            })
            .ToArray();

            foreach (var item in result)
                item.Total = item.Biker + item.Empresa;

            return result;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<GetCountPercentageCatgoriesCompanyResponse> GetCountPercentageCatgoriesCompany()
        {
            string[] Empresas = new[] { "Itens Restantes", "Itens Concluídos" };
            var rand = new Random();

            var result = Empresas.Select(index => new GetCountPercentageCatgoriesCompanyResponse
            {
                Nome = index,
                Quantidade = rand.Next(0, 50)
            })
            .ToArray();

            var total = result.Sum(x => x.Quantidade);                                                                                                                                                                                                                                  

            foreach (var item in result)
                item.Porcentagem = (((double)item.Quantidade / total) * 100);

            return result;
        }
    }
}
