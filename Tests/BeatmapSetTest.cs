using OsuPrune.Beatmaps;
using static OsuPrune.Beatmaps.CommonMethods;
using static OsuPrune.Common;
using static OsuPrune.WebUtils.DownloadUtils;

namespace OsuPrune_UnitTest.Tests
{
    [TestClass]
    public class BeatmapSetTest
    {
        readonly SortedSets MapStorage = new();

        [TestCategory("ListCounts")]
        [TestMethod]
        public void TestPlayedSetCount()
        {
            Console.WriteLine($"Played sets: {MapStorage.PlayedSets.Count}");
            Assert.AreEqual(468, MapStorage.PlayedSets.Count);
        }

        [TestCategory("ListCounts")]
        [TestMethod]
        public void TestStandardMapCount()
        {
            Console.WriteLine($"Standard sets: {MapStorage.Standard.Count}");
            Assert.AreEqual(351, MapStorage.Standard.Count);
        }

        [TestCategory("ListCounts")]
        [TestMethod]
        public void TestTaikoMapCount()
        {
            Console.WriteLine($"Taiko sets: {MapStorage.Taiko.Count}");
            Assert.AreEqual(9, MapStorage.Taiko.Count);
        }

        [TestCategory("ListCounts")]
        [TestMethod]
        public void TestCatchMapCount()
        {
            Console.WriteLine($"Catch sets: {MapStorage.CatchTheBeat.Count}");
            Assert.AreEqual(7, MapStorage.CatchTheBeat.Count);
        }

        [TestCategory("ListCounts")]
        [TestMethod]
        public void TestManiaMapCount()
        {
            Console.WriteLine($"Mania sets: {MapStorage.Mania.Count}\n");
            Assert.AreEqual(356, MapStorage.Mania.Count);
        }

        [TestCategory("ListCounts")]
        [TestMethod]
        public void TestMultipleModesMapCount()
        {
            Console.WriteLine($"Multiple game mode sets: {MapStorage.MultipleModes.Count}\n");
            Assert.AreEqual(49, MapStorage.MultipleModes.Count);
        }

        [TestCategory("ListCounts")]
        [TestMethod]
        public void TestTotalSetCount()
        {
            int total = MapStorage.Standard.Count;
            total += MapStorage.Taiko.Count;
            total += MapStorage.CatchTheBeat.Count;
            total += MapStorage.Mania.Count;
            total += MapStorage.MultipleModes.Count;
            Console.WriteLine($"Total maps: {MapStorage.BeatmapSets.Count} ({total})\n");

            Assert.AreEqual(MapStorage.BeatmapSets.Count, total);
        }

        [TestCategory("ListCounts")]
        [TestMethod]
        public void TestTotalMapCount()
        {
            int total = 0;
            MapStorage.Standard.ForEach(set => total += set.Beatmaps.Count);
            MapStorage.Taiko.ForEach(set => total += set.Beatmaps.Count);
            MapStorage.CatchTheBeat.ForEach(set => total += set.Beatmaps.Count);
            MapStorage.Mania.ForEach(set => total += set.Beatmaps.Count);
            MapStorage.MultipleModes.ForEach(set => total += set.Beatmaps.Count);

            Console.WriteLine($"Total maps: {OsuFile.Beatmaps.Count} ({total})\n");
            Assert.AreEqual(OsuFile.Beatmaps.Count, total);
        }

        [TestCategory("DataCheck")]
        [TestMethod]
        public void TestSetsToString()
        {
            foreach (BeatmapSet set in MapStorage.BeatmapSets)
            {
                Console.WriteLine(set.ToString());
            }
        }
    }
}