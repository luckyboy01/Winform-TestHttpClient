using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Winform_TestHttpClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUrl.Text = "https://ditest.oklinklink.com:8150/taskScheduler/asyncRun/relationupload";
            txtUrl.Text = "http://10.10.100.21:8150/taskScheduler/asyncRun/productiondata";
            //txtPostData.Text = "{\"orderCode\":\"T240123\",\"productLineCode\":\"trayLine\",\"workShop\":\"admin\",\"matCode\":\"123\",\"packageRate\":\"1:6\",\"batchNo\":\"240123\",\"subBatchNo\":\"\",\"productDate\":\"2024-01-01\",\"invalidDate\":\"2026-01-01\",\"pinBatchNo\":null,\"pinProductDate\":null,\"pinInvalidDate\":null,\"productCode\":\"1914701\",\"productName\":\"碘海醇注射液\",\"subNo\":\"1914701005\",\"linkProductCode\":\"1914701005\",\"assCorpCode\":\"\",\"qty\":2,\"palletQty\":1,\"cartonQty\":2,\"bundleQty\":0,\"unitQty\":12,\"isLast\":0,\"remark\":null,\"remark1\":null,\"remark2\":null,\"data\":[{\"parentPackCode\":null,\"packCode\":\"9147010001\",\"childQty\":2,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":9,\"data\":[{\"parentPackCode\":\"9147010001\",\"packCode\":\"88632390000000194105\",\"childQty\":6,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":2,\"data\":[{\"parentPackCode\":\"88632390000000194105\",\"packCode\":\"81578820000000556674\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000194105\",\"packCode\":\"81578820000000545136\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000194105\",\"packCode\":\"81578820000000523731\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000194105\",\"packCode\":\"81578820000000539003\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000194105\",\"packCode\":\"81578820000000502670\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000194105\",\"packCode\":\"81578820000000511542\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null}]},{\"parentPackCode\":\"9147010001\",\"packCode\":\"88632390000000208421\",\"childQty\":6,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":2,\"data\":[{\"parentPackCode\":\"88632390000000208421\",\"packCode\":\"81578820000000619215\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000208421\",\"packCode\":\"81578820000000560206\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000208421\",\"packCode\":\"81578820000000578764\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000208421\",\"packCode\":\"81578820000000582375\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000208421\",\"packCode\":\"81578820000000599670\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null},{\"parentPackCode\":\"88632390000000208421\",\"packCode\":\"81578820000000602066\",\"childQty\":0,\"pinChildQty\":0,\"packFlag\":0,\"levelNo\":1,\"data\":null}]}]}]}";
            txtTime.Text = "1";
            txtUrl.Text = "https://ecd-d.bgnesz.com:8150/taskScheduler/asyncRun/rebackdata";
            //txtUrl.Text = "https://ecd-d.bgnesz.com:8150/taskScheduler/asyncRun/rebackdata";
            txtPostData.Text = "{\"orderCode\":\"T240123\",\"productLineCode\":\"T240123\",\"matCode\":null,\"batchNo\":\"240123\",\"subBatchNo\":\"\",\"productCode\":\"1914701\",\"productName\":\"碘海醇注射液\",\"subNo\":\"1914701003\",\"operateType\":3,\"packCode\":\"88000130000000005615\",\"levelNo\":2,\"secPackCode\":null,\"remark\":null,\"remark1\":null,\"remark2\":null}";
            //var filePath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            //if (!File.Exists(filePath))
            //{
            //    File.Create(filePath);
            //}
        }

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string GetHttpWebResponse(string url, string header, object postData, string Method = "POST")
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                // 这里设置了协议类型。
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;// SecurityProtocolType.Tls1.2; 
                request = WebRequest.Create(url) as HttpWebRequest;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request.ProtocolVersion = HttpVersion.Version11;
                request.KeepAlive = false;
                request.AllowAutoRedirect = true;
                ServicePointManager.CheckCertificateRevocationList = false;
                ServicePointManager.DefaultConnectionLimit = 100;
                ServicePointManager.Expect100Continue = false;

            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(url);
            }

            if (!string.IsNullOrEmpty(header))
            {
                request.Headers.Add("Authorization", header);
            }

            request.Method = Method;
            request.ContentType = "application/json;charset=UTF-8";
            if (url.Contains("UploadCollectionRates") || url.Contains("Services/ScanCodeService.asmx"))
            {
                request.ContentType = "text/xml;charset=utf-8";
            }
            //request.ContentLength = postData.Length;
            request.Timeout = 100000;

            HttpWebResponse response = null;

            try
            {
                if (Method != "GET")
                {
                    StreamWriter swRequestWriter = new StreamWriter(request.GetRequestStream());
                    swRequestWriter.Write(postData);

                    if (swRequestWriter != null)
                        swRequestWriter.Close();
                }

                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                    throw ex;

                HttpWebResponse exResponse = (HttpWebResponse)ex.Response;
                //500错误
                if (exResponse.StatusCode == HttpStatusCode.InternalServerError)
                {
                    using (var stream = exResponse.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                if (exResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    using (var stream = exResponse.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response.Close();
                if (request != null)
                    request.Abort();
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int times = 0;
            if(string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                MessageBox.Show("请输入请求地址","警告", MessageBoxButtons.OK);
                return;
            }
            if(string.IsNullOrWhiteSpace(txtPostData.Text))
            {
                MessageBox.Show("请输入请求报文","警告", MessageBoxButtons.OK);
                return;
            }
            int.TryParse(txtTime.Text, out times);
            if(times <= 0)
            {

                MessageBox.Show("请求次数请输入正整数", "警告", MessageBoxButtons.OK);
                return;
            }
            var res = string.Empty;
            for (int i = 0; i < times; i++)
            {
                try
                {
                    res = GetHttpWebResponse(txtUrl.Text.Trim(), "", txtPostData.Text) + "\r\n";
                }
                catch (Exception ex)
                {
                    res = ex.Message + "\r\n";
                }
                Thread.Sleep(100);
                AppendMsg(res);
                //txtResult.Text = res;
            }
        }
        void AppendMsg(string msg)
        {
            msg = string.Concat("[", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"), "]:", msg);
            txtBox.Items.Add(msg);
            this.txtBox.SelectedIndex = this.txtBox.Items.Count - 1;
            this.txtBox.SelectedIndex = -1;
            var filePath = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            ////if (!File.Exists(filePath))
            ////    File.Create(filePath);
            ////File.WriteAllText(filePath, msg);
            using (StreamWriter writer = new StreamWriter(filePath, true)) // true 代表追加模式
            {
                writer.WriteLine(msg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int times = 0;
            if (string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                MessageBox.Show("请输入请求地址", "警告", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPostData.Text))
            {
                MessageBox.Show("请输入请求报文", "警告", MessageBoxButtons.OK);
                return;
            }
            int.TryParse(txtTime.Text, out times);
            if (times <= 0)
            {

                MessageBox.Show("请求次数请输入正整数", "警告", MessageBoxButtons.OK);
                return;
            }
            var res = string.Empty;
            for (int i = 0; i < times; i++)
            {
                try
                {
                    res = GetHttpWebResponse(txtUrl.Text.Trim(), "", txtPostData.Text, "GET") + "\r\n";
                }
                catch (Exception ex)
                {
                    res = ex.Message + "\r\n";
                }
                Thread.Sleep(100);
                AppendMsg(res);
                //txtResult.Text = res;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = GetHttpWebResponse("http://192.168.141.7:8083/Api/BasicAuthentication/login", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("admin:123")), null, "GET");
            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.141.7:8083/Api/BasicAuthentication/login");
            //request.Headers.Add("Authorization", "••••••");
            //var content = new StringContent("", null, "application/json");
            //request.Content = content;
            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
