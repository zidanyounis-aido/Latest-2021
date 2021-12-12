using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace dms.Controlers.Common
{
    public static class ExtentionMethods
    {
        #region Boolean
        /// <summary>
        ///     check if the T list has valid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValid<T>(this List<T> input)
        {
            if (input.Count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if input is valid.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this int? input)
        {
            if (input != null && input.Value > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if input is valid.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this double? input)
        {
            if (input != null)
                return true;
            return false;
        }
        /// <summary>
        ///     extension method check if two string is equal or not
        /// </summary>
        /// <param name="compareValue"></param>
        /// <param name="secondvalue"></param>
        /// <returns></returns>
        public static bool IsStringEqual(this string compareValue, string secondvalue)
        {
            if (compareValue.Trim().ToLower() == secondvalue.Trim().ToLower())
                return true;

            return false;
        }

        /// <summary>
        /// Returns true if input is valid.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input) && input.Length > 0 && input != "")
                return true;

            return false;
        }



        public static bool IsCharOnly(this string input)
        {
            if (input.IsValid())
            {
                foreach (var charachter in input)
                {
                    if (char.IsDigit(charachter))
                        return false;
                }
                return true;
            }


            return false;
        }
        /// <summary>
        /// Returns true if input is valid Number.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumber(this string input)
        {
            var result = true;
            foreach (var charachter in input)
            {
                if (!char.IsDigit(charachter))
                    result = false;
            }
            return result;
        }
        /// <summary>
        /// Returns true if input is valid.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this DateTime? input)
        {
            var defaultDatetime = new DateTime(1900, 1, 1);
            return input != null && input.Value > defaultDatetime;
        }

        /// <summary>
        /// Returns true if input is valid.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is valid DateTime; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDateTime(this string input)
        {
            if (DateTime.TryParse(input, out var _))
                return true;
            else
                return false;
        }
        public static bool ToBool(this string input)
        {
            try
            {
                if (input.ToLower() == "yes" || input.ToLower() == "true")
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static string BoolToString(this bool input)
        {
            try
            {
                if (input)
                {
                    return "Yes";
                }
                return "No";
            }
            catch (Exception)
            {
                return "No";
            }

        }

        public static string ListIntToString(this List<int> input)
        {
            try
            {
                return input.Select(i => i.ToString(CultureInfo.InvariantCulture))
                    .Aggregate((x1, x2) => x1 + "," + x2);
            }
            catch (Exception e)
            {
                return "";
            }

        }
        /// <summary>
        /// Check string is Email or not
        /// </summary>
        /// <param name="input"></param>
        /// <returns>bool</returns>
        public static bool IsEmail(this string input)
        {
            return new EmailAddressAttribute().IsValid(input);
        }

        public static bool IsValid(this bool? input)
        {
            if (input != null)
                return true;
            return false;
        }

        public static bool HasValue(this string input)
        {
            return !string.IsNullOrEmpty(input);
        }
        #endregion

        #region Double

        public static double MileToKm(this double input)
        {
            return Math.Round(input / 0.62137, 0);
        }
        public static double KmToMile(this double input)
        {
            return Math.Round(input / 1.60934, 0);
        }
        public static int MileToKm(this int input)
        {
            return (int)Math.Round(input / 0.62137, 0);
        }
        public static int KmToMile(this int input)
        {
            return (int)Math.Round(input / 1.60934, 0);
        }

        public static List<int> StringToListInt(this string input)
        {
            try
            {
                return input.Split(',').Select(int.Parse).ToList();

            }
            catch (Exception e)
            {
                return new List<int>();
            }

        }
        public static bool IsDouble(this string input)
        {
            try
            {
                if (double.TryParse(input, out var _))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static double ToDouble(this string input)
        {
            try
            {
                return input.IsDouble() ? Convert.ToDouble(input) : 0;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public static int ToInt(this string input)
        {
            try
            {
                return input.IsNumber() ? Convert.ToInt32(input) : 0;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public static int ToInt(this double input)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch (Exception)
            {
                return 0;
            }

        }
        #endregion

        #region DateTime

        /// <summary>
        /// Returns true if input is valid.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is valid return DateTime; otherwise, <c>null</c>.
        /// </returns>
        public static DateTime? ToDateTime(this string input)
        {
            if (input.IsDateTime())
            {
                DateTime.TryParse(input, out var temp);
                return temp;
            }
            return null;
        }

        #endregion

        public static string HtmlToString(this HtmlTable html)
        {

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            html.RenderControl(hw);
            string str = sw.ToString();
            hw.Close();
            sw.Close();
            return str;
        }

        public static void MessageShowAndRedirect(this Page page, string message, string url)
        {
            string cleanMessage = message.Replace("'", "\'");
            //Page page = System.Web.HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');window.location ='{1}';", cleanMessage, url);
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
            }
        }
        public static void MessageShow(this Page page, string message)
        {
            string cleanMessage = message.Replace("'", "\'");
            //Page page = System.Web.HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("alert('{0}');", cleanMessage);
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
            }
        }



        public static void ConfirmMessage(this Page page, string confirmMessage)
        {
            string cleanMessage = confirmMessage.Replace("'", "\'");
            //Page page = System.Web.HttpContext.Current.CurrentHandler as Page;
            string script = string.Format("confirm('{0}');", cleanMessage);
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("confirm"))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "confirm", script, true /* addScriptTags */);
            }

        }

        #region String

        public static string MaskEmail(this string email)
        {
            try
            {

                if (email.IsEmail())
                {
                    var emailStr = email.Split('@');
                    var emailName = emailStr[0];
                    emailName = string.Format("{0}****{1}", emailName[0],
                        emailName.Substring(email.IndexOf('@') - 1));
                    var companyHost = string.Format("****{0}", emailStr[1].Substring(emailStr[1].IndexOf('.') - 1));
                    return emailName + '@' + companyHost;
                }

            }
            catch (Exception e)
            {

            }
            return email;
        }

        public static string MaskString(this string value)
        {
            try
            {


                if (value.IsValid())
                {

                    string requiredMask = "";
                    var lastDigits = value.Substring(value.Length - 4, 4);
                    if (value.IsDateTime())
                    {
                        requiredMask = "**/**/";
                    }
                    else
                        requiredMask = new string('*', value.Length - lastDigits.Length);

                    value = string.Concat(requiredMask, lastDigits);



                    return value;
                }

            }
            catch (Exception e)
            {

            }
            return value;
        }
        public static bool IsMasked(this string value)
        {
            try
            {
                return value.Contains("*");
            }
            catch (Exception e)
            {
                return false;
            }


        }
        #endregion
    }

}
