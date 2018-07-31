using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageOreZoneList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageOreZone[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageOreZone item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpageorezonelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpageorezonelist>",
                    Success,
                    Message, sb);
        }
    }
}