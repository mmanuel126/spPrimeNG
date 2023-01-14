namespace sportprofiles.Models.Common
{
    public class LogModel
    {
        public string Message { get; set; } = string.Empty;
        public string Stack { get; set; } = string.Empty;
        public int Level { get; set; }
        public DateTime Timestamp { get; set; } 
        public string FileName { get; set; } = string.Empty;
        public Int32 FileNumber { get; set; }
    }
}
