using System;
using Microsoft.Win32.TaskScheduler;

class Program
{
    static void Main(string[] args)
    {
        using (TaskService ts = new TaskService())
        {
            // Create a new task definition and assign properties
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Shutdown PC at 11 PM";

            // Create a daily trigger at 11 PM
            DailyTrigger dailyTrigger = new DailyTrigger();
            dailyTrigger.StartBoundary = DateTime.Today + TimeSpan.FromHours(23); // 11 PM today
            td.Triggers.Add(dailyTrigger);

            // Create an action that will launch the shutdown command
            td.Actions.Add(new ExecAction("shutdown", "/s /f"));

            // Register the task in the root folder
            ts.RootFolder.RegisterTaskDefinition(@"ShutdownAt11PM", td);
        }

        Console.WriteLine("Task scheduled. Press any key to exit.");
        Console.ReadKey();
    }
}
