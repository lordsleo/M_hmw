using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageCoalZoneList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageCoalZone[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageCoalZone item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpagecoalzonelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpagecoalzonelist>",
                    Success,
                    Message, sb);
        }
    }
}