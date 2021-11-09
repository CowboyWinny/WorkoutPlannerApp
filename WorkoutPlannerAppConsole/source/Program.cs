using System;
using System.Collections.Generic;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WorkoutPlannerAppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Program started");
            using (var db = new WorkoutPlannerDB())
            {
                List<Excercise> excercises = DatabaseEditor.GetExcercisesWithTag(ExcerciseTags.Core);
                if (excercises is not null)
                {
                    foreach (Excercise e in excercises)
                    {
                        WriteLine($"{e.ID} excercise has name {e.Name} and core is {e.TagsForExcercises.Core}");
                        WriteLine($"{e.Description}");
                    }
                }
            }
            WriteLine("Program finished");
        }
    }
}
