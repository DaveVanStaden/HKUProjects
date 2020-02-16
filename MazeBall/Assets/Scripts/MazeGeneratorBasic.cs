using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeGeneratorBasic
{
    public int RowCount { get { return mMazeRows; } }
    public int ColumnCount { get { return mMazeColums; } }

    private int mMazeRows;
    private int mMazeColums;
    private MazeCell[,] mMaze;

    public MazeGeneratorBasic(int rows, int columns)
    {
        mMazeRows = Mathf.Abs(rows);
        mMazeColums = Mathf.Abs(columns);
        if (mMazeRows == 0)
        {
            mMazeRows = 1;
            
        }
        if (mMazeColums == 0)
        {
            mMazeColums = 1;
        }

        mMaze = new MazeCell[rows, columns];
        for(int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                mMaze[row, column] = new MazeCell();
            }
        }
    }

    public abstract void GenerateMaze();

    public MazeCell GetMazeCell(int row, int column)
    {
        if(row >= 0 && column >= 0 && row < mMazeRows && column < mMazeColums)
        {
            return mMaze[row, column];
        }
        else
        {
            Debug.Log(row + " " + column);
            throw new System.ArgumentOutOfRangeException();
        }
    }

    protected void SetMazeCell(int row, int column, MazeCell cell)
    {
        if(row >= 0 && column >= 0 && row < mMazeRows && column < mMazeColums)
        {
            mMaze[row, column] = cell;
        }
        else
        {
            throw new System.ArgumentOutOfRangeException();
        }
    }
}
