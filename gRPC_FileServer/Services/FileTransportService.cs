using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using gRPCFileTransferServer;

namespace gRPC_FileServer.Services
{
    public class FileTransportService : FileService.FileServiceBase
    {
        readonly IWebHostEnvironment _webHostEnvironment;
        public FileTransportService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public override async Task<Empty> FileUpLoad(IAsyncStreamReader<BytesContent> requestStream, ServerCallContext context)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "files");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //The target FileStream to stream the file to.
            FileStream fileStream = null;

            try
            {
                int count = 0;

                //The variable 'chunkSize' is defined for percentage calculation.
                decimal chunkSize = 0;

                // The incoming stream is being read.
                while (await requestStream.MoveNext())
                {
                    // Initial functions to be performed when the stream starts (at the first step) are being executed.
                    if (count++ == 0)
                    {
                        // The file name is determined with the FileName property of the Info object received in the stream.
                        fileStream = new FileStream($"{path}/{requestStream.Current.Info.FileName}{requestStream.Current.Info.FileExtension}", FileMode.CreateNew);

                        // Space is allocated for the upcoming file size. This operation is not mandatory but prevents any other program from filling up the disk during the process.
                        fileStream.SetLength(requestStream.Current.FileSize);
                    }

                    // The buffer is the piece of data for each chunk coming in the stream.
                    var buffer = requestStream.Current.Buffer.ToByteArray();

                    // Writing the chunks coming in the stream to the target FileStream object. Here, with the '0' in the second parameter, it is indicated from which byte in the buffer it will be read and written.
                    await fileStream.WriteAsync(buffer, 0, requestStream.Current.ReadedByte);

                    // Calculating the percentage of the stream transferred.
                    // The number of bytes read (ReadedByte) is added to the chunkSize variable, then multiplied by 100 and divided by the total size.
                    // The final result is rounded to the nearest integer, showing what percentage of the transfer has been completed.
                    Console.WriteLine($"{Math.Round(((chunkSize += requestStream.Current.ReadedByte) * 100) / requestStream.Current.FileSize)}%");
                }
                Console.WriteLine("Uploaded...");

            }
            catch (Exception ex)
            {
                //When the stream is 'CompleteAsync' in the client, a possible error may occur here. Therefore, we control this whole process with try catch.
            }
            await fileStream.DisposeAsync();
            fileStream.Close();
            return new Empty();
        }

        public override async Task FileDownLoad(gRPCFileTransferServer.FileInfo request, IServerStreamWriter<BytesContent> responseStream, ServerCallContext context)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "files");

            // The file information requested to be downloaded by the client is sent. Corresponding file is found and marked as FileStream.
            using FileStream fileStream = new FileStream($"{path}/{request.FileName}{request.FileExtension}", FileMode.Open, FileAccess.Read);

            // Determining the data piece to be sent in each stream.
            byte[] buffer = new byte[2048];

            // Providing information about the file to be sent.
            BytesContent content = new BytesContent
            {
                FileSize = fileStream.Length,
                Info = new gRPCFileTransferServer.FileInfo { FileName = Path.GetFileNameWithoutExtension(fileStream.Name), 
                                                             FileExtension = Path.GetExtension(fileStream.Name) },
                ReadedByte = 0
            };

            // Each buffer is read from the 0th byte up to 2048 bytes, and the result is assigned to 'content.ReadedByte'.
            while ((content.ReadedByte = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                // The read buffer is converted to the 'bytes' type in the 'message.proto' file to be streamed.
                content.Buffer = ByteString.CopyFrom(buffer);
                // 'BytesContent' object is sent as a stream.
                await responseStream.WriteAsync(content);
            }

            fileStream.Close();
        }
    }
}
