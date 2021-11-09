using System;
using System.Collections.Generic;

#nullable disable

namespace WorkoutPlannerAppConsole
{
    public partial class TagsForExcercise
    {
        public TagsForExcercise()
        {
            Excercises = new HashSet<Excercise>();
        }

        public long ID { get; set; }
        public bool Arms { get; set; }
        public bool Legs { get; set; }
        public bool Back { get; set; }
        public bool Core { get; set; }
        public bool IsIsolated { get; set; }
        public bool IsBallistic { get; set; }

        public virtual ICollection<Excercise> Excercises { get; set; }
    }
}
