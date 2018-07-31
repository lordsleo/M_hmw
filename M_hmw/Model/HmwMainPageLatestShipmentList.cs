using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageLatestShipmentList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageLatestShipment[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageLatestShipment item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpagelatestshipmentlist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpagelatestshipmentlist>",
                    Success,
                    Message, sb);
        }
    }
}