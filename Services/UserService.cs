using ST10390916_PROG7311_POE.Data;
using ST10390916_PROG7311_POE.Models;

namespace ST10390916_PROG7311_POE.Services
{
    public class UserService
    {

        public int CheckUser(string email, string password)
        {
            int userID = -1;

            AppDBContext context = new AppDBContext();
            User? user = context.Users.Where(e => e.Email.Equals(email)).SingleOrDefault();

            if ((user != null) && (user.Password == password))
            {
                userID = user.UserID;
            }

            return userID;
        }

        public string AddNewUser(User user)
        {
            AppDBContext context = new AppDBContext();
            string msg = "";

            if (context.Users.Where(e => e.Email.Equals(user.Email)) != null)
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            else
            {
                msg = $"An account with {user.Email} already exists.";
            }

            return msg;
        }

        public User GetUser(int userID)
        {
            AppDBContext context = new AppDBContext();
            User? user = context.Users.Where(e => e.UserID == userID).SingleOrDefault();

            return user;
        }

        public List<User> GetUsers()
        {
            AppDBContext context = new AppDBContext();
            List<User> users = context.Users.ToList();

            return users;
        }

        //prepopulate the database with an admin user
        public void AddEmployee()
        {
            User employee = new User();
            //employee.UserID = 0;
            employee.FirstName = "Admin";
            employee.LastName = "Admin";
            employee.Email = "admin@admin.com";
            employee.Password = "Admin1";
            employee.Role = UserRole.Employee;

            AddNewUser(employee);
        }

        //prepopulate the database with another admin user
        public void AddEmployee2()
        {
            User employee = new User();
            //employee.UserID = 1;
            employee.FirstName = "Kevin";
            employee.LastName = "Bacon";
            employee.Email = "kevin@bacon.com";
            employee.Password = "123456";
            employee.Role = UserRole.Employee;

            AddNewUser(employee);
        }

        //prepopulate the database with a farmer user
        public void AddFarmer()
        {
            User farmer = new User();
            //farmer.UserID = 2;
            farmer.FirstName = "Farmer";
            farmer.LastName = "Farmer";
            farmer.Email = "farmer@farmer.com";
            farmer.Password = "Farmer";
            farmer.Role = UserRole.Farmer;

            AddNewUser(farmer);
        }

        //prepopulate the database with a farmer user
        public void AddFarmer2()
        {
            User farmer = new User();
            //farmer.UserID = 3;
            farmer.FirstName = "Gavin";
            farmer.LastName = "van Wyk";
            farmer.Email = "gavinvanwyk@gmail.com";
            farmer.Password = "123456";
            farmer.Role = UserRole.Farmer;

            AddNewUser(farmer);
        }

    }
}
