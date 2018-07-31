using System.Text;

namespace ServiceInterface.Model
{
    public class HmwSysPersonInfoList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwSysPersonInfo[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwSysPersonInfo item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                     "<hmwsyspersoninfolist><success>{0}</success><message>{1}</message><value>{2}</value></hmwsyspersoninfolist>",
                     Success,
                     Message, sb);
        }
    }
}