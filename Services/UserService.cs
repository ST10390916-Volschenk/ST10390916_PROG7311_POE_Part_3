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
            employee.FirstName = "Admin";
            employee.LastName = "Admin";
            employee.Email = "admin@admin.com";
            employee.Password = "Admin1";
            employee.Role = UserRole.Employee;

            AddNewUser(employee);
        }

        //prepopulate the database with a farmer user
        public void AddFarmer()
        {
            User farmer = new User();
            farmer.FirstName = "Farmer";
            farmer.LastName = "Farmer";
            farmer.Email = "farmer@farmer.com";
            farmer.Password = "Farmer";
            farmer.Role = UserRole.Farmer;

            AddNewUser(farmer);
        }

    }
}
