using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Utils.Helper.TXT;
using Utils.Helper.CheckCorrectness;

namespace Utils.Helper.EMail
{
    /// <summary>
    /// E-Mail邮件帮助类
    /// 创建日期:2017年6月18日
    /// </summary>
    public class EMailHelper
    {
        /// <summary>
        /// 发送邮件(163SMTP)(未测试)
        /// </summary>
        /// <param name="strSender">发件人邮箱(163)</param>
        /// <param name="strSenderPassword">发件人密码(163)</param>
        /// <param name="listAddressee">收件人</param>
        /// <param name="listCC">抄送</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool SendMail_163SMTP(string strSender, string strSenderPassword, List<string> listAddressee, List<string> listCC, string strSubject, string strBody)
        {
            try
            {
                if (!CheckCorrectnessHelper.CheckEMail(strSender) || listAddressee.Count < 1 || string.IsNullOrEmpty(strSubject))
                {
                    return false;
                }
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(strSender);
                if (listAddressee != null)
                {
                    foreach (string strAddressee in listAddressee)
                    {
                        if (CheckCorrectnessHelper.CheckEMail(strAddressee))
                        {
                            mailMessage.To.Add(strAddressee);
                        }
                    }
                }
                if (listCC != null)
                {
                    foreach (string strCC in listCC)
                    {
                        if (CheckCorrectnessHelper.CheckEMail(strCC))
                        {
                            mailMessage.CC.Add(strCC);
                        }
                    }
                }
                mailMessage.Subject = strSubject;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Body = strBody;
                mailMessage.BodyEncoding = Encoding.UTF8;
                //是否是HTML邮件
                mailMessage.IsBodyHtml = false;
                //邮件优先级
                mailMessage.Priority = MailPriority.High;
                //使用163邮箱SMTP发送
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.163.com";
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new System.Net.NetworkCredential(strSender, strSenderPassword);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 发送邮件(QQSMTP)(未测试)
        /// </summary>
        /// <param name="strSender">发件人邮箱(QQ)</param>
        /// <param name="strAuthorizationCode">发件人密码(QQ)</param>
        /// <param name="listAddressee">收件人</param>
        /// <param name="listCC">抄送</param>
        /// <param name="strSubject">邮件标题</param>
        /// <param name="strBody">邮件内容</param>
        /// <returns>成功返回true,失败返回false</returns>
        public static bool SendMail_QQSMTP(string strSender, string strAuthorizationCode, List<string> listAddressee, List<string> listCC, string strSubject, string strBody)
        {
            try
            {
                if (!CheckCorrectnessHelper.CheckEMail(strSender) || listAddressee.Count < 1 || string.IsNullOrEmpty(strSubject))
                {
                    return false;
                }
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(strSender);
                if (listAddressee != null)
                {
                    foreach (string strAddressee in listAddressee)
                    {
                        if (CheckCorrectnessHelper.CheckEMail(strAddressee))
                        {
                            mailMessage.To.Add(strAddressee);
                        }
                    }
                }
                if (listCC != null)
                {
                    foreach (string strCC in listCC)
                    {
                        if (CheckCorrectnessHelper.CheckEMail(strCC))
                        {
                            mailMessage.CC.Add(strCC);
                        }
                    }
                }
                mailMessage.Subject = strSubject;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Body = strBody;
                mailMessage.BodyEncoding = Encoding.UTF8;
                //是否是HTML邮件
                mailMessage.IsBodyHtml = false;
                //邮件优先级
                mailMessage.Priority = MailPriority.High;
                //使用163邮箱SMTP发送
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.qq.com";
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new System.Net.NetworkCredential(strSender, strAuthorizationCode);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                TXTHelper.Logs(ex.ToString());
                return false;
            }
        }
    }
}
