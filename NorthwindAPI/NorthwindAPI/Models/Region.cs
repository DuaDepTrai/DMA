namespace NorthwindAPI.Models
{
    public class Region
    {
        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
        public ICollection<Territories> Territories { get; set; }
    }
}
