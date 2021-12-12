using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.DAL
{
    public class Feedbacks
    {
        DataProccess dp = new DataProccess();
        public Feedbacks()
        {
            dp.catchingError += new DataProccess.catchingErrorHandle(catchingDataProccessError);
        }

        public void catchingDataProccessError(String Method, Exception e)
        {
            //get DataProccess Error and add it to logs
            catchingError(Method, e);
        }
        public delegate void catchingErrorHandle(String Method, Exception e);
        public event catchingErrorHandle catchingError;

        public int Save(String UserID, string FeedbackMessage, string PageURL)
        {
            int FeedbackID = 0;
            dp.parameters.Clear();
            dp.parameters.Add("@UserID", UserID);
            dp.parameters.Add("@FeedbackMessage", FeedbackMessage);
            dp.parameters.Add("@PageURL", PageURL);
            dp.parameters.Add("@CDate", DateTime.Now);
            FeedbackID = int.Parse(dp.executeScalar("InsertFeedbacks").ToString());
            return FeedbackID;
        }
    }
}