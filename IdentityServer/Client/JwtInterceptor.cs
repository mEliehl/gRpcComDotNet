using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Client
{
    public class JwtInterceptor : Interceptor
    {
        readonly ApiClient _client;

        public JwtInterceptor(ApiClient client)
        {
            _client = client;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var tokenResponse = _client.GetAccessTokenAsync().Result;

            var headers = context.Options.Headers;
            if (headers == null)
            {
                headers = new Metadata();
                var options = context.Options.WithHeaders(headers);
                context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            }
            headers.Add("Authorization", $"{tokenResponse.TokenType} {tokenResponse.AccessToken}");

            return continuation(request, context);
        }
    }
}