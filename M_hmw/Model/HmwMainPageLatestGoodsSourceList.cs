using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageLatestGoodsSourceList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageLatestGoodsSource[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageLatestGoodsSource item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpagelatestgoodssourcelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpagelatestgoodssourcelist>",
                    Success,
                    Message, sb);
        }
    }
}