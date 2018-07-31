using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageLatestTransCapList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageLatestTransCap[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageLatestTransCap item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpagelatesttranscaplist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpagelatesttranscaplist>",
                    Success,
                    Message, sb);
        }
    }
}