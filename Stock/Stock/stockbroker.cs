using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Stock
{
    public class StockBroker
    {
        public string BrokerName { get; set; }

        public List<stock> stocks = new List<stock>();

        public static ReaderWriterLockSlim myLock = new ReaderWriterLockSlim();
        //readonly string docPath = @"C:\Users\Documents\CECS 475\Lab3_output.txt";

        readonly string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        "Lab1_output.txt");

        public string titles = "Broker".PadRight(16) + "Stock".PadRight(16) +
        "Value".PadRight(16) + "Changes".PadRight(16) + "Date and Time";

        private static bool titlePrinted = false;
        //---------------------------------------------------------------------------------------
        /// <summary>
        /// The stockbroker object
        /// </summary>
        /// <param name="brokerName">The stockbroker's name</param>
        public StockBroker(string brokerName)
        {
            BrokerName = brokerName;
        }
        //---------------------------------------------------------------------------------------
        /// <summary>
        /// Adds stock objects to the stock list
        /// </summary>
        /// <param name="stock">Stock object</param>
        public void AddStock(stock stock)
        {
            stocks.Add(stock);
            stock.StockEvent += EventHandler;
        }
        //---------------------------------------------------------------------------------------

        /// <summary>
        /// The eventhandler that raises the event of a change
        /// </summary>
        /// <param name="sender">The sender that indicated a change</param>
        /// <param name="e">Event arguments</param>
        async void EventHandler(Object sender, EventArgs e)
        {
            ///Console.WriteLine(titles);
            try
            {
                myLock.EnterWriteLock();

                //______________________________
                stock newStock = (stock)sender;
                var stockNotification = e as StockNotification;
                //!NOTE!: Check out C#events, pg.4
                // Display the output to the console windows
                if (stockNotification != null)
                {
                    string output = BrokerName.PadRight(16) + stockNotification.StockName.PadRight(16) + stockNotification.CurrentValue + " ".PadRight(16) + stockNotification.NumChanges + " ".PadRight(16) + DateTime.Now.ToString(); ;
                    using (StreamWriter outputFile = new StreamWriter(destPath, false))
                    {
                        if (!titlePrinted)
                        {
                            Console.WriteLine(titles);
                            await outputFile.WriteLineAsync(titles);
                            titlePrinted = true;
                        }
                        await outputFile.WriteLineAsync(output);
                    }

                    Console.WriteLine(output);

                }
            }
            finally
            {
                myLock.ExitWriteLock();
            }
        }
        //---------------------------------------------------------------------------------------
    }
}

