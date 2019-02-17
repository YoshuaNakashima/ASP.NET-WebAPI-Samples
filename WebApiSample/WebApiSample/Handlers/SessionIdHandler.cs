using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiSample.Repository;

/// <summary>
/// 
/// </summary>
public class SessionIdHandler : DelegatingHandler
{
    /// <summary>
    /// 
    /// </summary>
    public static string SessionIdToken = "session-id";

    IUserRepository UserRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userRepository"></param>
    public SessionIdHandler(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    async protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        string sessionId;

        // Try to get the session ID from the request; otherwise create a new ID.
        //var cookie = request.Headers.GetCookies(SessionIdToken).FirstOrDefault();
        var queries = request.GetQueryNameValuePairs().Where((kv) => kv.Key == "sessionId");
        var cookie = request.Headers.GetCookies(SessionIdToken).FirstOrDefault();
        if (queries == null || queries.Count() != 1)
        {
            sessionId = Guid.NewGuid().ToString();
        }
        else
        {
            //sessionId = cookie[SessionIdToken].Value;
            sessionId = queries.Single().Value;
            try
            {
                Guid guid = Guid.Parse(sessionId);
            }
            catch (FormatException)
            {
                // Bad session ID. Create a new one.
                sessionId = Guid.NewGuid().ToString();
            }
        }

        // Store the session ID in the request property bag.
        //request.Properties[SessionIdToken] = sessionId;

        // Continue processing the HTTP request.
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        // Set the session ID as a cookie in the response message.
        response.Headers.AddCookies(new CookieHeaderValue[] {
            new CookieHeaderValue(SessionIdToken, sessionId)
        });

        return response;
    }
}