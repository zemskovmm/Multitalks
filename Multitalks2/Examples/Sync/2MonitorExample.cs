public class MonitorExample
{
    private readonly object _lockObject;
    private Decimal _balance;

    public MonitorExample()
    {
        this._lockObject = new();
    }

    public Decimal GetBalance()
    {
        return _balance;
    }

    public void DepositSugar(Decimal amount)
    {
        // The EnterScope method of the Lock object is called to acquire a lock on the critical section (in this case, updating the balance)
        lock (_lockObject)
        {
            this._balance += amount;
        }
    }

    public void DepositUnwrapped(Decimal amount)
    {
        bool lockTaken = false;
        try
        {
            Monitor.Enter(_lockObject, ref lockTaken);
            this._balance += amount;
        }
        finally
        {
            if (lockTaken) Monitor.Exit(_lockObject);
        }
    }
}


