using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Security.Cryptography;

namespace WorkoutPlannerAppConsole
{
    public partial class DayPlan
    {
        public static long NewID()
        {
            using (var db = new WorkoutPlannerDB())
            {
                long newID = default;
                using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider()) 
                { 
                    byte[] rno = new byte[8];    
                    rg.GetBytes(rno);    
                    newID = BitConverter.ToInt64(rno, 0);
                    if(newID < 0)
                    {
                        newID *= -1;
                    }
                }

                IQueryable<DayPlan> dayPlan = db.DayPlans.Where(e => e.ID == newID);
                if (dayPlan.Any())
                {
                    return DayPlan.NewID();
                }
                else return newID;
            }
        }
    }
}
