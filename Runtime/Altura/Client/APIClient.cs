using System;
using System.Collections.Generic;
using System.Globalization;


namespace AlturaSdk.Api.Client
{
    /// <summary>
    /// API client is mainly responible for making the HTTP call to the API backend.
    /// </summary>
    public class ApiClient
    {
        private readonly Dictionary<String, String> _defaultHeaderMap = new Dictionary<String, String>();
  
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient" /> class.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        public ApiClient(String basePath = "http://localhost:6000")
        {
            BasePath = basePath;
            RestClient = new RestClient(BasePath);
        }
  
        /// <summary>
        /// Gets or sets the base path.
        /// </summary>
        /// <value>The base path</value>
        public String BasePath { get; set; }
  
        /// <summary>
        /// Gets or sets the RestClient.
        /// </summary>
        /// <value>An instance of the RestClient</value>
        public RestClient RestClient { get; set; }
  
        /// <summary>
        /// Gets the default header.
        /// </summary>
        public Dictionary<String, String> DefaultHeader
        {
            get { return _defaultHeaderMap; }
        }
  
        /// <summary>
        /// Gets the default API client.
        /// </summary>
        /// <value>The default API client.</value>
        public static ApiClient Default;
  
        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use DefaultHeader instead.")]
        public Dictionary<String, String> DefaultHeader()
        {
            return DefaultHeader;
        }
  
        /// <summary>
        /// Gets the default API client.
        /// </summary>
        /// <value>The default API client.</value>
        public static ApiClient DefaultApiClient
        {
            get { return Default; }
        }
  
        /// <summary>
        /// Sets the default API client.
        /// </summary>
        /// <value>The default API client.</value>
        [Ob
}