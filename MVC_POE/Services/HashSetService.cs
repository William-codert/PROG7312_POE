using MVC_POE.Models;

namespace MVC_POE.Services
{
    public class HashSetService
    {
        private static readonly HashSet<ReportIssuesForm> _reportIssuesForms = new HashSet<ReportIssuesForm>();

        public IEnumerable<ReportIssuesForm> GetAllForms() => _reportIssuesForms;

        public void AddForm(ReportIssuesForm reportIssuesForm)
        {
            _reportIssuesForms.Add(reportIssuesForm);
        }
        //this method adds forms into the hashset upon program start up
        public void AddForms(ReportIssuesForm reportIssuesForm)
        {
            _reportIssuesForms.Add(reportIssuesForm);
        }
        // Seed initial forms
        public void SeedForms()
        {
            _reportIssuesForms.Add(new ReportIssuesForm
            {
                Location = "Walmer",
                Category = "Roads",
                Description = "Potholes near the traffic light just after Circular drive.",
                MediaAttachment = "~/uploads/broken road.jpg"
            });

            _reportIssuesForms.Add(new ReportIssuesForm
            {
                Location = "Westering",
                Category = "Roads",
                Description = "Robot is on the ground, utterly damaged.",
                MediaAttachment = "~/uploads/brokenraod2.jpg"
            });

            _reportIssuesForms.Add(new ReportIssuesForm
            {
                Location = "Mill Park",
                Category = "Sanitisation",
                Description = "Trash bins not collected for 3 days and rubbish is flying around in streets.",
                MediaAttachment = "~/uploads/rubbish2.jpg"
            });

            _reportIssuesForms.Add(new ReportIssuesForm
            {
                Location = "Westering",
                Category = "Roads",
                Description = "Robot is destroyed and laying on the ground.",
                MediaAttachment = "~/uploads/burstpipe.jpg"
            });
        }

    }

    
}
