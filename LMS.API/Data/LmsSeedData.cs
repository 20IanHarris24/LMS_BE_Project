using Bogus;
using Humanizer.Localisation;
using LMS.API.Models.Dtos;
using LMS.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.IO;

namespace LMS.API.Data
{
    public class LmsSeedData
    {
        internal static Faker faker = new Faker("en");
        internal static async Task InitAsync(DatabaseContext db)
        {

            //This ia a check to see if data exists in the DB. If true don't seed(return). If you don't want to re-seed Courses/modules etc. remove the comment

            if (await db.Courses.AnyAsync()) return;


            var courses = SeedCourses();
            await db.AddRangeAsync(courses);
            Console.WriteLine($"Courses seeded?.....");

            var activityTypes = SeedActivityTypes();
            await db.AddRangeAsync(activityTypes);
            Console.WriteLine($"Activity Types seeded?.....");

            var modules = SeedModules(courses);
            await db.AddRangeAsync(modules);
            Console.WriteLine($"Modules seeded?.....");

            var activities = SeedActivities(modules, activityTypes);
            await db.AddRangeAsync(activities);
            Console.WriteLine($"Activities seeded?.....");

            await db.SaveChangesAsync();
            Console.WriteLine("Saving changes to the database.....");
        }


        public static List<Course> SeedCourses()  //Generate Modules with random attributes
        {
            List<string> courseSubject = new List<string> { "Computer Science", "Theology", "Mathematics", "Agriculture","Literature"};
            
            var courses = new List<Course>();

            foreach (var subject in courseSubject)
            {

                var course = new Course()
                {
                    Name = subject,
                    Description = faker.Lorem.Sentence(30, 15),
                    Start = DateTime.UtcNow,
                };

                courses.Add(course);

            }
            return courses;
        }


        public static List<ActivityType> SeedActivityTypes()
        {
            var activityTypes = new List<ActivityType>();

            List<string> activityCategory = new List<string> { "Lecture", "E-Learning", "Project", "Self Study" };
            
            foreach (var category in activityCategory)
            {
                    var type = new ActivityType()
                    {
                        Name = category,
                    };

                    activityTypes.Add(type);

            }
            return activityTypes;
        }



        public static List<Module> SeedModules(List<Course>courses)  //Generate Modules with random attributes
        {
            int noOfModulesPerCourse = courses.Count;
            var modules = new List<Module>();

            foreach (var course in courses)
            {
                var courseId = course.Id;
                            
                for ( int y = 0; y < noOfModulesPerCourse; y++)
                {
                    var module = new Module()
                    {

                        Name = faker.Commerce.Department(),
                        Description = faker.Lorem.Sentence(),
                        Start = DateTime.UtcNow.AddMonths(y),
                        End = DateTime.UtcNow.AddMonths(y+1),
                        CourseId = courseId,

                    };
                    
                    modules.Add(module);

                };

            }
            return modules;
        }



        public static List<Activity> SeedActivities(List<Module>modules, List<ActivityType> activityTypes)
        {
            int noOfActivitiesPerModule = activityTypes.Count;
            int dayOffset = 0;
            var activities = new List<Activity>();
                       
            foreach (var module in modules)
            {
                var moduleId = module.Id;

               
                    foreach (var activityType in activityTypes)
                    {
                        var activityTypeId = activityType.Id;

                        var activity = new Activity()
                        {

                            Name = faker.Commerce.Department(),
                            Description = faker.Lorem.Sentence(),
                            Start = DateTime.UtcNow.AddDays(dayOffset),
                            End = DateTime.UtcNow.AddDays(dayOffset + 5),
                            TypeId = activityTypeId,
                            ModuleId = moduleId,

                        };

                     activities.Add(activity);
                     dayOffset++;

                    };

            
            }
            return activities;
        }
       
    }
}
