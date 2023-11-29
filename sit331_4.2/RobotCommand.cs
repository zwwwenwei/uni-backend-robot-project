namespace sit331_4._1
{
    public class RobotCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMoveCommand { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? Description { get; set; }
        public RobotCommand(
            int id, string name, bool isMoveCommand, DateTime createDate, DateTime modifiedDate, string? description = null)
        {
            Id = id;
            Name = name;
            IsMoveCommand = isMoveCommand;
            CreateDate = createDate;
            ModifiedDate = modifiedDate;
            // Initialize every property with parameters.
        }


    }
}
