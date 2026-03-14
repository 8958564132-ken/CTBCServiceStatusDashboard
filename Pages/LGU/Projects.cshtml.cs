using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace CTBCServiceStatusDashboard.Pages.LGU
{
    [Authorize(Roles = "LGU")]
    public class ProjectsModel : PageModel
    {
        private readonly ILogger<ProjectsModel> _logger;

        public ProjectsModel(ILogger<ProjectsModel> logger)
        {
            _logger = logger;
        }

        public List<LGUProject> Projects { get; set; }

        public class LGUProject
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Location { get; set; }
            public string Status { get; set; }
            public int Progress { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public decimal Budget { get; set; }
            public string Contractor { get; set; }
        }

        public void OnGet()
        {
            Projects = new List<LGUProject>
            {
                new LGUProject
                {
                    Id = 1,
                    Name = "Road Construction - Phase 2",
                    Location = "Barangay San Jose",
                    Status = "Active",
                    Progress = 75,
                    StartDate = "2024-01-15",
                    EndDate = "2024-06-30",
                    Budget = 2500000,
                    Contractor = "ABC Construction Corp"
                },
                new LGUProject
                {
                    Id = 2,
                    Name = "School Building Construction",
                    Location = "Barangay Santo Niño",
                    Status = "Active",
                    Progress = 30,
                    StartDate = "2024-03-01",
                    EndDate = "2024-12-15",
                    Budget = 5000000,
                    Contractor = "BuildRight Inc."
                },
                new LGUProject
                {
                    Id = 3,
                    Name = "Public Market Renovation",
                    Location = "Barangay Poblacion",
                    Status = "Active",
                    Progress = 60,
                    StartDate = "2024-02-10",
                    EndDate = "2024-08-20",
                    Budget = 1800000,
                    Contractor = "MegaBuilders Co."
                },
                new LGUProject
                {
                    Id = 4,
                    Name = "Barangay Health Center",
                    Location = "Barangay San Isidro",
                    Status = "Planning",
                    Progress = 0,
                    StartDate = "2024-05-01",
                    EndDate = "2024-11-30",
                    Budget = 3200000,
                    Contractor = "TBD"
                },
                new LGUProject
                {
                    Id = 5,
                    Name = "Water System Upgrade",
                    Location = "Barangay San Roque",
                    Status = "Completed",
                    Progress = 100,
                    StartDate = "2023-10-01",
                    EndDate = "2024-02-28",
                    Budget = 1500000,
                    Contractor = "WaterWorks Inc."
                }
            };
        }
    }
}