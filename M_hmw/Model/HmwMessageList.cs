﻿using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMessageList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMessage[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMessage item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmessagelist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmessagelist>",
                    Success,
                    Message, sb);
        }
    }
}