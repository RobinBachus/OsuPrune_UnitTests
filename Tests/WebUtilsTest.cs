using OsuPrune.Beatmaps;
using static OsuPrune.Beatmaps.CommonMethods;
using static OsuPrune.Common;
using static OsuPrune.WebUtils.DownloadUtils;

namespace OsuPrune_UnitTest.Tests
{
    [TestClass]
    public class WebUtilsTest
    {
        readonly SortedSets MapStorage = new();

        [TestCategory("Downloads")]
        [TestMethod]
        public Task TestDownloadFromURL()
        {
            BeatmapSet set = MapStorage.Mania[5];
            string url = GetDownloadURL(set);
            string path = $@"{OSU_DOWNLOAD_FILE_PATH}\{set.FolderName}.osz";
            Console.WriteLine($"Starting download of {url}\n\tto {path}\n");
            Task? task = null;
            // task = Download(url, path);
            if (task != null)
            {
                task.Wait();
                Console.WriteLine("Done");
            }
            else
            {
                Console.WriteLine("Download task was not run");
            }

            return Task.CompletedTask;
        }

        [TestCategory("Downloads")]
        [TestMethod]
        public async Task TestDownloadPathDoesNotExist()
        {
            BeatmapSet set = MapStorage.Mania[5];
            string url = GetDownloadURL(set);
            string path = $@"{OSU_DOWNLOAD_FILE_PATH}\NoneExistendPath\{set.FolderName}.osz";
            Console.WriteLine($"Starting download of {url}\n\tto {path}\n");

            await Assert.ThrowsExceptionAsync<DirectoryNotFoundException>(async () =>
            {
                try
                {
                    await Download(url, path);
                    Console.WriteLine("Done");
                }
                catch (AggregateException ex)
                {
                    throw ex.Flatten();
                }
            });
        }

        [TestCategory("Downloads")]
        [TestMethod]
        public async Task TestDownloadUrlNotFound()
        {
            BeatmapSet set = MapStorage.Mania[5];
            string url = GetDownloadURL(set) + "5555555";
            string path = $@"{OSU_DOWNLOAD_FILE_PATH}\{set.FolderName}.osz";
            Console.WriteLine($"Starting download of {url}\n\tto {path}\n");

            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                try
                {
                    try
                    {
                        await Download(url, path);
                        Console.WriteLine("Done");
                    }
                    catch (AggregateException ex)
                    {
                        throw ex.Flatten();
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Status: {(ex.StatusCode != null ? ex.StatusCode : "Unavailable")}");
                    Console.WriteLine($"Status code: {(ex.StatusCode != null ? (int)ex.StatusCode : "Unavailable")}");

                    Console.WriteLine($"Message: {ex.Message}");
                    throw ex;
                }
            });
        }
    }
}
