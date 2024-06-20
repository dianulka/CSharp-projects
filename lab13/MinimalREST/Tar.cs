using System.IO.Compression;
using System.Text;

public class Tar
{
    /// <summary>
    /// Extracts a <i>.tar.gz</i> archive to the specified directory.
    /// </summary>
    /// <param name="filename">The <i>.tar.gz</i> to decompress and extract.</param>
    /// <param name="outputDir">Output directory to write the files.</param>
    public static void ExtractTarGz(string filename, string outputDir)
    {
        using var stream = File.OpenRead(filename);
        ExtractTarGz(stream, outputDir);
    }

    /// <summary>
    /// Extracts a <i>.tar.gz</i> archive stream to the specified directory.
    /// </summary>
    /// <param name="stream">The <i>.tar.gz</i> to decompress and extract.</param>
    /// <param name="outputDir">Output directory to write the files.</param>
    public static void ExtractTarGz(Stream stream, string outputDir)
    {
        int read;
        const int chunk = 4096;
        var buffer = new byte[chunk];

        // A GZipStream is not seekable, so copy it first to a MemoryStream
        using var gzip = new GZipStream(stream, CompressionMode.Decompress);
        using var memStr = new MemoryStream();
        
        //For .NET 6+
        while ((read = gzip.Read(buffer, 0, buffer.Length)) > 0)
        {
            memStr.Write(buffer, 0, read);
        }

        //For .NET versions < 6
        /*
        do
        {
            read = gzip.Read(buffer, 0, chunk);
            memStr.Write(buffer, 0, read);
        } while (read == chunk);*/

        gzip.Close();
        memStr.Seek(0, SeekOrigin.Begin);
        ExtractTar(memStr, outputDir);
    }

    /// <summary>
    /// Extracts a <c>tar</c> archive to the specified directory.
    /// </summary>
    /// <param name="filename">The <i>.tar</i> to extract.</param>
    /// <param name="outputDir">Output directory to write the files.</param>
    public static void ExtractTar(string filename, string outputDir)
    {
        using var stream = File.OpenRead(filename);
        ExtractTar(stream, outputDir);
    }

    /// <summary>
    /// Extracts a <c>tar</c> archive to the specified directory.
    /// </summary>
    /// <param name="stream">The <i>.tar</i> to extract.</param>
    /// <param name="outputDir">Output directory to write the files.</param>
    public static void ExtractTar(Stream stream, string outputDir)
    {
        var buffer = new byte[100];
        var longFileName = string.Empty;
        while (true)
        {
            stream.Read(buffer, 0, 100);
            var name = string.IsNullOrEmpty(longFileName) ? Encoding.ASCII.GetString(buffer).Trim('\0') : longFileName; //Use longFileName if we have one read
            if (string.IsNullOrWhiteSpace(name))
                break;
            stream.Seek(24, SeekOrigin.Current);
            stream.Read(buffer, 0, 12);
            var size = Convert.ToInt64(Encoding.UTF8.GetString(buffer, 0, 12).Trim('\0').Trim(), 8);
            stream.Seek(20, SeekOrigin.Current); //Move head to typeTag byte
            var typeTag = stream.ReadByte();
            stream.Seek(355L, SeekOrigin.Current); //Move head to beginning of data (byte 512)

	    if (typeTag == 'L')
            {
                //If Type Tag is 'L' we have a filename that is longer than the 100 bytes reserved for it in the header.
                //We read it here and save it temporarily as it will be the file name of the next block where the actual data is
                var buf = new byte[size];
                stream.Read(buf, 0, buf.Length);
                longFileName = Encoding.ASCII.GetString(buf).Trim('\0');
	    }
            else
            {
                //We have a normal file or directory
                longFileName = string.Empty; //Reset longFileName if current entry is not indicating one
		var output = Path.Combine(outputDir, name);
		if (!Directory.Exists(Path.GetDirectoryName(output)))
			Directory.CreateDirectory(Path.GetDirectoryName(output));

		if (!name.EndsWith("/")) //Directories are zero size and don't need anything written
		{
			using var str = File.Open(output, FileMode.OpenOrCreate, FileAccess.Write);
			var buf = new byte[size];
			stream.Read(buf, 0, buf.Length);
			str.Write(buf, 0, buf.Length);
		}
	    }			

            //Move head to next 512 byte block 
            var pos = stream.Position;
            var offset = 512 - (pos % 512);
            if (offset == 512)
                offset = 0;

            stream.Seek(offset, SeekOrigin.Current);
        }
    }
}