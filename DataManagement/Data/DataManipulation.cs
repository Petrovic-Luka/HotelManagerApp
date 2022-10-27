using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Numerics;
using System.Xml.Linq;
using System.Configuration;

namespace DataManagement;


public static class DataManipulation
{
   
    private static readonly string connString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Path.GetFullPath("HotelDb.mdf")};Integrated Security=True";
   

    public async static Task<List<RoomModel>> GetRoomsAsync()
    {
        using (SqlConnection cnn = new SqlConnection(connString))
        {
            var output = await cnn.QueryAsync<RoomModel>("spGetRoomsAll", commandType: CommandType.StoredProcedure);
            return (List<RoomModel>)output;
        }
    }

    public static async Task<List<GuestModel>> GetGuestsAsync(string name)
    {
        //List<GuestModel> output = new List<GuestModel>();

        using (SqlConnection cnn = new SqlConnection(connString))
        {
            cnn.Open();
            var p = new DynamicParameters();
            p.Add("@Name",name);
            var output =await cnn.QueryAsync<GuestModel>("spGetGuests",p, commandType: CommandType.StoredProcedure);
            return (List<GuestModel>)output;
        }
    }

    public static async Task<List<ReservationModel>> GetReservation(string name,string roomNumber)
    {
        using (SqlConnection cnn = new SqlConnection(connString))
        {
            DynamicParameters p = new();
            p.Add("@Guest_name", name);
            p.Add("@@Room_number", roomNumber);
            var output =await cnn.QueryAsync<ReservationModel>("spGetReservation",p, commandType: CommandType.StoredProcedure);
            return (List<ReservationModel>)output;
        }
    }

    /// <summary>
    /// Returns list of ReservationModels that Start on given date
    /// </summary>
    /// <param name="name"></param>
    /// <param name="roomNumber"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public static async Task<List<ReservationModel>> GetReservationWithDateAsync(string name, string roomNumber,string date)
    {
        string dateFormat = SqlDateFormat(date);
        using (SqlConnection cnn = new SqlConnection(connString))
        {
            DynamicParameters p = new();
            p.Add("@Guest_name", name);
            p.Add("@@Room_number", roomNumber);
            p.Add("@Start_date", dateFormat);
            //output = cnn.Query<ReservationModel>("spGetReservationWithDate", p, commandType: CommandType.StoredProcedure).ToList();
            var output = await cnn.QueryAsync<ReservationModel>("spGetReservationWithDate", p, commandType: CommandType.StoredProcedure);
            return (List<ReservationModel>)output;
        }
    }

    public static void SaveGuestAsync(string name,string email,string phone)
    {
        if(!CheckData.CheckGuestData(name,email,phone))
        {
            throw new Exception("incorrect format");
        }
        using (SqlConnection cnn = new SqlConnection(connString))
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Name", name);
            p.Add("@Email", email);
            p.Add("@Phone", phone);
            cnn.QueryAsync("spInsertNewGuest", p, commandType: CommandType.StoredProcedure);
        }
        
    }
   
    public static void SaveReservationAsync(int id, string name, string roomNumber, string startDate, string endDate)
    {
        if (!CheckData.CheckReservation(roomNumber, startDate, endDate))
        {
            throw new Exception("Room is already reserved");
        }

        using (SqlConnection cnn = new SqlConnection(connString))
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Guest_Id", id);
            p.Add("@Guest_Name", name);
            p.Add("@Room_Number", roomNumber);
            p.Add("@Start_Date", SqlDateFormat(startDate));
            p.Add("@End_date", SqlDateFormat(endDate));
            cnn.QueryAsync("spInsertNewReservation", p, commandType: CommandType.StoredProcedure);
        }
    }

    public static void SaveRoomAsync(string roomNumber,int bedsNumber)
    {
        if(!CheckData.CheckRoomData(roomNumber,bedsNumber))
        {
            throw new Exception("Bad format");
        }

        using (SqlConnection cnn = new SqlConnection(connString))
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Room_Number", roomNumber);
            p.Add("@Beds_Number", bedsNumber);
            cnn.QueryAsync("spInsertNewRoom", p, commandType: CommandType.StoredProcedure);
        }
    }

    public static void AlterGuestAsync(IGuest guest)
    {
        using (SqlConnection cnn = new SqlConnection(connString))
        {
            if (!CheckData.CheckGuestData(guest.Name, guest.Email, guest.Phone_Number))
            {
                throw new Exception("incorrect format");
            }
            DynamicParameters p = new DynamicParameters();
            p.Add("@Guest_id", guest.Guest_Id);
            p.Add("@Name", guest.Name);
            p.Add("@Email", guest.Email);
            p.Add("@Phone", guest.Phone_Number);
            cnn.QueryAsync("spAlterGuest", p, commandType: CommandType.StoredProcedure);
        }

    }

    public static void UpdateNameInReservationsAsync(int id,string name)
    {
        using (SqlConnection cnn = new SqlConnection(connString))
        {
            DynamicParameters p = new();
            p.Add("@GuestId", id);
            p.Add("@Name", name);
            cnn.Open();
            cnn.QueryAsync("spUpdateReservationsName", p, commandType: CommandType.StoredProcedure);
        }
    }

    public static void DeleteReservationAsync(int id)
    {
        using (SqlConnection cnn = new SqlConnection(connString))
        {
            DynamicParameters p = new();
            p.Add("@id", id);
            cnn.Open();
            cnn.QueryAsync("spDeleteReservation", p, commandType: CommandType.StoredProcedure);
        }
    }
    
    /// <summary>
    /// takes dd/mm/yy format and converts into yy/mm/dd
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string SqlDateFormat(string date)
    {
        string output = date.Split("/")[2] + "/" + date.Split("/")[1] + "/" + date.Split("/")[0];
        return output;
    }

    public static int Test()
    {
        int results = 0;
        int a = 17;
        using (SqlConnection cnn = new SqlConnection(connString))
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@cnt", results, direction: ParameterDirection.Output);
            a=cnn.Query("spTest",p,commandType:CommandType.StoredProcedure).Count();
        }
        return a;
    }
}
