# laget.Auditing
A simple framework to audit entities in .NET and .NET Core...

![Nuget](https://img.shields.io/nuget/v/laget.Auditing)
![Nuget](https://img.shields.io/nuget/dt/laget.Auditing)

## Usage
### Auditor
```c#
public class AccountService
{
    private readonly IAuditor _auditor;
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _auditor = new Auditor("ServiceBusConnectionString");
        _repository = repository;
    }


    public Create(Account account, By by)
    {
        account = _repository.Create(account, By by);

        var message = new Created(account)
            .With(x => x.By, by);

        _auditor.Send(message);

    }

    public Update(Account account, By by)
    {
        _repository.Update(account);

        var message = new Updated(account)
            .With(x => x.By, by);

        _auditor.Send(message);
    }

    public Delete(Account account, By by)
    {
        _repository.Delete(account);

        var message = new Deleted(account)
            .With(x => x.By, by)
            .With(x => x.From, site);

        _auditor.Send(message);
    }

    public RemoveFromSite(Account account, Site site, By by)
    {
        _repository.RemoveFromSite(account, site);

        var message = new Removed(account)
            .With(x => x.By, by)
            .With(x => x.From, site);

        _auditor.Send(message);
    }
}
```

### Methods
```c#
```