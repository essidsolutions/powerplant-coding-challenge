using System.Collections.Generic;
using SPaas.Services.DataModels;

namespace SPaas.Services.Interfaces
{
    public interface IPowerPlantService
    {
        public IEnumerable<PowerOutput> GetProductionPlan(IEnumerable<PowerPlant> powerPlants, Fuel fuel, decimal load);
    }
}