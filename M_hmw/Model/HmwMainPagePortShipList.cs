using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPagePortShipList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPagePortShip[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPagePortShip item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpageportshiplist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpageportshiplist>",
                    Success,
                    Message, sb);
        }
    }
}