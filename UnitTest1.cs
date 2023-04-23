using FinalProject;
using Action = FinalProject.Action;

namespace TestProject1
{
    [TestClass]
    public class DailyRoutineTest
    {
        [TestMethod]
        public void TotalWalkingDurationTest()
        {
            DailyRoutine dailyRoutine = new DailyRoutine();
            Action action00 = new Action()
            {
                StartTime = DateTime.Parse("7:00 AM"),
                NameAction = "Walk",
                DurationAction = TimeSpan.FromMinutes(30)
            };
            Action action01 = new Action()
            {
                StartTime = DateTime.Parse("07:00 PM"),
                NameAction = "Walk",
                DurationAction = TimeSpan.FromMinutes(30)
            };
            dailyRoutine.Actions.Add(action00);
            dailyRoutine.Actions.Add(action01);

            var expected = TimeSpan.FromHours(1);

            TimeSpan result = dailyRoutine.TotalWalkingDuration();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ToStringTest()
        {
            DailyRoutine dailyRoutine = new DailyRoutine();
            Action action00 = new Action()
            {
                StartTime = DateTime.Parse("7:00 AM"),
                NameAction = "Walk",
                DurationAction = TimeSpan.FromMinutes(30)
            };
            Action action01 = new Action()
            {
                StartTime = DateTime.Parse("07:00 PM"),
                NameAction = "Walk",
                DurationAction = TimeSpan.FromMinutes(30)
            };
            dailyRoutine.Actions.Add(action00);
            dailyRoutine.Actions.Add(action01);

            string expected = "07:00 AM - Walk - 00:30:00\r\n07:00 PM - Walk - 00:30:00";

            Assert.AreEqual(expected, dailyRoutine.ToString());
        }

        [TestMethod]
        public void ChangeActionTest()
        {
            DailyRoutine dailyRoutine = new DailyRoutine();
            Action action00 = new Action()
            {
                StartTime = DateTime.Parse("7:00 AM"),
                NameAction = "Walk",
                DurationAction = TimeSpan.FromMinutes(30)
            };
            Action action01 = new Action()
            {
                StartTime = DateTime.Parse("07:00 PM"),
                NameAction = "Watch TV",
                DurationAction = TimeSpan.FromMinutes(30)
            };
            dailyRoutine.Actions.Add(action00);
            dailyRoutine.Actions.Add(action01);

            string expected = "07:00 AM - Walk - 00:30:00\r\n07:00 PM - Walk - 01:30:00";

            dailyRoutine.ChangeAction();

            Assert.AreEqual(expected, dailyRoutine.ToString());
        }
    }
}