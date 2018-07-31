using System.Text;

namespace ServiceInterface.Model
{
    public class signinList
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public signin[] signins { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (signins != null)
            {
                foreach (signin signin in signins)
                {
                    if (null == signin) continue;
                    sb.Append(signin.ToXmlString());
                }
            }

            return
                string.Format(
                    "<signinlist><success>{0}</success><message>{1}</message><signins>{2}</signins></signinlist>",
                    Success,
                    Message, sb);
        }
    }
}