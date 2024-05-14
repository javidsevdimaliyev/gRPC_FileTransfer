using Grpc.Net.Client;
using gRPCFileTransferDownloadsClient;

var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new FileService.FileServiceClient(channel);

// The directory where the file will be downloaded is defined.
string downloadPath = @"C:\Users\cavids\Desktop\Developing\5 Implementations of Technologies\gRPC (Remote Procedure Calls)\gRPC_FileServer\gRPC_DownloadClient\DownloadedFiles";

// The file information requested from the server is defined as 'FileInfo'.
var fileInfo = new gRPCFileTransferDownloadsClient.FileInfo
{
    FileExtension = ".mp4",
    FileName = "testVideo"
};

FileStream fileStream = null;

// Request is made with the relevant 'FileInfo' from the server.
var request = client.FileDownLoad(fileInfo);

CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

int count = 0;
decimal chunkSize = 0;

// File chunks coming as a stream in response to the request are started to be read.
while (await request.ResponseStream.MoveNext(cancellationTokenSource.Token))
{
    // The basic details of the transferred file are determined in the first chunk received.
    if (count++ == 0)
    {
        // Configuration is performed to store the transferred file in the specified directory according to the information received from the server.
        fileStream = new FileStream(@$"{downloadPath}\{request.ResponseStream.Current.Info.FileName}{request.ResponseStream.Current.Info.FileExtension}", FileMode.CreateNew);

        // Space is allocated in the storage location for the file size.
        fileStream.SetLength(request.ResponseStream.Current.FileSize);
    }

    // The buffers coming in 'ByteString' type as specified in the 'message.proto' file are converted to byte arrays.
    var buffer = request.ResponseStream.Current.Buffer.ToByteArray();

    // The relevant FileStream is written with the chunks.
    await fileStream.WriteAsync(buffer, 0, request.ResponseStream.Current.ReadedByte);

    Console.WriteLine($"{Math.Round(((chunkSize += request.ResponseStream.Current.ReadedByte) * 100) / request.ResponseStream.Current.FileSize)}%");
}
Console.WriteLine("Downloaded...");

await fileStream.DisposeAsync();
fileStream.Close();
