namespace ServiceInterface.Model
{
    /// <summary>
    /// 用户登陆
    /// </summary>
    public class HmwSysLogin : IXmlData
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string CodeUser { set; get; }       
        /// <summary>
        /// 部门编号
        /// </summary>
        public string CodeDepartment { get; set; } 
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CodeCompany { get; set; }

        public string ToXmlString()
        {
            return
                string.Format(
                     "<hmwsyslogin><codeuser>{0}</codeuser><codedepartment>{1}</codedepartment><codecompany>{2}</codecompany></hmwsyslogin>",
                     CodeUser,
                     CodeDepartment,
                     CodeCompany
                );
        }

    }
}