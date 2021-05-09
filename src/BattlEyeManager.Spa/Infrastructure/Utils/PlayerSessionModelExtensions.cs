using BattlEyeManager.Spa.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattlEyeManager.Spa.Infrastructure.Utils
{
    public static class PlayerSessionModelExtensions
    {
        public static IEnumerable<PlayerSessionModel> Consolidate(this IEnumerable<PlayerSessionModel> input)
        {
            var sorted = input.OrderBy(x => x.StartDate);
            var ll = new LinkedList<PlayerSessionModel>();

            foreach (var next in sorted)
            {
                if (ll.Count == 0)
                {
                    ll.AddLast(next);
                    continue;
                }

                var prev = ll.Last.Value;

                if (prev.PlayerId != next.PlayerId || prev.ServerId != next.ServerId || prev.Name != next.Name || prev.IP != next.IP || prev.EndDate == null)
                {
                    ll.AddLast(next);
                    continue;
                }

                var diff = Math.Abs((next.StartDate - prev.EndDate.Value).TotalMinutes);

                if (diff > 15)
                {
                    ll.AddLast(next);
                    continue;
                }

                var newValue = new PlayerSessionModel()
                {
                    Id = prev.Id,
                    Name = prev.Name,
                    IP = prev.IP,
                    PlayerId = prev.PlayerId,
                    ServerId = prev.ServerId,
                    StartDate = prev.StartDate,
                    EndDate = next.EndDate
                };

                ll.RemoveLast();
                ll.AddLast(newValue);
            }

            return ll;
        }
    }
}
