using System;
using System.Collections.Generic;
using System.Linq;
using SPaas.Services.DataModels;

namespace SPaas.Services.Extensions
{
    public static class PowerPlantExtension
    {
        
        public static IEnumerable<PowerPlant> CalculateCostAndActualPower( this IEnumerable<PowerPlant> powerPlants,
            Fuel fuel)
        {
            IList<PowerPlant> plants = new List<PowerPlant>();
            foreach (var plant in powerPlants)
            {
                plant.ActualPMax = GetActualPMax(plant, fuel);
                plant.FuelCost = GetFuelCost(plant, fuel);
                plants.Add(plant);
            }
            return plants;
        }

        private static decimal GetFuelCost( this PowerPlant powerPlant, Fuel fuel)
        {
            return powerPlant.Type switch
            {
                PowerPlantType.WindTurbine => 0.0M,
                PowerPlantType.GasFired => fuel.Gas / powerPlant.Efficiency,
                _ => fuel.Kerosine / powerPlant.Efficiency
            };
        }
        

        private static decimal GetActualPMax(this PowerPlant powerPlant, Fuel fuel)
        {
            if (powerPlant.Type != SPaas.Services.DataModels.PowerPlantType.WindTurbine)
                return powerPlant.PMax;
            return powerPlant.PMax / 100.0M * fuel.Wind;
        }
        
        public static IEnumerable<PowerPlant> OrderPowerPlants (this IEnumerable<PowerPlant> powerPlants)
        {
            return powerPlants.OrderByDescending(i => i.Efficiency)
                .ThenBy(i => i.FuelCost)
                .ThenByDescending(i => i.ActualPMax);
        }
        
        public static IEnumerable<PowerOutput> ReportPlanLoad(this IEnumerable<PowerPlant> powerPlants, decimal planLoad)
         {
             var ret = new List<PowerOutput>();
             var load = planLoad;
             powerPlants
                 .ToList()
                 .ForEach(p => {
                     if (p.ActualPMax == 0 || p.PMin>load)
                     {
                         ret.Add(new PowerOutput()
                         {Name = p.Name, P = 0,
                                });
                         return;
                     }
                     if (load <= p.ActualPMax && load>= p.PMin)
                     {
                         ret.Add(new PowerOutput()
                         {
                             Name = p.Name, 
                             P = Math.Round(load),
                         });
                         load = 0;
                         return;
                     }
                     ret.Add(new PowerOutput()
                     {
                         Name = p.Name,
                         P = Math.Round(p.ActualPMax),
                     });
                     load -= p.ActualPMax;
                 });
             return ret;
         }
    }
}