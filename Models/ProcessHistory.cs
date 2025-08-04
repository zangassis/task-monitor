namespace TaskMonitor.Models;

public class ProcessHistory
{
    public int Id { get; set; }                
    public string ProcessName { get; set; }     
    public DateTime CreatedAt { get; set; }     
    public string Status { get; set; }           
    public string Details { get; set; }          
    public string CreatedBy { get; set; }
}
