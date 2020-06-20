using System;
using System.IO;

namespace VideoGrabber.Models
{
    public class MediaFile
    {
        public MediaFile(MemoryStream stream, string fileName, string extension)
        {
            Content = stream.ToArray();
            Name = GetNameWithExtension(fileName, extension);
        }

        public string Name { get; }
        public byte[] Content { get; }

        private string GetNameWithExtension(string fileName, string extension)
        {
            if (string.IsNullOrWhiteSpace(fileName) ||
                string.IsNullOrWhiteSpace(extension))
                return fileName;

            var isEndsWithExtension = fileName.EndsWith(extension, StringComparison.CurrentCultureIgnoreCase);
            if (isEndsWithExtension)
            {
                return fileName;
            }

            return $"{fileName}.{extension}";
        }
    }
}