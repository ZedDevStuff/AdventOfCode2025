using System;

namespace AdventOfCode2025.Solutions.Misc;

internal class Grid<T>
{
    private T[] _backingArray;
    public readonly int Width;
    public readonly int Height;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        _backingArray = new T[width * height];
    }

    public int IndexOf(T value)
    {
        return _backingArray.IndexOf(value);
    }
    public int IndexOfInRow(int row, T value)
    {
        for (int x = 0; x < Width; x++)
        {
            if (this[x, row]?.Equals(value) ?? false)
                return x;
        }
        return -1;
    }
    public long IndexOfInColumn(int column, T value)
    {
        for (int y = 0; y < Height; y++)
        {
            if (this[column, y]?.Equals(value) ?? false)
                return y;
        }
        return -1;
    }

    public T[] GetRow(int y)
    {
        T[] row = new T[Width];
        Array.Copy(_backingArray, y * Width, row, 0, Width);
        return row;
    }
    public Memory<T> GetRowMemory(int y)
    {
        return new Memory<T>(_backingArray, y * Width, Width);
    }
    public void SetRow(int y, T[] row)
    {
        if (row.Length != Width)
            throw new ArgumentException("Row length does not match grid width.", nameof(row));
        Array.Copy(row, 0, _backingArray, y * Width, Width);
    }
    public T[] GetColumn(int x)
    {
        T[] column = new T[Height];
        for (int y = 0; y < Height; y++)
        {
            column[y] = _backingArray[y * Width + x];
        }
        return column;
    }
    public Memory<T> GetColumnMemory(int x)
    {
        return new Memory<T>(GetColumn(x));
    }
    public void SetColumn(int x, T[] column)
    {
        if (column.Length != Height)
            throw new ArgumentException("Column length does not match grid height.", nameof(column));
        for (int y = 0; y < Height; y++)
        {
            _backingArray[y * Width + x] = column[y];
        }
    }

    public T[][] AsRows()
    {
        T[][] rows = new T[Height][];
        for (int y = 0; y < Height; y++)
        {
            rows[y] = GetRow(y);
        }
        return rows;
    }
    public T[][] AsColumns()
    {
        T[][] columns = new T[Width][];
        for (int x = 0; x < Width; x++)
        {
            columns[x] = GetColumn(x);
        }
        return columns;
    }

    public T this[int x, int y]
    {
        get => _backingArray[y * Width + x];
        set => _backingArray[y * Width + x] = value;
    }

    public void PrintGridCompact(Func<T, char> toChar)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Console.Write(toChar(this[x, y]));
            }
            Console.WriteLine();
        }
    }
}
