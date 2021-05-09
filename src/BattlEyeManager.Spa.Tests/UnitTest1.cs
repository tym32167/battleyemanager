using BattlEyeManager.Spa.Infrastructure.Utils;
using BattlEyeManager.Spa.Model;
using NUnit.Framework;
using System;
using System.Linq;

namespace BattlEyeManager.Spa.Tests
{
    [TestFixture]
    public class PlayerSessionModelExtensionsTests
    {
        [Test]
        public void Combine_Success()
        {
            var now = DateTime.UtcNow;

            var prev = new PlayerSessionModel()
            {
                StartDate = now,
                EndDate = now.AddMinutes(10)

            };

            var next = new PlayerSessionModel()
            {
                StartDate = prev.EndDate.Value.AddMinutes(5),
                EndDate = prev.EndDate.Value.AddMinutes(50),
            };


            var expected = new[] { prev, next }.Consolidate().ToList();

            Assert.AreEqual(1, expected.Count);

            var item = expected.Single();

            Assert.AreEqual(item.StartDate, prev.StartDate);
            Assert.AreEqual(item.EndDate, next.EndDate);
        }

        [Test]
        public void Not_Combine_Success()
        {
            var now = DateTime.UtcNow;

            var prev = new PlayerSessionModel()
            {
                StartDate = now,
                EndDate = now.AddMinutes(10)

            };

            var next = new PlayerSessionModel()
            {
                StartDate = prev.EndDate.Value.AddMinutes(50),
                EndDate = prev.EndDate.Value.AddMinutes(50),
            };


            var expected = new[] { prev, next }.Consolidate().ToList();

            Assert.AreEqual(2, expected.Count);
        }
    }
}