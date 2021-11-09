using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlannerAppConsole
{
    public partial class TagsForExcercise
    {
        public static long NewID()
        {
            using (var db = new WorkoutPlannerDB())
            {
                long newID = 0;
                IQueryable<TagsForExcercise> excerciseTags = db.TagsForExcercises.OrderBy(tag => tag.ID);
                if(excerciseTags.Any())
                {
                    newID = excerciseTags.Last().ID + 1;
                    return newID;
                }
                else return 1;
            }
        }
    }

    public enum ExcerciseTags
    {
        Arms,
        Legs,
        Back,
        Core,
        Isolated,
        Ballistic
    }
}
