using System;
using SB.CoreTest;

/// <summary>
/// SchoolsBuddy Technical Test.
///
/// Your task is to find the highest floor of the building from which it is safe
/// to drop a marble without the marble breaking, and to do so using the fewest
/// number of marbles. You can break marbles in the process of finding the answer.
///
/// The method Building.DropMarble should be used to carry out a marble drop. It
/// returns a boolean indicating whether the marble dropped without breaking.
/// Use Building.NumberFloors for the total number of floors in the building.
///
/// A very basic solution has already been implemented but it is up to you to
/// find your own, more efficient solution.
///
/// Please use the function Attempt2 for your answer.
/// </summary>
namespace SB.TechnicalTest
{
    class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine($"Attempt 1 Highest Safe Floor: {Attempt1()}");
            Console.WriteLine($"Attempt 1 Total Drops: {Building.TotalDrops}");

            Console.WriteLine();
            Building.Reset();

            Console.WriteLine($"Attempt 2 Highest Safe Floor: {Attempt2()}");
            Console.WriteLine($"Attempt 2 Total Drops: {Building.TotalDrops}");
        }

        /// <summary>
        /// First attempt - start at first floor and work up one floor at a time
        /// until you reach a floor at which marble breaks.
        /// The highest safe floor is one below this.
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt1()
        {
            var i = 0;
            while (++i <= Building.NumberFloors && Building.DropMarble(i));

            return i - 1;
        }

        /// <summary>
        /// Second attempt - you do this one!
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt2()
        {
            //So in the first thought the solution I have chosen is something of a binary sort.
            //We know the highest floor - Building.NumberOfFloors, but I, the developer technically
            // don't and my code needs to run for any number of floors.
            // I'd also really like to be able to test this against larger and smaller numbers of floors to really test my working,
            // But I think in doing that it would defeat the point of the exercise somewhat.

            var startFloor = 0;
            var endFloor = Building.NumberFloors;
            var midFloor = endFloor;
            var highestFloor = 0;
            
            //Starting at the highest point, drop a marble, you never know, we might get lucky.
            var safeResult = Building.DropMarble(midFloor);
            
            //if we arent safe...
            if (!safeResult)
            {
                while (true)
                {
                    //Half the total
                    midFloor = GetMidFloor(startFloor, endFloor);

                    //Check we have reached the end and break if we need too
                    if (highestFloor == midFloor) break;

                    if (Building.DropMarble(midFloor))
                    {
                        //If safe then we know we need to look in the higher section
                        startFloor = midFloor;
                    }
                    else
                    {
                        //We know we are in the lower half
                        endFloor = midFloor;
                    }

                    highestFloor = midFloor;
                }
            }
            
            return midFloor;
        }

        private static int GetMidFloor(int startFloor, int endFloor)
        {
            return ((endFloor - startFloor) / 2) + startFloor; //As much as this code works, I feel dirty about dividing an odd number by 2 and shoving it into an int. 
        }
    }
}
