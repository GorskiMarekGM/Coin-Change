using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CoinChangeLab
{

    class Program
    {
        static void Main(string[] args)
        {
            int[] Coins = { 1, 2, 5, 10, 20, 50 ,100 };
            int change = 47;
            Console.WriteLine("Hello, this is coin change calculator");
            Console.WriteLine("Enter value for change");
            change = Convert.ToInt32(Console.ReadLine());
            if (change<0)
            {
                throw new System.InvalidOperationException("Enter change greater than 0");
            }
            Console.WriteLine("Available coins: {0}", string.Join(", ", Coins));
            Console.WriteLine("Change to be made: {0}", change);
            makeChangeGreedy(Coins, change);
            makeChangeDynamic(Coins, change);
        }
        
        public static void makeChangeGreedy(int[] coinsTemp, int change)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int[] coins = new int[coinsTemp.Length];

            Array.Copy(coinsTemp, coins, coins.Length);
            Array.Sort(coins);
            Array.Reverse(coins);

            List<string> change_list = new List<string>();

            foreach (int coin in coins)
            {
                int how_many = change / coin;

                if (how_many > 0)
                {
                    change_list.Add(how_many + "x" + coin);
                }

                change = change % coin;
            }

            Console.WriteLine(string.Join(", ", change_list.ToArray()));
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time in Milliseconds: " + elapsedMs);
        }

        public static void makeChangeDynamic(int[] arrCoins, int change)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int[] coins = new int[arrCoins.Length];
            Array.Copy(arrCoins, coins, arrCoins.Length);
            Array.Sort(coins);

            int[] minCoins = new int[change + 1];
            int[] firstCoinIndex = new int[change + 1];

            for (int currChange = 0; currChange < change + 1; currChange++)
            {
                int coinCount = currChange;
                int newCoinIndex = 0;

                for (int coinIndex = 0; coinIndex < coins.Length; coinIndex++)
                {
                    int coin = coins[coinIndex];
                    if (coin > currChange)
                    {
                        continue;
                    }
                    if (1 + minCoins[currChange - coin] < coinCount)
                    {
                        coinCount = 1 + minCoins[currChange - coin];
                        newCoinIndex = coinIndex;
                    }
                }
                minCoins[currChange] = coinCount;
                firstCoinIndex[currChange] = newCoinIndex;
            }

            int currChange2 = change;
            int[] coincount = new int[arrCoins.Length];
            List<string> change_list = new List<string>();

            while (currChange2 > 0)
            {
                int coin = coins[firstCoinIndex[currChange2]];
                coincount[firstCoinIndex[currChange2]]++;
                currChange2 -= coin;
            }
            for (int i = 0; i < coincount.Length; i++)
            {
                if (coincount[i] > 0)
                {
                    change_list.Add(coincount[i] + "x" + coins[i]);
                }
            }
            Console.WriteLine(string.Join(", ", change_list.ToArray()));
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time in Milliseconds: " + elapsedMs);

        }
    }

}
