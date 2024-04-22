using Grpc.Core;
using Grpc.Core.Interceptors;
using Opc.Ua;
using StatusCode = Grpc.Core.StatusCode;
// ReSharper disable InconsistentNaming

namespace ViCellBluOpcUaModelDesign.OpcUa
{
    public class OpcExceptionInterceptor : Interceptor
    {
        public const string ERROR_UNEXPECTED = "Unexpected error";
        public const string ERROR_PERMISSION_DENIED = "Permission denied";
        public const string ERROR_NOT_ALLOWED = "Unable to perform action";

        private static ServiceResultException TransformException(RpcException e)
        {
            var opcStatusCode = StatusCodes.BadUnexpectedError;
            var msg = ERROR_UNEXPECTED;
            switch (e.StatusCode)
            {
                case StatusCode.PermissionDenied:
                    opcStatusCode = StatusCodes.BadRequestNotAllowed;
                    msg = ERROR_PERMISSION_DENIED;
                    if (!string.IsNullOrEmpty(e.Status.Detail))
                        msg += $": {e.Status.Detail}";
                    break;
                case StatusCode.FailedPrecondition:
                    opcStatusCode = StatusCodes.BadRequestNotAllowed;
                    msg = string.IsNullOrEmpty(e.Status.Detail) ? ERROR_NOT_ALLOWED : e.Status.Detail;
                    break;
                default:
                    break;
            }

            return new ServiceResultException(opcStatusCode, msg);
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                return base.BlockingUnaryCall(request, context, continuation);
            }
            catch (RpcException e)
            {
                throw TransformException(e);
            }
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                return base.AsyncUnaryCall(request, context, continuation);
            }
            catch (RpcException e)
            {
                throw TransformException(e);
            }
        }

        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                return base.AsyncServerStreamingCall(request, context, continuation);
            }
            catch (RpcException e)
            {
                throw TransformException(e);
            }
        }

        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context,
            AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                return base.AsyncClientStreamingCall(context, continuation);
            }
            catch (RpcException e)
            {
                throw TransformException(e);
            }
        }

        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context,
            AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            try
            {
                return base.AsyncDuplexStreamingCall(context, continuation);
            }
            catch (RpcException e)
            {
                throw TransformException(e);
            }
        }
    }
}