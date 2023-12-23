using System;

namespace ConsoleApp6
{
    public struct Worker
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime Birthday { get; set; }
        public string Birthplace { get; set; }

        public Worker (
            int id, 
            DateTime createDateTime, 
            string fullName, 
            int age, 
            int height, 
            DateTime birthday, 
            string birthplace)
        {
            Id = id;
            CreateDateTime = createDateTime;
            FullName = fullName;
            Age = age;
            Height = height;
            Birthday = birthday;
            Birthplace = birthplace;
        }

        public Worker (int id, string fullName) : 
            this(id, 
                DateTime.Now, 
                fullName, 
                0, 
                0, 
                new DateTime(1900, 1,1, 0,0,0), 
                String.Empty)
        {           

        }

        public Worker(int id, string fullName, int age) :
            this(id,
                DateTime.Now,
                fullName, 
                age, 
                0, 
                new DateTime(1900, 1, 1, 0, 0, 0), 
                String.Empty)
        {
            
        }

        public Worker(int id, string fullName, int age, int height) :
            this(id, 
                DateTime.Now, 
                fullName, 
                age, 
                height, 
                new DateTime(1900, 1, 1, 0, 0, 0), 
                String.Empty)
        {
            
        }

        public Worker(int id, string fullName, int age, int height, DateTime birthday) :
           this(id,
               DateTime.Now, 
               fullName, 
               age, 
               height, 
               birthday, 
               String.Empty)
        {
            
        }
    }
}
