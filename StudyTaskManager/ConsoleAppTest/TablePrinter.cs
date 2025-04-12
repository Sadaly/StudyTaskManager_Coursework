using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppTest;

public static class TablePrinter
{
    private static int[] _columnWidths = null!;
    private static int _tableWidth;
    private static int _columnsCount;

    // Цвета элементов таблицы
    private static readonly ConsoleColor BorderColor = ConsoleColor.DarkGray;
    private static readonly ConsoleColor TitleColor = ConsoleColor.White;
    private static readonly ConsoleColor HeaderColor = ConsoleColor.White;
    private static readonly ConsoleColor ContentColor = ConsoleColor.White;
    private static readonly ConsoleColor FooterColor = ConsoleColor.White;

    /// <summary>
    /// Выводит в консоль таблицу.
    /// </summary>
    public static void PrintTable(string tableTitle, string[] columnNames, List<string[]> rows)
    {
        InitializeTableParameters(columnNames, rows);

        //if (rows == null || rows.Count == 0)
        //{
        //    PrintTableTitle(tableTitle);
        //    PrintTitleSeparator();
        //    PrintRow(columnNames, isHeader: true);
        //    PrintFooterSeparator();
        //    PrintRecordsCount(0);
        //    PrintBottomLine();
        //    return;
        //}

        PrintTableTitle(tableTitle);
        PrintTitleSeparator();
        PrintRow(columnNames, isHeader: true);
        PrintHeaderSeparator();
        foreach (var row in rows)
        {
            PrintRow(row);
        }
        PrintFooterSeparator();
        PrintRecordsCount(rows.Count);
        PrintBottomLine();
    }

    private static void InitializeTableParameters(string[] columnNames, List<string[]> rows)
    {
        _columnsCount = columnNames.Length;
        _columnWidths = new int[_columnsCount];

        var allRows = new List<string[]> { columnNames };
        if (rows != null && rows.Count > 0)
            allRows.AddRange(rows);

        for (int i = 0; i < _columnsCount; i++)
        {
            _columnWidths[i] = allRows.Max(row => i < row.Length ? (row[i]?.Length ?? 0) : 0);
        }

        _tableWidth = _columnWidths.Sum() + (_columnsCount * 3) - 1;
    }

    private static void PrintTableTitle(string title)
    {
        string formattedTitle = title.Length > _tableWidth - 2
            ? title.Substring(0, _tableWidth - 2)
            : title;

        int totalPadding = _tableWidth - formattedTitle.Length;
        int leftPadding = totalPadding / 2;
        int rightPadding = totalPadding - leftPadding;

        Console.ForegroundColor = BorderColor;
        Console.Write("┌");
        Console.Write(new string('─', leftPadding));

        Console.ForegroundColor = TitleColor;
        Console.Write(formattedTitle);

        Console.ForegroundColor = BorderColor;
        Console.Write(new string('─', rightPadding));
        Console.WriteLine("┐");
        Console.ResetColor();
    }

    private static void PrintTitleSeparator()
    {
        Console.ForegroundColor = BorderColor;
        Console.Write("├");
        for (int i = 0; i < _columnsCount; i++)
        {
            Console.Write(new string('─', _columnWidths[i] + 2));
            if (i < _columnsCount - 1)
                Console.Write("┬");
        }
        Console.WriteLine("┤");
        Console.ResetColor();
    }

    private static void PrintHeaderSeparator()
    {
        Console.ForegroundColor = BorderColor;
        Console.Write("├");
        for (int i = 0; i < _columnsCount; i++)
        {
            Console.Write(new string('─', _columnWidths[i] + 2));
            if (i < _columnsCount - 1)
                Console.Write("┼");
        }
        Console.WriteLine("┤");
        Console.ResetColor();
    }

    private static void PrintRow(string[] row, bool isHeader = false)
    {
        Console.ForegroundColor = BorderColor;
        Console.Write("│");

        for (int i = 0; i < _columnsCount; i++)
        {
            string cell = i < row.Length ? row[i] ?? "" : "";

            Console.ForegroundColor = isHeader ? HeaderColor : ContentColor;
            Console.Write($" {cell.PadRight(_columnWidths[i])}");

            Console.ForegroundColor = BorderColor;
            Console.Write(" │");
        }

        Console.WriteLine();
        Console.ResetColor();
    }

    private static void PrintFooterSeparator()
    {
        Console.ForegroundColor = BorderColor;
        Console.Write("├");
        for (int i = 0; i < _columnsCount; i++)
        {
            Console.Write(new string('─', _columnWidths[i] + 2));
            if (i < _columnsCount - 1)
                Console.Write("┴");
        }
        Console.WriteLine("┤");
        Console.ResetColor();
    }

    private static void PrintRecordsCount(int count)
    {
        Console.ForegroundColor = BorderColor;
        Console.Write("│ ");

        Console.ForegroundColor = FooterColor;
        Console.Write($"Всего записей: {count}".PadRight(_tableWidth - 2));

        Console.ForegroundColor = BorderColor;
        Console.WriteLine(" │");
        Console.ResetColor();
    }

    private static void PrintBottomLine()
    {
        Console.ForegroundColor = BorderColor;
        Console.Write("└");
        Console.Write(new string('─', _tableWidth));
        Console.WriteLine("┘");
        Console.ResetColor();
    }
}