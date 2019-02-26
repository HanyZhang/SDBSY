using qcloudsms_csharp;
using qcloudsms_csharp.httpclient;
using qcloudsms_csharp.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDBSY.Common
{
    public static class SendSMS
    {
        // 短信应用SDK AppID
        private static readonly int appid = 1400095109;
        // 短信应用SDK AppKey
        private static readonly string appkey = "a9424deeebd7909248e0ab1811a2e9bb";
        // 短信模板ID，需要在短信应用中申请
        private static readonly int templateId = 174087; 
        public static SendSmsResult Send(string phoneNum,string code)
        {
            try
            {
                SmsSingleSender sender = new SmsSingleSender(appid, appkey);
                //模板发送
                var sendResult = sender.sendWithParam("86", phoneNum, templateId, new[] { code }, "", "", "");
                SendSmsResult smsResult = new SendSmsResult {
                    result = sendResult.result,
                    errMsg = sendResult.errMsg,
                    ext=sendResult.ext,
                    sid=sendResult.sid,
                    fee=sendResult.fee,
                };
                return smsResult;
            }
            catch(JSONException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch(HTTPException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch(Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        public static SendSmsResult SendTest(string phone,string mess)
        {
            try
            {
                SmsSingleSender sender = new SmsSingleSender(appid, appkey);
                //模板发送
                var sendResult = sender.send(0,"86", phone, mess, "", "");
                SendSmsResult smsResult = new SendSmsResult
                {
                    result = sendResult.result,
                    errMsg = sendResult.errMsg,
                    ext = sendResult.ext,
                    sid = sendResult.sid,
                    fee = sendResult.fee,
                };
                return smsResult;
            }
            catch (JSONException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch (HTTPException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        
    }
    public class SendSmsResult
    {
        public int result;
        public string errMsg;
        public string ext;
        public string sid;
        public int fee;
    }
}
