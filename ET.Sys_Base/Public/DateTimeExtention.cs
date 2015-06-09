using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.Sys_Base.Public
{
   public class DateTimeExtention
    {
        /// <summary>
        /// Calculates number of business days, taking into account:
        ///  - weekends (Saturdays and Sundays)
        ///  - bank holidays in the middle of the week
        /// </summary>
        /// <param name="firstDay">First day in the time interval</param>
        /// <param name="lastDay">Last day in the time interval</param>
        /// <param name="bankHolidays">List of bank holidays excluding weekends</param>
        /// <returns>Number of business days during the 'span'</returns>
        public static int BusinessDaysUntil( DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = firstDay.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)firstDay.DayOfWeek;
                int lastDayOfWeek = lastDay.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)lastDay.DayOfWeek;

                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay)
                    --businessDays;
            }

            return businessDays;
        }



        /// <summary>
        /// 判断是不是节假日,节假日返回true 
        /// </summary>
        /// <param name="date">日期格式：yyyyMMdd</param>
        /// <returns></returns>
        public static bool IsHolidayByDate(string date)
        {
            bool isHoliday = false;
            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("d", date);//参数
            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues(@"http://www.easybots.cn/api/holiday.php", "POST", PostVars);//请求地址,传参方式,参数集合
                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);//获取返回值

                string result = Newtonsoft.Json.Linq.JObject.Parse(sRemoteInfo)[date].ToString();
                if (result == "0")
                {
                    isHoliday = false;
                }
                else if (result == "1" || result == "2")
                {
                    isHoliday = true;
                }
            }
            catch
            {
                isHoliday = false;
            }
            return isHoliday;
        }
        /// <summary>
        /// 判断是不是节假日,节假日返回true 
        /// </summary>
        /// <param name="date">日期格式：yyyyMMdd</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, int>> GetWorkDate(DateTime startdate, DateTime enddate)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            for (DateTime date = startdate; date <= enddate; date = date.AddDays(1))
            {
                PostVars.Add("d", date.ToString("yyyyMMdd"));//参数
            }

            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues(@"http://www.easybots.cn/api/holiday.php", "POST", PostVars);//请求地址,传参方式,参数集合
                string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);//获取返回值
                Dictionary<string, int> results = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(sRemoteInfo);
                //List<DateTime> listresults = new List<DateTime>();
                //0为工作日，1休息日，2节假日
                return results.Where(i => i.Value == 0).ToList();
                //string result =Newtonsoft.Json.JsonConvert.DeserializeObject<DW00ClaimInvoice>(sRemoteInfo); Newtonsoft.Json.Linq.JObject.Parse(sRemoteInfo)[date].ToString();
                //if (result == "0")
                //{
                //    isHoliday = false;
                //}
                //else if (result == "1" || result == "2")
                //{
                //    isHoliday = true;
                //}
            }
            catch
            {
                
            }
            return new List<KeyValuePair<string, int>>();
        }
    
    }
}
