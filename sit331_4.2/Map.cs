namespace sit331_4._1
{
    public class Map
    {
        public int Id { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Map(int id, int columns, int rows, string name, DateTime createddate, DateTime modifieddate, string? description = null)
        {
            Id = id;
            Columns = columns;
            Rows = rows;
            Name = name;
            CreatedDate = createddate;
            ModifiedDate = modifieddate;
            Description = description;
        }
    }
}
