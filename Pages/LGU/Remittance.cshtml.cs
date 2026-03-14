using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace CTBCServiceStatusDashboard.Pages.LGU
{
    [Authorize(Roles = "LGU, User Admin")]
    public class RemittanceModel : PageModel
    {
        private readonly ILogger<RemittanceModel> _logger;

        public RemittanceModel(ILogger<RemittanceModel> logger)
        {
            _logger = logger;
        }

        public List<Remittance> Remittances { get; set; }
        public decimal TotalRemittances { get; set; }
        public decimal AverageMonthly { get; set; }

        public class Remittance
        {
            public int Id { get; set; }
            public string Date { get; set; }
            public string Source { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public string Status { get; set; }
            public string Reference { get; set; }
        }

        public void OnGet()
        {
            Remittances = new List<Remittance>
            {
                new Remittance
                {
                    Id = 1,
                    Date = "2024-03-10",
                    Source = "Internal Revenue Allotment (IRA)",
                    Description = "Monthly IRA for March 2024",
                    Amount = 245000,
                    Status = "Received",
                    Reference = "IRA-2024-03-001"
                },
                new Remittance
                {
                    Id = 2,
                    Date = "2024-02-10",
                    Source = "Internal Revenue Allotment (IRA)",
                    Description = "Monthly IRA for February 2024",
                    Amount = 245000,
                    Status = "Received",
                    Reference = "IRA-2024-02-001"
                },
                new Remittance
                {
                    Id = 3,
                    Date = "2024-01-10",
                    Source = "Internal Revenue Allotment (IRA)",
                    Description = "Monthly IRA for January 2024",
                    Amount = 245000,
                    Status = "Received",
                    Reference = "IRA-2024-01-001"
                },
                new Remittance
                {
                    Id = 4,
                    Date = "2023-12-15",
                    Source = "Special Project Fund",
                    Description = "Road construction project funding",
                    Amount = 510000,
                    Status = "Received",
                    Reference = "SPF-2023-12-045"
                },
                new Remittance
                {
                    Id = 5,
                    Date = "2023-12-10",
                    Source = "Internal Revenue Allotment (IRA)",
                    Description = "Monthly IRA for December 2023",
                    Amount = 245000,
                    Status = "Received",
                    Reference = "IRA-2023-12-001"
                },
                new Remittance
                {
                    Id = 6,
                    Date = "2023-11-10",
                    Source = "Internal Revenue Allotment (IRA)",
                    Description = "Monthly IRA for November 2023",
                    Amount = 245000,
                    Status = "Received",
                    Reference = "IRA-2023-11-001"
                }
            };

            TotalRemittances = 0;
            foreach (var r in Remittances)
            {
                TotalRemittances += r.Amount;
            }
            AverageMonthly = TotalRemittances / 6;
        }
    }
}