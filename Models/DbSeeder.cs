namespace LoadsheddingV1.Models
{
    public static class DbSeeder
    {
        public static void Seed(LoadSheddingContext context)
        {
            SeedEvents(context);
            SeedLabs(context);
        }

        private static void SeedEvents(LoadSheddingContext context)
        {
            if (context.LoadSheddingEvents != null && context.LoadSheddingEvents.Any())
                return;

            var now  = DateTime.Now;
            var area = "capetown-15-rondebosch";

            var events = new List<LoadsheddingEvent>
            {
                new LoadsheddingEvent
                {
                    AreaId      = area,
                    StartTime   = now.Date.AddHours(8),
                    EndTime     = now.Date.AddHours(10).AddMinutes(30),
                    LastUpdated = now
                },
                new LoadsheddingEvent
                {
                    AreaId      = area,
                    StartTime   = now.Date.AddHours(14),
                    EndTime     = now.Date.AddHours(16).AddMinutes(30),
                    LastUpdated = now
                },
                new LoadsheddingEvent
                {
                    AreaId      = area,
                    StartTime   = now.Date.AddDays(1).AddHours(6),
                    EndTime     = now.Date.AddDays(1).AddHours(8).AddMinutes(30),
                    LastUpdated = now
                },
                new LoadsheddingEvent
                {
                    AreaId      = area,
                    StartTime   = now.Date.AddDays(1).AddHours(16),
                    EndTime     = now.Date.AddDays(1).AddHours(18).AddMinutes(30),
                    LastUpdated = now
                },
                new LoadsheddingEvent
                {
                    AreaId      = area,
                    StartTime   = now.Date.AddDays(2).AddHours(10),
                    EndTime     = now.Date.AddDays(2).AddHours(12).AddMinutes(30),
                    LastUpdated = now
                },
            };

            context.LoadSheddingEvents!.AddRange(events);
            context.SaveChanges();
        }

        private static void SeedLabs(LoadSheddingContext context)
        {
            if (context.Labs != null && context.Labs.Any())
                return;

            var labs = new List<Labs>
            {
                new Labs { LabName = "Lab 1",  PC = "PC-L1",  OnOff = true  },
                new Labs { LabName = "Lab 2",  PC = "PC-L2",  OnOff = true  },
                new Labs { LabName = "Lab 3",  PC = "PC-L3",  OnOff = false },
                new Labs { LabName = "Lab 4",  PC = "PC-L4",  OnOff = true  },
                new Labs { LabName = "Lab 5",  PC = "PC-L5",  OnOff = false },
                new Labs { LabName = "Lab 6",  PC = "PC-L6",  OnOff = true  },
                new Labs { LabName = "Lab 7",  PC = "PC-L7",  OnOff = true  },
                new Labs { LabName = "Lab 8",  PC = "PC-L8",  OnOff = false },
                new Labs { LabName = "Lab 9",  PC = "PC-L9",  OnOff = true  },
                new Labs { LabName = "Lab 10", PC = "PC-L10", OnOff = true  },
                new Labs { LabName = "Lab 11", PC = "PC-L11", OnOff = false },
                new Labs { LabName = "Lab 12", PC = "PC-L12", OnOff = true  },
            };

            context.Labs!.AddRange(labs);
            context.SaveChanges();
        }
    }
}
