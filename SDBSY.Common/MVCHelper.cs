using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SDBSY.Common
{
   public class MVCHelper
    {
        public static string GetValidMsg(ModelStateDictionary modelState)//有两个ModelStateDictionary类，别弄混乱了。要使用System.Web.Mvc下的
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count <= 0)
                {
                    continue;
                }
                sb.Append("错误：");
                foreach (var modelError in modelState[key].Errors)
                {
                    sb.AppendLine(modelError.ErrorMessage);
                }
            }
            return sb.ToString();
        }
        public static string ToQueryString(NameValueCollection nvc)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in nvc.AllKeys)
            {
                string value = nvc[key];
                sb.Append(key).Append("=").Append(Uri.EscapeDataString(value)).Append("&");
            }
            return sb.ToString().Trim('&');
        }
        /// <summary>
        /// 删除NameValueCollection中name键值对，如果name不存在也不会报错
        /// </summary>
        /// <param name="nvc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string RemoveQueryString(NameValueCollection nvc, string name)
        {
            NameValueCollection newNvc = new NameValueCollection(nvc);//复制一份，不要修改原来的
            newNvc.Remove(name);
            return ToQueryString(newNvc);
        }
        /// <summary>
        /// 修改NameValueCollection中的键值对，如果name存在就更新，否则就添加
        /// </summary>
        /// <param name="nvc"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UpdateQueryString(NameValueCollection nvc, string name, string value)
        {
            NameValueCollection newNvc = new NameValueCollection(nvc);
            if (newNvc.AllKeys.Contains(name))
            {
                newNvc[name] = value;
            }
            else
            {
                newNvc.Add(name, value);
            }
            return ToQueryString(newNvc);
        }
    }
}
