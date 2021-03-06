﻿using System.Text;

namespace ServiceInterface.Model
{
    public class HmwHelpPictureList
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwHelpPicture[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwHelpPicture item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwhelppicturelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwhelppicturelist>",
                    Success,
                    Message, sb);
        }
    }
}