using System;
using System.Collections.Generic;
using System.Linq;

namespace Arcadia.Assessment.Core.Models
{
    public class Testlet
    {
        public string TestletId { get; }

        private List<Item> Items;

        private Random random;

        private int TopPretestItems = 2;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;

            random = new Random(DateTime.Now.Millisecond);
        }

        public List<Item> Randomize()
        {
            // Items private collection has 6 Operational and 4 Pretest Items.
            // Randomize the order of these items as per the requirement(with TDD)
            // The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured &clean readable code.

            var result = new List<Item>();

            // Indexes of Pretest items
            var pretestIndexes = Items
                .Select((p, i) => p.ItemType == ItemTypeEnum.Pretest ? i : -1)
                .Where(p => p > -1)
                .ToList();
            // Indexes of All items
            var allIndexes = Items
                .Select((p, i) => i)
                .ToList();
            
            // Adding {TopPretestItems} random Pretest items to the result
            for (var i = 0; i < TopPretestItems; i++)
            {
                var index = pretestIndexes[random.Next(pretestIndexes.Count)];
                result.Add(Items[index]);
                pretestIndexes.Remove(index);
                allIndexes.Remove(index);
            }
            // Adding the rest items to the result
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
