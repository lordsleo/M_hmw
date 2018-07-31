using System.Text;

namespace ServiceInterface.Model
{
    public class HmwLoadingPictureList
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwLoadingPicture[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwLoadingPicture item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwloadingpicturelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwloadingpicturelist>",
                    Success,
                    Message, sb);
        }
    }
}