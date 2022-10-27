using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement;

public class ReservationModel
{
    public int Id { get; init; }

    public int Guest_Id { get; set; }

    public string Guest_Name { get; set; }

    public string Room_Number { get; set; }

    public DateTime Start_Date { get; set;}

    public DateTime End_Date { get; set; }

    public bool Paid { get; set; }

    public string Display { get { return $"Guest: {Guest_Name} Room: {Room_Number} Duration: {Start_Date.Date.ToString().Split(" ")[0]} - {End_Date.Date.ToString().Split(" ")[0]} Paid: {Paid}"; } }

}
