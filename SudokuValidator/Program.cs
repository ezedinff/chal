using System;
using System.Linq;

class SudokuValidator
{
    public static bool IsValid(int[][] board)
    {
        if (board == null || board.Length == 0)
            return false;

        int n = board.Length;
        double sqrt = Math.Sqrt(n);
        
        // Check if N > 0 and √N is an integer
        if (n <= 0 || sqrt != Math.Floor(sqrt))
            return false;

        // Check if all rows have correct length
        if (board.Any(row => row.Length != n))
            return false;

        int boxSize = (int)sqrt;

        // Check rows, columns, and boxes using LINQ
        return ValidateRows(board, n) &&
               ValidateColumns(board, n) &&
               ValidateBoxes(board, n, boxSize);
    }

    private static bool ValidateRows(int[][] board, int n)
    {
        return board.All(row =>
            row.All(num => num >= 1 && num <= n) && // Check range
            row.Distinct().Count() == n);           // Check uniqueness
    }

    private static bool ValidateColumns(int[][] board, int n)
    {
        return Enumerable.Range(0, n).All(col =>
        {
            var column = Enumerable.Range(0, n)
                .Select(row => board[row][col])
                .ToList();

            return column.All(num => num >= 1 && num <= n) && // Check range
                   column.Distinct().Count() == n;            // Check uniqueness
        });
    }

    private static bool ValidateBoxes(int[][] board, int n, int boxSize)
    {
        return Enumerable.Range(0, n).All(box =>
        {
            int rowStart = (box / boxSize) * boxSize;
            int colStart = (box % boxSize) * boxSize;

            var square = Enumerable.Range(rowStart, boxSize)
                .SelectMany(row => Enumerable.Range(colStart, boxSize)
                    .Select(col => board[row][col]))
                .ToList();

            return square.All(num => num >= 1 && num <= n) && // Check range
                   square.Distinct().Count() == n;            // Check uniqueness
        });
    }

    public static void Main()
    {
        // Example usage with the provided test case
        int[][] goodSudoku1 = {
            new int[] {7,8,4, 1,5,9, 3,2,6},
            new int[] {5,3,9, 6,7,2, 8,4,1},
            new int[] {6,1,2, 4,3,8, 7,5,9},

            new int[] {9,2,8, 7,1,5, 4,6,3},
            new int[] {3,5,7, 8,4,6, 1,9,2},
            new int[] {4,6,1, 9,2,3, 5,8,7},

            new int[] {8,7,6, 3,9,4, 2,1,5},
            new int[] {2,4,3, 5,6,1, 9,7,8},
            new int[] {1,9,5, 2,8,7, 6,3,4}
        };

        bool isValid = IsValid(goodSudoku1);
        Console.WriteLine($"Is the Sudoku valid? {isValid}");

        // Additional test case with invalid Sudoku
        int[][] badSudoku = {
            new int[] {1,1,1, 1,1,1, 1,1,1},
            new int[] {2,2,2, 2,2,2, 2,2,2},
            new int[] {3,3,3, 3,3,3, 3,3,3},

            new int[] {4,4,4, 4,4,4, 4,4,4},
            new int[] {5,5,5, 5,5,5, 5,5,5},
            new int[] {6,6,6, 6,6,6, 6,6,6},

            new int[] {7,7,7, 7,7,7, 7,7,7},
            new int[] {8,8,8, 8,8,8, 8,8,8},
            new int[] {9,9,9, 9,9,9, 9,9,9}
        };

        bool isBadValid = IsValid(badSudoku);
        Console.WriteLine($"Is the bad Sudoku valid? {isBadValid}");
    }
}
