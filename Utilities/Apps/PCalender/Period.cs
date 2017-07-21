using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCalender
{
    public class Period
    {
        // represents the date and if its the start of the period
        public Dictionary<DateTime, bool> AllPeriodDates { get; private set; }
        public List<DateTime> AllFertilityDates { get; private set; }
        public List<DateTime> AllOvulationDates { get; private set; }

        public const int DAYS_IN_YEAR = 365; // don't care about leap
        public const int YEARS_BACK = 1;
        public const int YEARS_FORWARD = 2;
        private const int MAX_DAYS = DAYS_IN_YEAR * YEARS_FORWARD;

        // don't know where this comes from but i found on the webz :)
        private const int FERTILITY_DURATION = 6;
        private const int FERTILITY_BEFORE_OVULATION = 3;
        private const int OVULATION_DIVISION_IN_DURATION = 3;

        public DateTime StartDate { get; private set; }
        public int Duration { get; private set; }
        public int CycleLength { get; private set; }

        public Period(DateTime Start, int Dur, int Cyc)
        {
            AllPeriodDates = new Dictionary<DateTime, bool>();
            AllFertilityDates = new List<DateTime>();
            AllOvulationDates = new List<DateTime>();

            StartDate = Start;
            Duration = Dur;
            CycleLength = Cyc;

            CalculateAllDates();
        }

        private void CalculateAllDates()
        {
            DateTime date = StartDate;

            // while we're within maxDays days of our original start date, add a new date time 
            // object to our list as a valid date   
            // also make sure we go out 2 years from todays date, INCLUDING whatever was in the past
            while ((date - StartDate).Days <= (MAX_DAYS + (Math.Abs((DateTime.Today - StartDate).Days))))
            {
                DateTime pDate = date;

                // add all period days to the list
                for (int i = 0; i < Duration; i++)
                {
                    AllPeriodDates.Add(new DateTime(pDate.Year, pDate.Month, pDate.Day), i==0);
                    pDate = pDate.AddDays(1);
                }

                // add in all ovulation days
                DateTime oDate = date.AddDays(CycleLength / OVULATION_DIVISION_IN_DURATION);
                AllOvulationDates.Add(oDate);

                // add higher probabilty of fertility days, 
                // they're always 3 days before and 2 days after ovulation
                // http://www.ovulation-calculator.org/
                DateTime fDate = oDate.AddDays(FERTILITY_BEFORE_OVULATION * -1); // -1 to move back in time
                for (int i = 0; i < FERTILITY_DURATION; i++)
                {
                    AllFertilityDates.Add(new DateTime(fDate.Year, fDate.Month, fDate.Day));
                    fDate = fDate.AddDays(1);
                }

                // add the cyclelength to the date to move to the next cycle
                date = date.AddDays(CycleLength);
            }
        }
    }
}

