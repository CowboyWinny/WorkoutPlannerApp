using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutPlannerAppConsole
{
    public static class DatabaseEditor
    {
        public static bool AddExcercise(string name,
                                        long tagsForExcerciseID,
                                        long caloriesCost = 0,
                                        string description = null)
        {
            using (var db = new WorkoutPlannerDB())
            {
                var newExcercise = new Excercise
                {
                    ID = Excercise.NewID(),
                    Name = name,
                    TagsForExcercisesID = tagsForExcerciseID,
                    CaloriesCost = caloriesCost,
                    Description = description
                };

                db.Excercises.Add(newExcercise);

                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }
        public static bool AddExcercise(Excercise newExcercise)
        {
            using (var db = new WorkoutPlannerDB())
            {
                db.Excercises.Add(newExcercise);

                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }
        public static bool AddExcercise(string name,
                                        long caloriesCost = 0,
                                        string description = null,
                                        bool arms = false,
                                        bool legs = false,
                                        bool back = false,
                                        bool core = false,
                                        bool isIsolated = false,
                                        bool isBallistic = false)
        {
            using (var db = new WorkoutPlannerDB())
            {
                DatabaseEditor.AddTagsForExcercise(arms, legs, back, core, isIsolated, isBallistic);

                long tagsForExcerciseID = default;
                IQueryable<TagsForExcercise> tags = db.TagsForExcercises.OrderBy(tags => tags.ID);
                if (tags.Any())
                {
                    tagsForExcerciseID = tags.Last().ID;
                }
                else throw new ArgumentNullException();

                return DatabaseEditor.AddExcercise(name, tagsForExcerciseID, caloriesCost, description);
            }
        }
        public static bool AddExcercise(string name,
                                        long caloriesCost = 0,
                                        string description = null)
        {
            using (var db = new WorkoutPlannerDB())
            {
                DatabaseEditor.AddTagsForExcercise();
                long tagsForExcerciseID = default;
                IQueryable<TagsForExcercise> tags = db.TagsForExcercises.OrderBy(tags => tags.ID);
                if (tags.Any())
                {
                    tagsForExcerciseID = tags.Last().ID;
                }
                else throw new ArgumentNullException();

                return DatabaseEditor.AddExcercise(name, tagsForExcerciseID, caloriesCost, description);
            }
        }
        public static bool AddTagsForExcercise( bool arms = false,
                                                bool legs = false,
                                                bool back = false,
                                                bool core = false,
                                                bool isIsolated = false,
                                                bool isBallistic = false)
        {
            using (var db = new WorkoutPlannerDB())
            {
                var newTags = new TagsForExcercise
                {
                    ID = TagsForExcercise.NewID(),
                    Arms = arms,
                    Legs = legs,
                    Back = back,
                    Core = core,
                    IsIsolated = isIsolated,
                    IsBallistic = isBallistic

                };
                
                db.TagsForExcercises.Add(newTags);

                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }
        public static Excercise GetExcercise(string name)
        {
            using (var db = new WorkoutPlannerDB())
            {
                IQueryable<Excercise> excercises = db.Excercises.Include(e => e.TagsForExcercises)
                                                                .Where(e => e.Name == name);
                if (excercises.Count() == 1)
                {
                    return excercises.First();
                }
                else if (excercises.Count() == 0)
                {
                    return null;
                }
                else
                {
                    throw new InvalidOperationException("More than one excercise with that name.");
                }
            }
        }
        public static List<Excercise> GetAllExcercises()
        {
            using (var db = new WorkoutPlannerDB())
            {
                IQueryable<Excercise> excercises = db.Excercises.Include(e => e.TagsForExcercises);
                List<Excercise> allExcercises = new();
                if (excercises.Any())
                {
                    foreach (Excercise excercise in excercises)
                    {
                        allExcercises.Add(excercise);
                    }
                }
                return allExcercises;
            }
        }
        public static List<Excercise> GetExcercisesWithTag(ExcerciseTags tag)
        {
            using (var db = new WorkoutPlannerDB())
            {
                IQueryable<Excercise> excercises;
                List<Excercise> excercisesWithTag = new();

                switch(tag)
                {
                    case ExcerciseTags.Arms:
                        excercises = db.Excercises.Include(e => e.TagsForExcercises)
                                                                .Where(e => e.TagsForExcercises.Arms == true);
                        break;
                    case ExcerciseTags.Legs:
                        excercises = db.Excercises.Include(e => e.TagsForExcercises)
                                                                .Where(e => e.TagsForExcercises.Legs == true);
                        break;
                    case ExcerciseTags.Back:
                        excercises = db.Excercises.Include(e => e.TagsForExcercises)
                                                                .Where(e => e.TagsForExcercises.Back == true);
                        break;
                    case ExcerciseTags.Core:
                        excercises = db.Excercises.Include(e => e.TagsForExcercises)
                                                                .Where(e => e.TagsForExcercises.Core == true);
                        break;
                    case ExcerciseTags.Isolated:
                        excercises = db.Excercises.Include(e => e.TagsForExcercises)
                                                                .Where(e => e.TagsForExcercises.IsIsolated == true);
                        break;
                    case ExcerciseTags.Ballistic:
                        excercises = db.Excercises.Include(e => e.TagsForExcercises)
                                                                .Where(e => e.TagsForExcercises.IsBallistic == true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("There is no such tag.");
                };

                if (excercises.Any())
                {
                    foreach(Excercise excercise in excercises)
                    {
                        excercisesWithTag.Add(excercise);
                    }
                }
                return excercisesWithTag;
            }
        }
        public static List<Excercise> GetExcercisesWithTags(List<ExcerciseTags> tags)
        {
            using (var db = new WorkoutPlannerDB())
            {
                IQueryable<Excercise> excercises = db.Excercises.Include(e => e.TagsForExcercises);
                List<Excercise> excercisesWithTags = new();

                if(tags.Contains(ExcerciseTags.Arms))
                {
                    excercises = excercises.Where(e => e.TagsForExcercises.Arms == true);
                }
                if (tags.Contains(ExcerciseTags.Legs))
                {
                    excercises = excercises.Where(e => e.TagsForExcercises.Legs == true);
                }
                if (tags.Contains(ExcerciseTags.Back))
                {
                    excercises = excercises.Where(e => e.TagsForExcercises.Back == true);
                }
                if (tags.Contains(ExcerciseTags.Core))
                {
                    excercises = excercises.Where(e => e.TagsForExcercises.Core == true);
                }
                if (tags.Contains(ExcerciseTags.Isolated))
                {
                    excercises = excercises.Where(e => e.TagsForExcercises.IsIsolated == true);
                }
                if (tags.Contains(ExcerciseTags.Ballistic))
                {
                    excercises = excercises.Where(e => e.TagsForExcercises.IsBallistic == true);
                }

                if (excercises.Any())
                {
                    foreach (Excercise excercise in excercises)
                    {
                        excercisesWithTags.Add(excercise);
                    }
                }
                return excercisesWithTags;
            }
        }
        public static List<ExcerciseTags> GetExcerciseTags(Excercise excercise)
        {
            List<ExcerciseTags> tags = new();

            if(excercise.TagsForExcercises.Arms)
            {
                tags.Add(ExcerciseTags.Arms);
            }
            if (excercise.TagsForExcercises.Legs)
            {
                tags.Add(ExcerciseTags.Legs);
            }
            if (excercise.TagsForExcercises.Back)
            {
                tags.Add(ExcerciseTags.Back);
            }
            if (excercise.TagsForExcercises.Core)
            {
                tags.Add(ExcerciseTags.Core);
            }
            if (excercise.TagsForExcercises.IsIsolated)
            {
                tags.Add(ExcerciseTags.Isolated);
            }
            if (excercise.TagsForExcercises.IsBallistic)
            {
                tags.Add(ExcerciseTags.Ballistic);
            }

            return tags;
        }
        public static bool AddDayPlan(List <DayPlan> excercisesForDay)
        {
            using (var db = new WorkoutPlannerDB())
            {
                foreach(DayPlan excerciseForDay in excercisesForDay)
                {
                    db.DayPlans.Add(excerciseForDay);
                }

                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }
        public static bool AddExcerciseForDay(  DateTime dateForTraining, 
                                                Excercise excercise,
                                                long rounds = 1,
                                                long repeats = 1,
                                                bool isSingleDay = true)
        {
            using (var db = new WorkoutPlannerDB())
            {
                var newDayPlan = DatabaseEditor.CreateExcerciseForDay(  dateForTraining, 
                                                                        excercise, 
                                                                        rounds,
                                                                        repeats,
                                                                        isSingleDay);

                db.DayPlans.Add(newDayPlan);

                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }
        public static DayPlan CreateExcerciseForDay(DateTime dateForTraining, 
                                                    Excercise excercise,
                                                    long rounds = 1,
                                                    long repeats = 1,
                                                    bool isSingleDay = true)
        {
            var newDayPlan = new DayPlan
            {
                ID = DayPlan.NewID(),                    DayOfWeek = dateForTraining.DayOfWeek.ToString(),
                Date = dateForTraining.Date.ToString(),
                ExcerciseID = excercise.ID,
                Rounds = rounds,
                Repeats = repeats,
                IsSingleDay = isSingleDay
            };
            return newDayPlan;
        }
        public static List<DayPlan> GetDayExcercises(DateTime trainingDate)
        {
            using (var db = new WorkoutPlannerDB())
            {
                IQueryable<DayPlan> day = db.DayPlans.Include(e => e.Excercise)
                                                                .Where(e => e.Date == trainingDate.Date.ToString());
                List<DayPlan> dayExcercises = new();
                if (day.Any())
                {
                    foreach(DayPlan dayExcercise in day)
                    {
                        dayExcercises.Add(dayExcercise);
                    }
                }
                return dayExcercises;
            }
        }

    }
}
