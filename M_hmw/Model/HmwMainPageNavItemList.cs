using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageNavItemList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageNavItem[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageNavItem item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpagenavitemlist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpagenavitemlist>",
                    Success,
                    Message, sb);
        }
    }
}