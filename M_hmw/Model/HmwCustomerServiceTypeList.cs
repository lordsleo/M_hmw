using System.Text;

namespace ServiceInterface.Model
{
    public class HmwCustomerServiceTypeList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwCustomerServiceType[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwCustomerServiceType item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwcustomerservicetypelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwcustomerservicetypelist>",
                    Success,
                    Message, sb);
        }
    }
}