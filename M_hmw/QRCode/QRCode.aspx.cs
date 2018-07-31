using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ServiceInterface.Common;
using ThoughtWorks.QRCode.Codec;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace M_hmw.QRCode
{
    public partial class QRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var message = Request.Params["Message"];
            //message = "227 1111111";
            var str = message.ToString().Split(' ');
            var codeUser = str[0];
            var content = str[1];

            Bitmap bmp = null;
            MemoryStream stream = null;
            try
            {
                if (message == null)
                {
                    Json = "参数Message不能为null！";
                    return;
                }

                //数据加密编码
                string strBase64Content = Base64Tool.Base64Code(content);
                //生成二维码Bitmap
                QRCodeEncoder qrCode = new QRCodeEncoder();
                qrCode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCode.QRCodeScale = 6;
                qrCode.QRCodeVersion = 7;
                qrCode.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                bmp = qrCode.Encode(strBase64Content);
                //Bitmap转换成MemoryStream
                stream = new System.IO.MemoryStream();
                bmp.Save(stream, ImageFormat.Jpeg);
                //内存流MemoryStream转成bytes
                byte[] bt = stream.ToArray();
                stream.Write(bt, 0, bt.Length);
                //获取上传地址
                string strQRCodePicPath = FileTool.GetWebConfigKey("QRCodePicUploadPath");
                string strQRCodePicName = codeUser + ".jpg";

                if (!FileTool.UploadFile(strQRCodePicPath, strQRCodePicName, bt))
                {
                    Json = "False";
                }

                Json = JsonConvert.SerializeObject(strQRCodePicName);
            }
            catch (Exception ex)
            {
                LogTool.WriteLog(typeof(QRCode), ex);
                Json = "Error";
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (bmp != null)
                {
                    bmp.Dispose();
                }
            }
        }
        protected string Json;
    }
}