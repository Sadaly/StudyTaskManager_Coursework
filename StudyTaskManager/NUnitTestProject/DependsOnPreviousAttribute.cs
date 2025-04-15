using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

public class DependsOnPreviousAttribute : Attribute, ITestAction
{
    private static readonly Dictionary<string, ResultState> TestResults = new Dictionary<string, ResultState>();

    public void BeforeTest(ITest test)
    {
        // Получаем имя текущего теста
        var currentTestName = test.FullName;

        // Находим предыдущий тест в порядке Order
        var previousTestName = GetPreviousTestName(test);

        if (previousTestName != null && TestResults.TryGetValue(previousTestName, out var previousResult))
        {
            if (previousResult != ResultState.Success)
            {
                Assert.Inconclusive($"Предыдущий тест {previousTestName} не прошёл! Статус: {previousResult}");
            }
        }
    }

    public void AfterTest(ITest test)
    {
        // Сохраняем результат теста после его выполнения
        TestResults[test.FullName] = TestContext.CurrentContext.Result.Outcome;
    }

    private string GetPreviousTestName(ITest test)
    {
        // Получаем Order текущего теста
        var currentOrder = GetTestOrder(test);
        if (currentOrder == -1) return null;

        // Получаем все тесты из фикстуры
        var fixtureTests = GetTestsFromFixture(test.Parent);

        // Ищем тест с Order = currentOrder - 1
        foreach (var fixtureTest in fixtureTests)
        {
            var order = GetTestOrder(fixtureTest);
            if (order == currentOrder - 1)
                return fixtureTest.FullName;
        }

        return null;
    }

    private IEnumerable<ITest> GetTestsFromFixture(ITest fixture)
    {
        // NUnit хранит тесты в виде дерева, нужно получить все листья (сами тесты)
        var tests = new List<ITest>();
        CollectTests(fixture, tests);
        return tests;
    }

    private void CollectTests(ITest node, List<ITest> tests)
    {
        if (node.IsSuite)
        {
            foreach (var child in node.Tests)
            {
                CollectTests(child, tests);
            }
        }
        else
        {
            tests.Add(node);
        }
    }

    private int GetTestOrder(ITest test)
    {
        // Получаем атрибуты OrderAttribute из метода теста
        var method = GetTestMethodInfo(test);
        if (method == null) return -1;

        var orderAttr = method.GetCustomAttributes(typeof(OrderAttribute), false).FirstOrDefault() as OrderAttribute;
        return orderAttr?.Order ?? -1;
    }

    private System.Reflection.MethodInfo GetTestMethodInfo(ITest test)
    {
        // Получаем MethodInfo из ITest
        if (test.Method == null) return null;
        return test.Method.MethodInfo;
    }

    public ActionTargets Targets => ActionTargets.Test;
}