public class LockExample
{
    private readonly Lock _lockObject;
    private Decimal _balance;

    public LockExample()
    {
        this._lockObject = new Lock();
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
        // The EnterScope method of the Lock object is called to acquire a lock on the critical section (in this case, updating the balance)
        Lock.Scope scope = this._lockObject.EnterScope();
        try
        {
            this._balance += amount;
        }
        finally
        {
            //Releasing the Lock
            scope.Dispose();
        }
    }
}