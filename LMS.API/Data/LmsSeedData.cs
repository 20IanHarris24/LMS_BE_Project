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
    internal class LmsSeedData
    {
        private static Faker faker = new Faker("en");
        internal static async Task InitAsync(DatabaseContext db)
        {

            //Check if data exists in the DB. If true stop seeding(return) else seed DB
            if (await db.Courses.AnyAsync()) return;


            var courses = GenerateCourses();
            await db.AddRangeAsync(courses);
            Console.WriteLine($"Courses seeded?.....");
           
            var modules = GenerateModules();
            await db.AddRangeAsync(modules);
            Console.WriteLine($"Modules seeded?.....");

            var activities = GenerateActivities(3);
            await db.AddRangeAsync(activities);
            Console.WriteLine($"Activities seeded?.....");
            
            var users = GenerateUsers(200);
            await db.AddRangeAsync(users);
            Console.WriteLine($"Users seeded?.....");

            await db.SaveChangesAsync();
            Console.WriteLine("Saving changes to the database.....");
        }



        private static List<Course> GenerateCourses()  //Generate Modules with random attributes
        {
            List<string> CourseSubject = new List<string> { "Computer Science", "Theology", "Mathematics", "Agriculture"};
            
            var courses = new List<Course>();

            for (int i = 0; i < CourseSubject.Count; i++)

            {
                //DateTime refDate = new DateTime(2004, 01, 01);
                //var dateOfBirth = faker.Date.Past(50, refDate);

                var course = new Course()
                {
                    Name = CourseSubject[i],
                    Description = faker.Lorem.Sentence(30, 15),
                    Start = DateTime.Now,
                };

                courses.Add(course);
            }
            return courses;
        }

        private static List<Module> GenerateModules(List<Course>courses)  //Generate Modules with random attributes
        {

            var modules = new List<Module>();

            for (int i = 0; i < courses.Count; i++)

            {

                //DateTime refDate = new DateTime(2004, 01, 01);

                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                //var dateOfBirth = faker.Date.Past(50, refDate);


                var module = new Module()
                {
                    Name = 
                    Description =
                    Start = 
                    End = 
                    CourseId =
                    Course =

    };

                modules.Add(module);
            }
            return modules;
        }






        private static List<UserForRegistrationDto> GenerateUsers(int numberOfUsers)  //Generate user (Teacher/Student) attributes randomly
        {

            var users = new List<UserForRegistrationDto>();
            var roleSelected = "";
            var faker = new Faker();
            List<Guid> courseIds = new List<Guid>();
            for (int i = 0; i < 20; i++)
            {

                courseIds.Add(Guid.NewGuid());
            }

            List<string> role = new List<string> { "Student", "Teacher" };

            for (int i = 0; i < numberOfUsers; i++)

            {

                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var domainName = faker.Internet.DomainName();
                var courseId = courseIds[faker.Random.Int(0, courseIds.Count -1)].ToString(); //choosing one of the 20 generated guid and assigning to a user

                if (numberOfUsers % 9 == 0)
                {
                    roleSelected = role[1];   //Choose a Teacher Role
                }
                else
                {

                    roleSelected = role[0]; //Choose a Student Role
                }
               

                var user = new UserForRegistrationDto

                {

                    UserName = $"{fName} {lName}",
                    Password = "Password123!",
                    Email = $"{fName}.{lName}@{domainName}",
                    Role = roleSelected,
                    CourseID = courseId
                };

                users.Add(user);
            };

            return users;
        }
        


        


        private static List<Genre> GenerateGenres(int numberOfGenres)
        {
            var genres = new List<Genre>();
            List<string> categories = new List<string> { "Action", "Drama", "Sci-Fi", "Western", "Arthouse", "Comedy", "Romance", "Horror", "Documentary", "Reality" };

            for (int i = 0; i < numberOfGenres; i++)
            {
                var genre = new Genre()
                {

                    Name = faker.PickRandom(categories),

                };
                genres.Add(genre);
            }
            return genres;
        }

        private static IEnumerable<Movie> GenerateMovieCards(int numberOfMovies, IEnumerable<Director> directors, IEnumerable<Actor> actors, IEnumerable<Genre> genres) //Generate random movie cards 
        {
            var rndSelect = new Random();
            var movies = new List<Movie>(numberOfMovies);
            List<string> adjectives = new List<string> { "Bombastic", "Haunted", "Golden", "Dark", "Silent", "Ancient", "Enchanted", "Slippery", "Hard", "Astounding" };
            List<string> verbs = new List<string> { "Return", "Rise", "Fall", "Quest", "Awakening", "Journey", "Uncover", "Escape", "Transform", "Conquer" };

            for (int i = 0; i < numberOfMovies; i++)
            {
                var director = faker.PickRandom<Director>(directors);
                var actor = actors.OrderBy(actor => rndSelect.Next()).Take(1).ToList();
                var genre = genres.OrderBy(genre => rndSelect.Next()).Take(1).ToList();


                DateTime startDate = new DateTime(1950, 01, 01);
                DateTime endDate = new DateTime(2023, 01, 01);
                string adjective = faker.PickRandom(adjectives);
                string verb = faker.PickRandom(verbs);
                string noun = faker.Lorem.Word();
                string city = faker.Address.City();
                string fName = faker.Name.FirstName();



                var filmTitle = $"{verb} {fName} of the {adjective} {noun} in {city}";
                var filmRating = new Random();
                var filmRelease = faker.Date.Between(startDate, endDate);
                var filmDescription = faker.Lorem.Sentence(15, 2);

                var movie = new Movie()
                {
                    Title = filmTitle,
                    Rating = filmRating.Next(0, 100),
                    ReleaseDate = filmRelease.ToShortDateString(),
                    Description = filmDescription,
                    Genres = genre,
                    Actors = actor,
                    Director = director,

                };

                movies.Add(movie);


            }
            return movies;






        }



    }
}
