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
| :---------- | :------------ |
| Activated   | ...           |
| Added       | ...           |
| Archived    | ...           |
| Attached    | ...           |
| Cloned      | ...           |
| Created     | ...           |
| Deactivated | ...           |
| Deleted     | ...           |
| Detached    | ...           |
| Enqueued    | ...           |
| Failed      | ...           |
| Information | ...           |
| Inserted    | ...           |
| Migrated    | ...           |
| Published   | ...           |
| Removed     | ...           |
| Restored    | ...           |
| Sent        | ...           |
| Succeded    | ...           |
| Unpublished | ...           |
| Updated     | ...           |

#### .With(Expression<Func<IEvent, object>> expression, object value)
| Property  | Type       | Default          | Description   |
| :-------- | :--------- | :--------------- | :------------ |
| ClubId    | int        | 0                | Should be set if the event was triggered from the context of a club |
| SiteId    | int        | 0                | Should be set if the event was triggered from the context of a site |
| System    | string     | Calling assembly | Indicates what system the event was triggered from, is |
| Reference | object     | null             | Indicates what object the event refers to, e.g. if you add an attribute for a user the attribute object should be set as the reference |
| By        | By (class) | null             | Indicates what user, if any, triggered the event |

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
            .With(x => x.By, by);

        _auditor.Send(message);
    }

    public Remove(Account account, Site site, By by)
    {
        _repository.Delete(account);

        var message = new Remove((nameof(Account), account)
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
