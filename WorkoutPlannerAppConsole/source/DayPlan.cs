using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlannerAppConsole
{
    public partial class DayPlan
    {
        public static long NewID()
        {
            using (var db = new WorkoutPlannerDB())
            {
                long newID = 0;
                IQueryable<DayPlan> dayPlan = db.DayPlans.OrderBy(dayPlan => dayPlan.ID);
                if (dayPlan.Any())
                {
                    newID = dayPlan.Last().ID + 1;
                    return newID;
                }
                else return 1;
            }
        }
    }
}
