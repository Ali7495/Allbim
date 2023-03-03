namespace Models.City
{
    public class ProvinceResultViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
    public class ProvinceInputViewModel
    {

        public string Name { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
}
