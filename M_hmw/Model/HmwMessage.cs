namespace ServiceInterface.Model
{
    public class HmwMessage : IXmlData
    {
        public int MsgId { get; set; }
        public string Content { get; set; }

        public string ToXmlString()
        {
            return string.Format("<hmwmessage><msgid>{0}</msgid><content>{1}</content></hmwmessage>", MsgId, Content);
        }
    }
}