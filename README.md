# laget.Auditing
A simple framework to audit entities in .NET and .NET Core...

![Nuget](https://img.shields.io/nuget/v/laget.Auditing)
![Nuget](https://img.shields.io/nuget/dt/laget.Auditing)

## Usage
### By
```c#
public class By
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("ip")]
    public string Ip { get; set; }
    [JsonProperty("referenceId")]
    public string ReferenceId { get; set; }
    [JsonProperty("superadmin")]
    public bool Superadmin { get; set; } = false;
}
```

### Auditor
#### Events (built-in)
| Property    | Description   |
| ----------- | ------------- |
| Added       | ...           |
| Created     | ...           |
| Deleted     | ...           |
| Enqueued    | ...           |
| Failed      | ...           |
| Information | ...           |
| Inserted    | ...           |
| Migrated    | ...           |
| Published   | ...           |
| Removed     | ...           |
| Succeded    | ...           |
| Unpublished | ...           |
| Updated     | ...           |

#### .With(Expression<Func<IEvent, object>> expression, object value)
| Property  | Type       | Description   |
| --------- | ---------- | ------------- |
| ClubId    | int        | ...           |
| SiteId    | int        | ...           |
| System    | string     | ...           |
| Reference | object     | ...           |
| By        | By (class) | ...           |

#### Examples
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

        var message = new Created(nameof(Account), account)
            .With(x => x.By, by);

        _auditor.Send(message);
    }

    public Update(Account account, By by)
    {
        _repository.Update(account);

        var message = new Updated((nameof(Account), account)
            .With(x => x.By, by);

        _auditor.Send(message);
    }

    public Delete(Account account, By by)
    {
        _repository.Delete(account);

        var message = new Deleted((nameof(Account), account)
            .With(x => x.By, by)
            .With(x => x.Reference, site);

        _auditor.Send(message);
    }
}
```

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
        var account = new
        {
            id = 123,
            name = "Jane Doe"
        };
        var site = new
        {
            id = 123,
            name = "FC GonAce"
        };

        var message = new Created("Account", account)
            .With(x => x.By, by)
            .With(x => x.Reference, site);

        await _auditor.Send(message);
    }
}
```
