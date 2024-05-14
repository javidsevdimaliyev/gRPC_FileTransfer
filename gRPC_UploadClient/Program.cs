using Google.Protobuf;
using Grpc.Net.Client;
using gRPCFileTransferClient;

var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new FileService.FileServiceClient(channel);

string file = @"C:\Users\cavids\Desktop\test\testVideo.mp4";

// Determining the file to be streamed.
using FileStream fileStream = new FileStream(file, FileMode.Open);

// Getting all the information of the file. This object will be sent along with the stream.
var content = new BytesContent
{
    FileSize = fileStream.Length,
    ReadedByte = 0,
    Info = new gRPCFileTransferClient.FileInfo
    {
        FileName = Path.GetFileNameWithoutExtension(fileStream.Name),
        FileExtension = Path.GetExtension(fileStream.Name)
    },
};

// Calling the FileUpload function on the server for the stream.
var upload = client.FileUpLoad();

// Pre-setting how many pieces will go in the stream. Here, a 2048-sized area is allocated. Since at most 2048 bytes piece can be sent regardless of the size of the file, it is set like this.
byte[] buffer = new byte[2048];

// Each buffer is read from the 0th byte up to 2048 bytes, and the result is assigned to 'content.ReadedByte'.
while ((content.ReadedByte = fileStream.Read(buffer, 0, buffer.Length)) > 0)
{
    // Converting the read buffer to the 'bytes' type in the 'message.proto' file to be streamed.
    content.Buffer = ByteString.CopyFrom(buffer);
    // 'BytesContent' object is sent as a stream.
    await upload.RequestStream.WriteAsync(content);
}

await upload.RequestStream.CompleteAsync();

fileStream.Close();