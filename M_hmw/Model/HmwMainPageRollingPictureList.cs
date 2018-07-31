using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageRollingPictureList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageRollingPicture[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageRollingPicture item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpagerollingpicturelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpagerollingpicturelist>",
                    Success,
                    Message, sb);
        }
    }
}