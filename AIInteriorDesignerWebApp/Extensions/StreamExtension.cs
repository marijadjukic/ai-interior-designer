namespace AIInteriorDesignerWebApp.Extensions
{
    public static class StreamExtension
    {
        public static byte[] GetByteArrayFromStream(this Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}