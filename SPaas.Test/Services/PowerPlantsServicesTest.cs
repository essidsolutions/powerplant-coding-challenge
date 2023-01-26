using System.Collections.Generic;
using NUnit.Framework;
using SPaas.Services.DataModels;
using SPaas.Services.Services;

namespace SPaas.Test.Services
{
    public class PowerPlantsServicesTest
    {
        private PowerPlantService _powerPlantService;
        [SetUp]
        public void SetUp()
        {
            _powerPlantService = new PowerPlantService();
        }
        
        [Test]
        public void Calculating_Power_Output()
        {
            //Arrange
             var powerPlants = new List<PowerPlant>() 
             {
                new ()
                {
                    Name = "windpark1",
                    Type = PowerPlantType.WindTurbine,
                    Efficiency = 1,
                    PMin = 0,
                    PMax = 150,
                    FuelCost = 0,
                    ActualPMax = 90
                },
                new()
                {
                    Name = "windpark2",
                    Type = PowerPlantType.WindTurbine,
                    Efficiency = 1,
                    PMin = 0,
                    PMax = 36,
                    FuelCost = 0,
                    ActualPMax = 21.6M,
                },
                new ()
                {
                    Name = "gasfiredbig1",
                    Type = PowerPlantType.GasFired,
                    Efficiency = 0.53M,
                    PMin = 100,
                    PMax = 460,
                    FuelCost = 25.28301886792453M,
                    ActualPMax = 460,
                },
                new ()
                {
                    Name = "gasfiredbig2",
                    Type = PowerPlantType.GasFired,
                    Efficiency = 0.53M,
                    PMin = 100,
                    PMax = 460,
                    FuelCost = 25.28301886792453M,
                    ActualPMax = 460,
                },
                new ()
                {
                    Name = "gasfiredsomewhatsmaller",
                    Type = PowerPlantType.GasFired,
                    Efficiency = 0.37M,
                    PMin = 200,
                    PMax = 2100,
                    FuelCost = 36.21621621621622M,
                    ActualPMax = 210,
                },
                new ()
                {
                    Name = "tj1",
                    Type = PowerPlantType.TurboJet,
                    Efficiency = 0.3M,
                    PMin = 0,
                    PMax = 16,
                    FuelCost = 169.33333333333334M,
                    ActualPMax = 16,
                },
            };
             var fuel = new Fuel()
             {
                 Co2 = 20,
                 Gas = 13.4M,
                 Kerosine = 50.8M,
                 Wind = 60,
             };
             const decimal load = 480;
             var powerOutput = new List<PowerOutput>
             {
                 new ()
                 {
                     Name="windpark1",
                     P=90
                 },
                 new ()
                 {
                     Name="windpark2",
                     P=22
                 },
                 new ()
                 {
                     Name="gasfiredbig1",
                     P=368
                 },
                 new ()
                 {
                     Name="gasfiredbig2",
                     P=0
                 },
                 new ()
                 {
                     Name="gasfiredsomewhatsmaller",
                     P=0
                 },
                 new ()
                 {
                     Name="tj1",
                     P=0
                 },
             };
            //Act
            var result = _powerPlantService.GetProductionPlan(powerPlants, fuel, load);
            //Assert
            Assert.AreEqual( powerOutput,result);
            
        }
    }
}