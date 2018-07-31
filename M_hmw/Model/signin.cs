namespace ServiceInterface.Model
{
    public class signin
    {
        public string department { get; set; }
        public string power { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string password { get; set; }

        public string user { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<signin><department>{0}</department><power>{1}</power><user>{2}</user><tel>{3}</tel><name>{4}</name><password>{5}</password></signin>",
                    department, power, user, tel, name, password);
        }
    }
}