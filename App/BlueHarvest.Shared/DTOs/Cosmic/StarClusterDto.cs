namespace BlueHarvest.Shared.DTOs.Cosmic;

public class StarClusterDto
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public DateTime? CreatedOn { get; set; }
   public EllipsoidDto? Size { get; set; }
}
