using System;

namespace dms.DTOS
{
    public class Response<Type>
    {
        public Type Result { get; set; }
        public string Description { get; set; }
        public string ExceptionMessage { get; set; }
        public bool State { get; set; }

        /// <summary>
        /// Valids the specified result object.
        /// </summary>
        /// <typeparam name="Type">The type of the ype.</typeparam>
        /// <param name="resultObject">The result object.</param>
        /// <returns></returns>
        public static Response<Type> Valid<Type>(Type resultObject)
        {
            return new Response<Type>
            {
                Result = resultObject,
                Description = DescriptionDTO.Succeeded,
                State = true
            };
        }

        public static Response<Type> Valid<Type>(Type resultObject, string Message)
        {
            return new Response<Type>
            {
                Result = resultObject,
                Description = Message,
                State = true
            };
        }


        /// <summary>
        /// Nots the found.
        /// </summary>
        /// <returns></returns>
        public static Response<Type> NotFound()
        {
            return new Response<Type>
            {
                Result = default,
                Description = DescriptionDTO.NotFound,
                State = false
            };
        }


        public static Response<Type> NotFound(Type resultObject)
        {
            return new Response<Type>
            {
                Result = resultObject,
                Description = DescriptionDTO.NotFound,
                State = false
            };
        }
        /// <summary>
        /// Nots the found.
        /// </summary>
        /// <returns></returns>
        public static Response<Type> NotFound(string message)
        {
            return new Response<Type>
            {
                Result = default,
                Description = message,
                State = true
            };
        }
        /// <summary>
        /// Faileds the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static Response<Type> Failed(Exception exception)
        {
            string exp = exception.Message + (exception.InnerException == null ? "" : " InnerException: " + exception.InnerException.ToString());
            return new Response<Type>
            {
                Result = default,
                Description = "الرجاء التواصل مع الدعم",
                ExceptionMessage = exp,
                State = false
            };
        }


        /// <summary>
        /// Faileds the specified failed message.
        /// </summary>
        /// <param name="failedMessage">The failed message.</param>
        /// <returns></returns>
        public static Response<Type> Failed(string failedMessage)
        {
            return new Response<Type>
            {
                Result = default,
                Description = failedMessage,
                ExceptionMessage = "",
                State = false
            };
        }
        public static Response<Type> Failed(string failedMessage, string exceptionMessage)
        {
            return new Response<Type>
            {
                Result = default,
                Description = failedMessage,
                ExceptionMessage = exceptionMessage,
                State = false
            };
        }
        public static Response<Type> InvalidRequest()
        {
            return new Response<Type>
            {
                Result = default,
                Description = DescriptionDTO.InvalidRequest,
                State = false
            };
        }
    }

}