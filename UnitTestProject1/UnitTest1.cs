using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string url = "http://gw.api.taobao.com/router/rest";
            string appkey = "24534188";
            string secret = "0cc64576afae93bd186714cfb25f8d40";




            //提供淘宝客批量创建广告位
            //无法找到TbkAdzoneCreateRequest
            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TbkAdzoneCreateRequest req = new TbkAdzoneCreateRequest();
            //req.SiteId = 123456L;
            //req.AdzoneName = "广告位";
            //TbkAdzoneCreateResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);

            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TbkTpwdCreateRequest req = new TbkTpwdCreateRequest();
            //req.UserId = "123";
            //req.Text = "长度大于5个字符";
            //req.Url = "https://uland.taobao.com/";
            //req.Logo = "https://uland.taobao.com/";
            //req.Ext = "{}";
            //TbkTpwdCreateResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);

            //ITopClient client = new DefaultTopClient(url, appkey, secret);
            //TbkItemGetRequest req = new TbkItemGetRequest();
            //req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick";
            //req.Q = "女装";
            //req.Cat = "16,18";
            //req.Itemloc = "杭州";
            //req.Sort = "tk_rate_des";
            //req.IsTmall = false;
            //req.IsOverseas = false;
            //req.StartPrice = 10L;
            //req.EndPrice = 10L;
            //req.StartTkRate = 123L;
            //req.EndTkRate = 123L;
            //req.Platform = 1L;
            //req.PageNo = 123L;
            //req.PageSize = 20L;

            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //TbkItemGetResponse rsp = client.Execute(req);
            //Console.WriteLine(rsp.Body);
        }
        public static string SignTopRequest(IDictionary<string, string> parameters, string secret, string signMethod)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder();
            if (Constants.SIGN_METHOD_MD5.Equals(signMethod))
            {
                query.Append(secret);
            }
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }

            // 第三步：使用MD5/HMAC加密
            byte[] bytes;
            if (Constants.SIGN_METHOD_HMAC.Equals(signMethod))
            {
                HMACMD5 hmac = new HMACMD5(Encoding.UTF8.GetBytes(secret));
                bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
            }
            else
            {
                query.Append(secret);
                MD5 md5 = MD5.Create();
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
            }

            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}
