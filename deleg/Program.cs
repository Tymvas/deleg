using System;
using System.Threading;
using static System.Console;

namespace deleg
{
    public delegate void GameDelegate();

    abstract class Car
    {
        private double _distance = 100;
        protected string name;
        protected int maxSpeed;
        private int carSpeed = 0;
        public Car(int maxSpeed = 60, string name = "") { this.maxSpeed = maxSpeed; this.name = name; }
        public void Move() 
        {
            Random rand = new Random();
            carSpeed = rand.Next(0, maxSpeed);
            _distance -= (carSpeed / 12);
            if (_distance <= 0) throw new Exception($"{name} - WINNER!");
            WriteLine($"{carSpeed}\nDistance: {_distance}");
        }
    }

    class SportCar : Car
    {
        public SportCar(int maxSpeed) : base(maxSpeed, "SportCar") { }
    }

    class SmalCar : Car
    {
        public SmalCar(int maxSpeed) : base(maxSpeed, "SmalCar") { }
    }

    class BigCar : Car
    {
        public BigCar(int maxSpeed) : base(maxSpeed, "BigCar") { }
    }

    class Bus : Car
    {
        public Bus(int maxSpeed) : base(maxSpeed, "Bus") { }
    }

    class Game
    {
        private int _money = 1000;
        public event GameDelegate GameEvent;
        public void Start()
        {
            SportCar sport = new SportCar(200);
            SmalCar smal = new SmalCar(150);
            BigCar big = new BigCar(120);
            Bus bus = new Bus(100);


            GameEvent += sport.Move;
            GameEvent += smal.Move;
            GameEvent += big.Move;
            GameEvent += bus.Move;

            while(true)
            {
                try
                {
                    Clear();
                    GameEvent.Invoke();
                    WriteLine();
                }
                catch(Exception ex)
                {
                    WriteLine(ex.Message);
                    break;
                }
                Thread.Sleep(5000);
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
