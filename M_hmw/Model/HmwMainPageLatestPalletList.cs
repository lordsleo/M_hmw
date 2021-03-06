﻿using System.Text;

namespace ServiceInterface.Model
{
    public class HmwMainPageLatestPalletList : IXmlData
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HmwMainPageLatestPallet[] Value { get; set; }

        public string ToXmlString()
        {
            var sb = new StringBuilder();
            if (Value != null)
            {
                foreach (HmwMainPageLatestPallet item in Value)
                {
                    if (null == item) continue;
                    sb.Append(item.ToXmlString());
                }
            }

            return
                string.Format(
                    "<hmwmainpagelatestpalletlist><success>{0}</success><message>{1}</message><value>{2}</value></hmwmainpagelatestpalletlist>",
                    Success,
                    Message, sb);
        }
    }
}