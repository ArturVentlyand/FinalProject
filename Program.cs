using System;
using System.Collections.Generic;
using System.Globalization;

namespace FinalProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathToMainFile = @"C:\Users\ventl\OneDrive\Рабочий стол\Projects\Study.CS.OOP\FinalProject\TheDailyRoutinePerson.txt";
            string pathToWalkFile = @"C:\Users\ventl\OneDrive\Рабочий стол\Projects\Study.CS.OOP\FinalProject\TotalWalkingDuration.txt";
            string pathToChangedFile = @"C:\Users\ventl\OneDrive\Рабочий стол\Projects\Study.CS.OOP\FinalProject\ChangedActionInDailyRoutine.txt";
            DailyRoutine dailyRoutine = new DailyRoutine();

            try
            {
                string[] lines = File.ReadAllLines(pathToMainFile);
                foreach (string line in lines)
                {
                    Action action = new Action();
                    string[] values = line.Split(" - ");
                    if (values.Length == 3)
                    {
                        action.NameAction = values[1];
                        action.StartTime = DateTime.Parse(values[0]);
                        action.DurationAction = TimeSpan.Parse(values[2]);
                        dailyRoutine.Actions.Add(action);
                    }
                    else
                        throw new FormatException();
                }
            }
            catch (DirectoryNotFoundException ex) { Console.WriteLine(ex.Message); }
            catch (UnauthorizedAccessException ex) { Console.WriteLine(ex.Message); }
            catch (FormatException ex) { Console.WriteLine(ex.Message); }
            catch (IOException ex) { Console.WriteLine(ex.Message); }

            TimeSpan walkingDuration = dailyRoutine.TotalWalkingDuration();
            try
            {
                using (StreamWriter sw = new StreamWriter(pathToWalkFile))
                    sw.Write("Total walking duration: " + walkingDuration);
            }
            catch (DirectoryNotFoundException ex) { Console.WriteLine(ex.Message); }
            catch (UnauthorizedAccessException ex) { Console.WriteLine(ex.Message); }
            catch (IOException ex) { Console.WriteLine(ex.Message); }

            if (walkingDuration < TimeSpan.FromHours(2))
                dailyRoutine.ChangeAction();

            try { File.WriteAllText(pathToChangedFile, dailyRoutine.ToString()); }
            catch (DirectoryNotFoundException ex) { Console.WriteLine(ex.Message); }
            catch (UnauthorizedAccessException ex) { Console.WriteLine(ex.Message); }
            catch (IOException ex) { Console.WriteLine(ex.Message); }
        }
    }
}