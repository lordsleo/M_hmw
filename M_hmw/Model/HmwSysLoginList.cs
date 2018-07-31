using System.Text;

namespace ServiceInterface.Model
{
    public class HmwSysLoginList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwSysLogin[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if(Value != null)
            {
                foreach(HmwSysLogin item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                     "<hmwsysloginlist><success>{0}</success><message>{1}</message><value>{2}</value></hmwsysloginlist>",
                     Success,
                     Message,sb);
        }
    }

}
