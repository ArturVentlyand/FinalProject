using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalProject
{
    public class DailyRoutine
    {
        [XmlElement]
        public DateTime Date { get; set; }
        [XmlElement]
        public List<Action> Actions { get; set; }

        public DailyRoutine()
        {
            Actions = new List<Action>();
        }

        public TimeSpan TotalWalkingDuration()
        {
            return Actions.
                Where(action => action.NameAction == "Walk").
                Select(action => action.DurationAction).
                Aggregate(TimeSpan.Zero, (duration1, duration2) => duration1 + duration2);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var action in Actions)
                sb.AppendLine(action.ToString());
            return sb.ToString().TrimEnd();
        }

        public void ChangeAction()
        {
            TimeSpan walkingDuration = TotalWalkingDuration();
            if (walkingDuration < TimeSpan.FromHours(2))
            {
                var watchTVActions = Actions.
                    Where(action => action.NameAction == "Watch TV" && action.StartTime >= DateTime.Today.AddHours(12)).
                    ToList();
                foreach (var actionWatchTV in watchTVActions)
                {
                    TimeSpan missingDuration = TimeSpan.FromHours(2) - walkingDuration;
                    int index = Actions.IndexOf(actionWatchTV);
                    Actions.Insert(index, new Action
                    {
                        NameAction = "Walk",
                        StartTime = actionWatchTV.StartTime,
                        DurationAction = missingDuration
                    });
                    Actions.Remove(actionWatchTV);
                }
            }
        }

        public static void SerializeToXml(DailyRoutine dailyRoutine, string filePath)
        {
            try
            {
                var serialize = new XmlSerializer(typeof(DailyRoutine));
                using (var stream = new StreamWriter(filePath))
                {
                    serialize.Serialize(stream, dailyRoutine);
                }
            }
            catch (Exception ex) { throw new InvalidOperationException("Failed to write data to file", ex); }
        }
        public static DailyRoutine DeserializeFromXml(string filePath)
        {
            try
            {
                var serialize = new XmlSerializer(typeof(DailyRoutine));
                using (var stream = new StreamReader(filePath))
                {
                    return (DailyRoutine)serialize.Deserialize(stream);
                }
            }
            catch (Exception ex) { throw new InvalidOperationException("Failed to read data from file", ex); }
        }
    }
}


