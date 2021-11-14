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
                List<DayPlan> excercises = new();
                Excercise snatch = DatabaseEditor.GetExcercise("Snatch");
                Excercise swing = DatabaseEditor.GetExcercise("Swing");
                excercises.Add(DatabaseEditor.CreateExcerciseForDay(new DateTime(2021, 11, 15), snatch, 6, 6));
                excercises.Add(DatabaseEditor.CreateExcerciseForDay(new DateTime(2021, 11, 15), swing, 6, 50));
                DatabaseEditor.AddDayPlan(excercises);
                List<DayPlan> dayPlan = DatabaseEditor.GetDayExcercises(new DateTime(2021, 11, 15));
                if (dayPlan is not null)
                {
                    foreach (DayPlan e in dayPlan)
                    {
                        WriteLine($"{e.ID} {e.DayOfWeek}, {e.Date}: {e.Excercise.Name} {e.Rounds} rounds for {e.Repeats} repeats ");
                    }
                }
            }
            WriteLine("Program finished");
        }
    }
}
