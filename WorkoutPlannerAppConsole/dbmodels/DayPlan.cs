using System;
using System.Collections.Generic;

#nullable disable

namespace WorkoutPlannerAppConsole
{
    public partial class DayPlan
    {
        public long ID { get; set; }
        public string DayOfWeek { get; set; }
        public string Date { get; set; }
        public long ExcerciseID { get; set; }
        public long Rounds { get; set; }
        public long Repeats { get; set; }
        public bool IsSingleDay {get; set; }

        public virtual Excercise Excercise { get; set; }
    }
}
