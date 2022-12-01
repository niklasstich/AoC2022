using System.Collections;
using AdventOfCode;
using NUnit.Framework;

namespace AdventOfTest;

public class TestDays
{
    [Test]
    [TestCaseSource(typeof(Days))]
    public async Task TestDayPartOne(BaseSolution day)
    {
        Assert.That(await day.Solve_1(), Is.EqualTo(day.PartOneSolution));
    }
    
    [Test]
    [TestCaseSource(typeof(Days))]
    public async Task TestDayPartTwo(BaseSolution day)
    {
        Assert.That(await day.Solve_2(), Is.EqualTo(day.PartTwoSolution));
    }
}

public class Days : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new Day01();
    }
}