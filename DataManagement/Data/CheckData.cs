using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;

namespace DataManagement
{
    public static class CheckData
    {
        //TODO namesti cnn string u config fajlu
        private static readonly string connString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetFullPath("HotelDb.mdf")};Integrated Security=True";
        /// <summary>
        /// Checks if the given room is already reserved for given time frame
        /// </summary>
        /// <param name="RoomNumber"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public  static bool CheckReservation(string roomNumber, string startDate, string endDate)
        {
            if (!CheckDates(startDate, endDate))
            {
                throw new FormatException("Bad date format");
            }
            int results = 0;
            string sDate = DataManipulation.SqlDateFormat(startDate);
            string eDate = DataManipulation.SqlDateFormat(endDate);
            using (SqlConnection cnn = new SqlConnection(connString))
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@RoomNumber", roomNumber);
                p.Add("@StartDate", sDate);
                p.Add("@EndDate", eDate);
                cnn.Open();
                results =  cnn.Query("spCheckReservation", p, commandType: CommandType.StoredProcedure).Count();
            }
            return results==0;
        }
        public static bool CheckDates(string start, string end)
        {
           if (start == null || end == null) return false;
           if (start.Length == 0 || end.Length == 0) return false;
           var startDate = DateTime.Parse(start);
           var endDate = DateTime.Parse(end);
           return startDate <= endDate;
        }
        public static bool CheckGuestData(string name, string email, string phone)
        {
            //check nulls
            if (name == null || email == null || phone == null) return false;
            //check min lengths
            if (name.Length < 2 || email.Length < 5 || phone.Length < 3) return false;
            //check max lengths
            if (name.Length > 50 || email.Length > 50 || phone.Length > 20) return false;
            //check if email is valid
            try
            {
                MailAddress m = new MailAddress(email);
            }
            catch (FormatException)
            {
                return false;
            }
            if (phone.Where(x => Char.IsLetter(x)).Count() > 0) return false;

            return true;
        }
        public static bool CheckGuestData(IGuest g)
        {
           return CheckGuestData(g.Name, g.Email, g.Phone_Number);
        }

        public static bool CheckRoomData(string roomNumber,int bedsNumber)
        {
            if (bedsNumber < 0) return false;
            if (roomNumber.Length > 5) return false;
            if (roomNumber.Where(x => !char.IsLetter(x) && !char.IsDigit(x)).Count() > 0) return false;
            return true;
        }
    }
}
