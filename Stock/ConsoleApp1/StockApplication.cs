using Stock;

public class StockApplication
{
    static void Main(string[] args)
    {
        stock stock1 = new stock("Technology", 160, 5, 15);
        stock stock2 = new stock("Retail", 30, 2, 6);
        stock stock3 = new stock("Banking", 90, 4, 10);
        stock stock4 = new stock("Commodity", 500, 20, 50);
        StockBroker b1 = new StockBroker("Broker 1");
        b1.AddStock(stock1);
        b1.AddStock(stock2);
        StockBroker b2 = new StockBroker("Broker 2");
        b2.AddStock(stock1);
        b2.AddStock(stock3);
        b2.AddStock(stock4);
        StockBroker b3 = new StockBroker("Broker 3");
        b3.AddStock(stock1);
        b3.AddStock(stock3);
        StockBroker b4 = new StockBroker("Broker 4");
        b4.AddStock(stock1);
        b4.AddStock(stock2);
        b4.AddStock(stock3);
        b4.AddStock(stock4);
    }
}
