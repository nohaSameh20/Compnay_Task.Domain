using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task_Company.DataAccess;
using Task_Company.Models;

namespace Task_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumptionController : ControllerBase
    {
        ApplicationDbContext dbContext;
        public ConsumptionController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("TotalConSumption")]
        public double TotalConSumption()
        {

            var consumptionsCount = dbContext.Consumptions.Count();
            if (consumptionsCount == 0)
                GetConsumptions("Task_Data.xlsx");

            var totalSumption = dbContext.Consumptions.Select(ob=>ob.Voltage * ob.Current).Sum();
            return totalSumption;
        }


        [HttpGet("AvgConSumption")]
        public double AvgConSumption()
        {
            var avgConSumption = dbContext.Consumptions.Select(ob => ob.Voltage * ob.Current).Average();
            return avgConSumption;
        }

        [HttpGet("MaxConSumption")]
        public double MaxConSumption()
        {
            var maxConSumption = dbContext.Consumptions.Select(ob => ob.Voltage * ob.Current).Max();
            return maxConSumption;
        }


        [HttpGet("MinConSumption")]
        public double MinConSumption()
        {
            var minConSumption = dbContext.Consumptions.Select(ob => ob.Voltage * ob.Current).Min();
            return minConSumption;
        }


        [HttpGet("MaxVoltage")]
        public double MaxVoltage()
        {
            var maxVoltage = dbContext.Consumptions.Max(obj => obj.Voltage);
            return maxVoltage;
        }

        [HttpGet("AvgVoltage")]
        public double AvgVoltage()
        {
            var maxVoltage = dbContext.Consumptions.Average(obj => obj.Voltage);
            return maxVoltage;
        }



        [HttpGet("MaxCurrent")]
        public double MaxCurrent()
        {
            var MaxCurrent = dbContext.Consumptions.Max(obj => obj.Current);
            return MaxCurrent;
        }

        [HttpGet("AvgCurrent")]
        public double AvgCurrent()
        {
            var AvgCurrent = dbContext.Consumptions.Average(obj => obj.Current);
            return AvgCurrent;
        }



        private List<ConsumptionModel> GetConsumptions(string fileName)
        {
            List<ConsumptionModel> consumptionsList = new List<ConsumptionModel>();
            List<Consumption> consumptions = new List<Consumption>();

            var path = $"{ Directory.GetCurrentDirectory()}{@"\Files"}" + "\\" + fileName;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            byte[] bytes = System.IO.File.ReadAllBytes(path);
            String AsBase64String = Convert.ToBase64String(bytes);
            byte[] tempBytes = Convert.FromBase64String(AsBase64String);
            using (MemoryStream stream = new MemoryStream(tempBytes))
            {
                stream.Position = 1;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    if (reader.RowCount < 2)
                    {
                        throw new Exception("EmptyFile");
                    }
                    if (reader.FieldCount != 6)
                    {
                        throw new Exception("FilePatternNotValid");

                    }
                    while (reader.Read())
                    {
                        consumptionsList.Add(new ConsumptionModel()
                        {
                            Voltage = reader.GetValue(0)?.ToString(),
                            Current = reader.GetValue(1)?.ToString(),
                            TimeStamp = reader.GetValue(2)?.ToString(),
                            Id=Guid.NewGuid()
                        });
                     
                    }
                    consumptionsList = consumptionsList.Skip(1).ToList();

                    foreach (var consumptionModel in consumptionsList)
                    {
                        consumptions.Add(new Consumption()
                        {
                            Voltage = double.Parse(consumptionModel.Voltage),
                            TimeStamp = DateTime.Parse(consumptionModel.TimeStamp),
                            Current = double.Parse(consumptionModel.Current),
                            Id = consumptionModel.Id
                        });
                        
                    }

                    dbContext.AddRange(consumptions);
                    dbContext.SaveChanges();
                }
                return consumptionsList;
            }

        }
    }
}
