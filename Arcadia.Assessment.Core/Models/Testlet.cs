using System;
using System.Collections.Generic;
using System.Linq;

namespace Arcadia.Assessment.Core.Models
{
    public class Testlet
    {
        private const int ItemsCountPretest = 4;
        private const int ItemsCountOperational = 6;
        private const int TopPretestItems = 2;

        public string TestletId { get; }

        private List<Item> Items;

        private Random random;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (items.Count(p => p.ItemType == ItemTypeEnum.Pretest) != ItemsCountPretest
                || items.Count(p => p.ItemType == ItemTypeEnum.Operational) != ItemsCountOperational)
            {
                throw new ArgumentException($"{nameof(items)} must contain {ItemsCountPretest} pretest items and {ItemsCountOperational} operational items.");
            }
            Items = items;

            random = new Random(DateTime.Now.Millisecond);
        }

        public List<Item> Randomize()
        {
            // Items private collection has 6 Operational and 4 Pretest Items.
            // Randomize the order of these items as per the requirement(with TDD)
            // The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured &clean readable code.

            var result = new List<Item>();

            var topPretestItems = RandomizeTopPretestItems();
            result.AddRange(topPretestItems);

            var excludeIds = topPretestItems.Select(p => p.ItemId).ToArray();
            var restItems = RandomizeRestItems(excludeIds);
            result.AddRange(restItems);

            return result;
        }

        private List<Item> RandomizeTopPretestItems()
        {
            // Indexes of Pretest items
            var pretestIndexes = Items
                .Select((p, i) => p.ItemType == ItemTypeEnum.Pretest ? i : -1)
                .Where(p => p > -1)
                .ToList();
            
            var result = new List<Item>();

            // Adding {TopPretestItems} random Pretest items to the result
            for (var i = 0; i < TopPretestItems; i++)
            {
                var index = pretestIndexes[random.Next(pretestIndexes.Count)];
                result.Add(Items[index]);
                pretestIndexes.Remove(index);
            }

            return result;
        }

        private List<Item> RandomizeRestItems(IEnumerable<string> excludeIds)
        {
            // Indexes of the rest of the items
            var allIndexes = Items
                .Select((p, i) => !excludeIds.Contains(p.ItemId) ? i : -1)
                .Where(p => p > -1)
                .ToList();
            
            var result = new List<Item>();

            // Adding the rest of the items to the result
            while (allIndexes.Count > 0)
            {
                var index = allIndexes[random.Next(allIndexes.Count)];
                result.Add(Items[index]);
                allIndexes.Remove(index);
            }

            return result;
        }
    }
}
