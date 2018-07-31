using System.Text;

namespace ServiceInterface.Model
{
    public class HmwCustomerServiceList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwCustomerService[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwCustomerService item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwcustomerservicelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwcustomerservicelist>",
                    Success,
                    Message, sb);
        }
    }
}