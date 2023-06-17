namespace Gym_Aplication
{
    public class ItemT
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ItemT(int id, string firstName, string lastName)
        {
            Id = id;
            FullName =id+". "+ firstName + " " + lastName;
        }
    }


    public class ItemS
    {
        public int Id { get; set; }
        public string SalaName { get; set; }

        public ItemS(int id, string salaName)
        {
            Id = id;
            SalaName = salaName;
        }
    }
    public class ItemK
    {
        public int Id { get; set; }
        public string Name_Surname { get; set; }

        public ItemK(int id, string firstName, string lastName)
        {
            Id = id;
            Name_Surname = firstName + " " + lastName;
        }
    }
    public class ItemZ
    {
        
        public string Zajecia { get; set; }
        public int Id { get; set; }
        public ItemZ( string zajecia, int id)
        {

            Zajecia = zajecia;
            Id = id;
        }
    }

    public class ItemGS
    {

        public string GodzinaS { get; set; }
        public int Id { get; set; }
        public ItemGS(string godzinaS, int iD)
        {

            GodzinaS = godzinaS;
            Id = iD;
        }
    }


    public class ItemGK
    {

        public string GodzinaGK { get; set; }
        public int Id { get; set; }
        public ItemGK(string godzinaGK, int iD)
        {

            GodzinaGK = godzinaGK;
            Id = iD;
        }
    }


}
