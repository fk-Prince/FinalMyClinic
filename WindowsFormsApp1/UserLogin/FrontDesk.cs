namespace ClinicSystem
{
    public class FrontDesk
    {
        private string username;
        private string password;
        private int id;
        private string type;
        public FrontDesk(string username, string password, int id, string type)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.type = type;
        }

        public int getId() {  return id; }
    }
}