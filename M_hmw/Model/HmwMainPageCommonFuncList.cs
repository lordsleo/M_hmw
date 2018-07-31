using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageCommonFuncList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageCommonFunc[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageCommonFunc item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpagecommonfunclist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpagecommonfunclist>",
                    Success,
                    Message, sb);
        }
    }
}