namespace BlueHarvest.Shared.DTOs.Cosmic;

public class CreateStarClusterDto
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public EllipsoidDto? ClusterSize { get; set; }
   public MinMaxDto<double>? DistanceBetweenSystems { get; set; }
   public int? PlanetarySystemsCountExact { get; set; } 
   public MinMaxDto<int>? PlanetarySystemsCountRange { get; set; } 
   public int? DeepSpaceObjectsCountExact { get; set; } 
   public MinMaxDto<int>? DeepSpaceObjectsCountRange { get; set; } 

   public MinMaxDto<double>? PlanetarySystemSizeRange { get; set; } 

// public PlanetarySystemFactoryOptions? PlanetarySystemOptions { get; set; } = new();
}

//public MinMax<double>? SystemRadius { get; set; } = new(10.0, 100.0);
