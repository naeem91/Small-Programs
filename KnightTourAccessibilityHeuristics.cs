using System;

namespace KnightTour
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] chess = new int[8, 8];   //empty chess board

            int[,] priority = { {2,3,4,4,4,4,3,2 },         //accessibility of each square from other squares
                                {3,4,6,6,6,6,4,3 }, 
                                {4,6,8,8,8,8,6,4 }, 
                                {4,6,8,8,8,8,6,4 }, 
                                {4,6,8,8,8,8,6,4 },
                                {4,6,8,8,8,8,6,4 }, 
                                {3,4,6,6,6,6,4,3 }, 
                                {2,3,4,4,4,4,3,2 }
                              };

            int[] horizontal = { 2, 1, -1, -2, -2, -1, 1, 2 };      //horizontaly possible moves 

            int[] vertical = { -1, -2, -2, -1, 1, 2, 2, 1 };          //vertically possible moves


            int currentRow = 0;                             //shows currently occupied row and 

            int currentCol = 0;                              //column by Knight

            int counter = 1;                                 //used to mark visited squares in sequence

            chess[currentRow, currentCol] = counter;            //at start current row and column are occupied 


            int tempRow;                                    //temporarily holds calculated

            int tempCol;                                       //rows and columns

            int smallestPriori;                              //holds smallest value of priority

            int goodMove;                                  //holds value of move with smallest accessibility

            int canMove = 1;                                //can knight furthur move or not


            while (canMove == 1)
            {
                canMove = 0;

                smallestPriori = 9;

                goodMove = -1;


                for (int move = 0; move < 8; move++)
                {

                    tempRow = currentRow;                       //reset the value of temp row and col                     

                    tempCol = currentCol;

                    tempRow = tempRow + vertical[move];         //calculate move's destination

                    tempCol = tempCol + horizontal[move];

                    if (tempRow < 8 && tempRow >= 0 && tempCol < 8 && tempCol >= 0 && chess[tempRow, tempCol] == 0)  //check if this move is valid
                    {
                        canMove = 1;

                        if (priority[tempRow, tempCol] < smallestPriori)
                        {
                            smallestPriori = priority[tempRow,tempCol];

                            goodMove = move;

                        }
                    }
                }

                if (goodMove != -1)
                {
                    counter++;

                    currentRow = currentRow + vertical[goodMove];    //move to the smallest accessibility square

                    currentCol = currentCol + horizontal[goodMove];

                    for (int i = 0; i < 8; i++)                     //reduce accessibility of all other 
                    {
                        if (currentRow + vertical[i] < 8 && currentRow + vertical[i] >= 0 && currentCol + horizontal[i] < 8 && currentCol + horizontal[i] >= 0)
                            --priority[currentRow + vertical[i], currentCol + horizontal[i]];
                    }

                    chess[currentRow, currentCol] = counter;
                }

            }

            //display chess board

            for (int row = 0; row < chess.GetLength(0); row++)
            {
                for (int col = 0; col < chess.GetLength(1); col++)
                {
                    Console.Write("{0,7}",chess[row,col]);
                }

                Console.WriteLine();

                Console.WriteLine();
            }


            Console.WriteLine();

            Console.Write("Total moves: {0}", counter);            //total squares moved by knight


            Console.WriteLine();

            Console.ReadKey();

        }
    }

}