using System.Web;

namespace Gym_Aplication
{
    internal class ScheduleEntry1
    {
       
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Activity { get; set; }
       
        public string Room { get; set; }

        public string Date { get; set; }

        public string Start_time { get; set; }
        public string End_time { get; set;}
    }

    internal class ScheduleEntry2
    {

        public string ID { get; set; }
        public string NameT { get; set; }
        public string NameK { get; set; }
        
        public string Activity { get; set; }

        

        public string Date { get; set; }

        public string Start_time { get; set; }
        public string End_time { get; set; }


        public ScheduleEntry2(string iD, string nameT, string surnameT, string nameK, string surnameK, string activity, string date, string start_time, string end_time)
        {
            ID = iD;
            NameT = nameT+" "+ surnameT;
            NameK = nameK+ " "+ surnameK;
            Activity = activity;
            Date = date;
            Start_time = start_time;
            End_time = end_time;
        }
    }



}


