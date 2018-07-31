using System.Text;

namespace ServiceInterface.Model
{
    public class DataList
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string datas { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                    "<DataList><success>{0}</success><message>{1}</message><Datas>{2}</Datas></DataList>",
                    Success,
                    Message, datas);
        }
    }
}