// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/fileTransport.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace gRPCFileTransferServer {
  public static partial class FileService
  {
    static readonly string __ServiceName = "file.FileService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCFileTransferServer.FileInfo> __Marshaller_file_FileInfo = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCFileTransferServer.FileInfo.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::gRPCFileTransferServer.BytesContent> __Marshaller_file_BytesContent = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::gRPCFileTransferServer.BytesContent.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCFileTransferServer.FileInfo, global::gRPCFileTransferServer.BytesContent> __Method_FileDownLoad = new grpc::Method<global::gRPCFileTransferServer.FileInfo, global::gRPCFileTransferServer.BytesContent>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "FileDownLoad",
        __Marshaller_file_FileInfo,
        __Marshaller_file_BytesContent);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::gRPCFileTransferServer.BytesContent, global::Google.Protobuf.WellKnownTypes.Empty> __Method_FileUpLoad = new grpc::Method<global::gRPCFileTransferServer.BytesContent, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.ClientStreaming,
        __ServiceName,
        "FileUpLoad",
        __Marshaller_file_BytesContent,
        __Marshaller_google_protobuf_Empty);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::gRPCFileTransferServer.FileTransportReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of FileService</summary>
    [grpc::BindServiceMethod(typeof(FileService), "BindService")]
    public abstract partial class FileServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task FileDownLoad(global::gRPCFileTransferServer.FileInfo request, grpc::IServerStreamWriter<global::gRPCFileTransferServer.BytesContent> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///It returns Empty, so it can be considered as no return value.
      /// </summary>
      /// <param name="requestStream">Used for reading requests from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> FileUpLoad(grpc::IAsyncStreamReader<global::gRPCFileTransferServer.BytesContent> requestStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(FileServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_FileDownLoad, serviceImpl.FileDownLoad)
          .AddMethod(__Method_FileUpLoad, serviceImpl.FileUpLoad).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, FileServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_FileDownLoad, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::gRPCFileTransferServer.FileInfo, global::gRPCFileTransferServer.BytesContent>(serviceImpl.FileDownLoad));
      serviceBinder.AddMethod(__Method_FileUpLoad, serviceImpl == null ? null : new grpc::ClientStreamingServerMethod<global::gRPCFileTransferServer.BytesContent, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.FileUpLoad));
    }

  }
}
#endregion
