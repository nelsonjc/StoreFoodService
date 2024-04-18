namespace ShopFood.Domain.DTOs.Results
{
    /// <summary>
    /// Http Generic Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResponse<T>
    {
        #region Properties
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="response"></param>
        public HttpResponse(int statusCode, string message, T response)
        {
            StatusCode = statusCode;
            Message = message;
            Response = response;
        }

        #endregion Constructor
    }
}
