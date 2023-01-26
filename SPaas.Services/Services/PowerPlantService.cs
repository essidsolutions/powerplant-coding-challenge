using System.Collections.Generic;
using SPaas.Services.DataModels;
using SPaas.Services.Extensions;
using SPaas.Services.Interfaces;

namespace SPaas.Services.Services
{
    public class PowerPlantService: IPowerPlantService
    {
        public IEnumerable<PowerOutput> GetProductionPlan(IEnumerable<PowerPlant> powerPlants, Fuel fuel, decimal load)
        { 
            return powerPlants
                .CalculateCostAndActualPower(fuel)
                .OrderPowerPlants()
                .ReportPlanLoad(load);
        }
    }
}