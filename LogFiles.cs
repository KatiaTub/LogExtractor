using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace LogExtractor
{
    public class LogFiles
    {
        public List<LogFile> FileList { get; set; } = new List<LogFile>();

        #region Download File  
        public (string fileType, byte[] archiveData, string archiveName) DownloadFiles()
        {
            var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    FileList.ForEach(logfile =>
                    {
                        var oneFile = archive.CreateEntry(logfile.FilePath);
                        using (var streamWriter = new StreamWriter(oneFile.Open()))
                        {
                            streamWriter.Write(File.ReadAllText(logfile.FilePath));
                        }
                    });
                }

                return ("application/zip", memoryStream.ToArray(), zipName);
            }

        }
        #endregion

        #region Download File ById
        public (byte[] data, string fileName) DownloadFile(int id)
        {
            string path = FileList.Find(x => (x.Id == id)).FilePath;

            using (var memoryStream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(File.ReadAllText(path));
                }
                return (memoryStream.ToArray(), Path.GetFileName(path));
            }
        }
        #endregion

    }
}