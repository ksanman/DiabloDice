using System;

namespace DiabloDice
{
    public class DiabloDiceGameLogic : IDisposable
    {
        private int maxScore;
        private Random randomizer;

        public DiabloDiceGameLogic(int maxScore)
        {
            this.maxScore = maxScore;
            randomizer = new Random(DateTime.Now.Millisecond);
        }

        public int GetComputerDiceRoll(int currentScore)
        {
            var score = 0;
            var numberOfRolls = GetNumberOfComputerRolls(currentScore);
            for (int i = 0; i < numberOfRolls; i++)
            {
                score = RollDice();
            }
            return (score == 0) ? 0 : score; 

        }

        private int GetNumberOfComputerRolls(int currentScore)
        {
            int value = 0;

            if (currentScore == 0)
            {
                value = (randomizer.Next() % 6) + 1; //random number 1-6
                if (value <= 3)
                {    
                    // if random number <= 3 add two to it.
                    value += 2;
                }
            }
            else if (currentScore <= 30 && currentScore > 0)
            {
                value = (randomizer.Next() % 5) + 1; //random number 1-5
                if (value <= 2)
                {   
                    // if random number <= 2 add 1 to it.
                    value += 1;
                }
            }
            else if (currentScore <= 60 && currentScore > 30)
            {
                value = (randomizer.Next() % 4) + 1; //random number 1-4
            }
            else if (currentScore <= 90 && currentScore > 60)
            {
                value = (randomizer.Next() % 3) + 1; //random number 1-3
            }
            else if (currentScore <= 99 && currentScore > 90)
            {
                value = (randomizer.Next() % 2) + 1; //random number 1-2
            }
            //returns the value of what the devil should roll
            return value;
        }

       /// <summary>
       /// Rolls the dice and reports back the value of the dice roll. 
       /// </summary>
       /// <returns> An interger representation of the dice roll. </returns>
        public int RollDice()
        {
            return (randomizer.Next() % 6) + 1;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls   
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //writer.Dispose();
                    //reader.Dispose();
                }
                disposedValue = true;
            }
        }
        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}