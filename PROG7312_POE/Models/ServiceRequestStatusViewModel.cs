namespace PROG7312_POE.Models
{
    public class ServiceRequestStatusViewModel
    {
        public List<ServiceRequest> Requests { get; set; } = new();
        public ServiceRequest? FoundRequest { get; set; }
        public List<string> OrderedRefs { get; set; } = new();
        public List<int> HeapPriorities { get; set; } = new();
        public List<string> GraphPath { get; set; } = new();
    }
}
