using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverPosition
{
    public enum Directions
    {
        N = 1, // Kuzey yönü
        S = 2, // Güney yönü
        E = 3, // Doğru yönü
        W = 4 // Batı yönü
    }

    public interface IPosition
    {
        void StartMoving(List<int> maxPoints, string moves); // It is like a signature!
    }

    public class Position : IPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Directions Direction { get; set; }

        public Position()
        {
            X = Y = 0; // Başlangıçta (0,0) noktasında
            Direction = Directions.N; // ve yönü kuzeye doğru
        }

        private void Rotate90Left()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Direction = Directions.W; // Yön kuzeyse ve sola dönerse yön batı oluyor.
                    break;
                case Directions.S:
                    this.Direction = Directions.E; // Yön güneyse ve sola dönerse yön doğu oluyor.
                    break;
                case Directions.E:
                    this.Direction = Directions.N; // Yön doğuya doğru ise ve sola dönerse Rover, yönü kuzey oluyor.
                    break;
                case Directions.W:
                    this.Direction = Directions.S; // Yön batıya doğru ve sola dönünce güneye yönelir.
                    break;
                default:
                    break;
            }
        }

        //Yukarıdaki ifadelerin benzeri tek fark bu sefer sağ tarafa dönmesi ele alındı.
        private void Rotate90Right()
        {
            switch (this.Direction)
            {
                case Directions.N:
                    this.Direction = Directions.E;
                    break;
                case Directions.S:
                    this.Direction = Directions.W;
                    break;
                case Directions.E:
                    this.Direction = Directions.S;
                    break;
                case Directions.W:
                    this.Direction = Directions.N;
                    break;
                default:
                    break;
            }
        }

        private void MoveInSameDirection()
        {
            switch (this.Direction)
            {
                case Directions.N: // Kuzey yönüne baktığı için "M" sonrası --> (x,y+1)
                    this.Y += 1;
                    break;
                case Directions.S: // Güney yönüne baktığı için "M" sonrası --> (x,y-1)
                    this.Y -= 1;
                    break;
                case Directions.E: // Doğu yönüne baktığı için "M" sonrası --> (x+1,y)
                    this.X += 1;
                    break;
                case Directions.W: // Batı yönüne baktığı için "M" sonrası --> (x-1,y)
                    this.X -= 1;
                    break;
                default:
                    break;
            }
        }


        // L - left
        // R - rigth
        // M - Move ( ileriye doğru sadece bir adım gider. ) 
        public void StartMoving(List<int> maxPoints, string moves)
        {
            foreach (var move in moves)
            {
                switch (move)
                {
                    case 'M':
                        this.MoveInSameDirection();
                        break;
                    case 'L':
                        this.Rotate90Left();
                        break;
                    case 'R':
                        this.Rotate90Right();
                        break;
                    default:
                        break;
                }

            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the upper-rigth coordinates of plateau (x,y) with leaving a space: ");
            var maxPoints = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToList();
            Console.Write("Enter the rover's (x,y,orientation) with leaving a space: ");
            var startPositions = Console.ReadLine().Trim().Split(' ');

            Position position = new Position
            {
                X = Convert.ToInt32(startPositions[0]),
                Y = Convert.ToInt32(startPositions[1]),
                Direction = (Directions)Enum.Parse(typeof(Directions), startPositions[2])
            };

            Console.Write("Enter the rover's L(left),R(rigth),M(move) letters without leaving space: ");
            var moves = Console.ReadLine().ToUpper();

            position.StartMoving(maxPoints, moves);

            Console.Write("Final coordinates and heading of rover is: ");
            Console.WriteLine($"{position.X} {position.Y} {position.Direction.ToString()}");        
        }
    }
}
