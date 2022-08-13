using System;
using System.Collections.Generic;
using Arcadia.Assessment.Core.Models;

namespace Arcadia.Assessment.Tests;

public class UnitTest_Testlet
{
    [Fact]
    public void Test1()
    {
        var testItems = new List<Item>();
        testItems.AddRange(GenerateTestItems(4, ItemTypeEnum.Pretest));
        testItems.AddRange(GenerateTestItems(6, ItemTypeEnum.Operational));

        var testlet = new Testlet(DateTime.Now.ToString("yyyyMMdd-HHmmss"), testItems);

        var actual = testlet.Randomize();

        Assert.Equal(ItemTypeEnum.Pretest, actual[0].ItemType);
        Assert.Equal(ItemTypeEnum.Pretest, actual[1].ItemType);
    }

    private IEnumerable<Item> GenerateTestItems(int count, ItemTypeEnum itemType)
    {
        for (var i = 0; i < count; i++)
        {
            yield return new Item { ItemId = $"{itemType}_{(i + 1)}", ItemType = itemType };
        }
    }
}