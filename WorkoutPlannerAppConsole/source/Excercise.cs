using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlannerAppConsole
{
    public partial class Excercise
    {
        public static long NewID()
        {
            using (var db = new WorkoutPlannerDB())
            {
                long newID = 0;
                IQueryable<Excercise> excercise = db.Excercises.OrderBy(excercise => excercise.ID);
                if (excercise.Any())
                {
                    newID = excercise.Last().ID + 1;
                    return newID;
                }
                else return 1;
            }
        }
    }
}
