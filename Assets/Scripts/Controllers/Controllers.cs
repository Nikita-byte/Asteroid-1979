

public sealed class Controllers : IInitialization
{
    private IExecute[] _executes;
    private IInitialization[] _initializations;
    private Context _context;

    public IExecute[] Executes { get { return _executes; } }

    public Controllers()
    {
        _context = new Context();

        _initializations = new IInitialization[2];
        _initializations[0] = new EnemysController(_context);
        _initializations[1] = new CruiserController(_context, _initializations[0] as EnemysController);

        _executes = new IExecute[3];
        _executes[0] = _initializations[0] as IExecute;
        _executes[1] = new InputController(_initializations[1] as CruiserController, _context, _initializations[0] as EnemysController);
        _executes[2] = _initializations[1] as IExecute;
    }

    public void Initialization()
    {
        foreach (IInitialization initialization in _initializations)
        {
            initialization.Initialization();
        }
    }
}
