using System;

namespace YTTrimmer.Application
{
    public class TimeDataModel
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        
        public TimeSpan GetDifference
        {
            get
            {
                return End.Subtract(Start);
            }
        }

        public TimeDataModel(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }

        public TimeDataModel(string start, string end)
        {
            Start = TimeSpan.Parse(start);
            End = TimeSpan.Parse(end);
        }
    }
}