using System;
using System.Collections.Generic;

#nullable disable

namespace WorkoutPlannerAppConsole
{
    public partial class Excercise
    {
        public Excercise()
        {
            DayPlans = new HashSet<DayPlan>();
        }

        public long ID { get; set; }
        public string Name { get; set; }
        public long CaloriesCost { get; set; }
        public string Description { get; set; }
        public long TagsForExcercisesID { get; set; }

        public virtual TagsForExcercise TagsForExcercises { get; set; }
        public virtual ICollection<DayPlan> DayPlans { get; set; }
    }
}
