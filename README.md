# **Repository and Unit of Work Design Patterns**

## **Repository Design Pattern**
The repository pattern is intended to **create an abstraction layer between the data access layer and the business logic layer** of an application. It is a data access pattern that prompts a more **loosely coupled approach** to data access. We create the data access logic in a separate class, or set of classes, called a repository with the responsibility of persisting the application's business model.

### **Advantages of Repository Design Pattern**
* **Database access logic and domain logic can be tested separately** with this pattern.
* **Domain-driven development** is easier.
* **Clean, maintainable, and reusable code**
* It **reduces redundancy of code**, generic repository is better solution than normal repository pattern to reduce code duplication.
* With this pattern it is **easy to maintain the centralized data access logic**.
* DRY (Don’t Repeat Yourself) design, the code to query and fetch data from data source, commands for updates (update, deletes) are not repeated.
* With using the Repository design pattern, you can **hide the details of how the data is eventually stored or retrieved** to and from the data store (data store can be a database, an xml file, etc)
* Provides a **flexible architecture**.


## **Unit of Work(UOW) Design Pattern**
Unit of Work is **referred to as a single transaction that involves multiple operations of insert/update/delete** and so on kinds. To say it in simple words, it means that for a specific user action (say registration on a website), all the transactions like insert/update/delete and so on are done in one single transaction, rather then doing multiple database transactions. 

---------------

## **About this exercise**
In this lab we will be working on the **Backend Code base** only

## **Backend Code Base**
### **Previously** 
We developed a base structure of an API solution in asp.net core that have just two controllers which are `TransactionController` and `AccountController`. We created a DTO for account request and added server-side validations with **data annotations** on the properties. We also used **AutoMapper** for to bind entities with the DTO.

* `TransactionController` have api functions `GetLast12MonthBalances` and `GetLast12MonthBalances/{userId}` which returns data for the last 12 months total balances.
* `AccountController` have api function `OpenAccount(Account account)` which is used to create an account for the user

There are 4 Projects in the solution. 

*	**Entities** : This project **contains DB models** like User where each User has one Account and each Account can have one or many Transactions. There is also a Response Model of LineGraphData that will be returned as API Response. 

*	**Infrastructure**: This project **contains BBBankContext** that service as fake DBContext that populates one User with its corresponding Account that has three Transactions dated of last three months with hardcoded data. 

* **Services**: This project **contains TransactionService** with the logic of converting Transactions into LineGraphData after fetching them from BBBankContext.

* **BBBankAPI**: This project **contains TransactionController** with 2 GET methods `GetLast12MonthBalances` & `GetLast12MonthBalances/{userId}` to call the TransactionService.

![](readme-images/4.png)

For more details about this base project see: https://github.com/PatternsTechGit/PT_ServerSideValidation-with-AutoMapper

-----------

## **Frontend Code Base**
Previously we have angular application in which we have

* **FontAwesome** library for icons.
* **Bootstrap** library for styling.
* Created **client side models** to receive data.
* Created **transaction service** to call the API.
* **Fixed the CORS** error on the server side.
* **Populated html table**, using data returned by API.
* Create **template driven form**.
* Perform **input fields validation**.
* Create client side **models** to map data for API.
* Create the **account service** to call the API.
   
 We don't have any further implementations for front-end side for this lab

_____________

## **In this exercise**

### **Backend Code**
* **Setup Repository Contract**
* **Implement Repository Class**
* **Setup Unit of Work Contract**
* **Implement Unit of Work**
* **Setup Specialized Account Repository Contract**
* **Implement Specialized Account Repository**
* **Change Account Service** w.r.t the Account Repository and Unit of Work
* **Add Repositories and UOW as a Service in Web Builder**


## Step 1: User and Account model changes

We will make some **user** model properties *nullable* so that if these are not provided from frontend then we should not be getting errors thrown by database so user model would be looks like this. Consider the **question mark (?)** in front of properties that needs to be nullable . 

```cs
    public class User : BaseEntity // Inheriting from Base Entity class
    {
        // First name
        public string? FirstName { get; set; }

        // Last name
        public string? LastName { get; set; }

        // Email of the user
        public string Email { get; set; }

        // Profile picture or avatar
        public string? ProfilePicUrl { get; set; }

        // Account attached to the user
        // disbaling default asp.net validation since it is a navigation property 
        [ValidateNever]
        public virtual Account Account { get; set; }
    }

```

In **Account** class we will decorate the `AccountStatus` with `[JsonConverter(typeof(JsonStringEnumConverter))]` so that it can map frontend values to account status enum.

```cs
        // This decoration is required to conver integer coming in from UI to respective Enum
        [JsonConverter(typeof(JsonStringEnumConverter))]
        //Account's status
        public AccountStatus AccountStatus { get; set; }

```

## Step 2 Data Migration
After changes in `User` & `Account` model, now you need to execute data migration commands so that schema changes can be reflected in database as well but before data migration commands you have to follow [data seeding](https://github.com/PatternsTechGit/PT_AzureSql_EFDataSeeding) lab so that you have database setup to execute migration .

### Step 2.1 Migration Commands Execution
Now open package manage console and select infrastructure project and run the command `Add-Migration` which creates a new migration class as per specified name
```
Add-Migration repositoryAndUnitOfWorkPattern
```
Then run the `update-Database` which executes the last migration file created by the Add-Migration command and applies changes to the database schema.
```
Update-Database
```

## Step 3: Setup Repository Contract
Create a new folder ***Contacts*** in the ***Infrastructure*** project of the solution. **Create an interface IRepository of type TEntity** where TEntity represents the BaseEntity class. In this interface we have defined the signatures of the common functionalities like CRUD and search etc which are shown in the code below.    

```cs
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity t);
    Task<TEntity[]> BatchAddAsync(TEntity[] entities);
    Task<int> CountAsync();
    void DeleteAsync(TEntity t);
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, string[] includes = null);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
    Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetAsync(string id, string include = null);
    Task<TEntity> GetAsync(string id, string[] includes = null);
    Task<TEntity> GetAsync(string id);
    Task<TEntity> UpdateAsync(TEntity updated);
    Task<bool> Exists(Expression<Func<TEntity, bool>> match);
}
```

## Step 4: Implement Repository Class
Create a new class **SQLRepository of type TEntity**, where TEntity represents the BaseEntity class, in the ***Infrastructure*** project. The **SQLRepository inherits from the IRepository interface**. The SQLRepository **provides the implementations** of the signatures defined in the repository contract. We have used the DBSet in the SQLRepository. A **DbSet represents the collection of all entities in the context**, or that can be queried from the database, of a given type. **DbSet objects are created from a DbContext** using the DbContext. The implementations are shown in the code below

```cs
 public class SQLRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbContext _context;
        protected DbSet<TEntity> DbSet;

        //DbContext is set to resokve with instance of BBBankContext in DI
        public SQLRepository(DbContext context)
        {
            //so _context goign to have BBBankContext because of DI
            _context = context;
            //DBSet set to incoming Generic Type
            DbSet = _context.Set<TEntity>();
        }
        // function returns colection of base objects by filtering on navigation properties.
        // where navigation properties are passed as expression
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, object>>[] includes = null)
        {
            var query = DbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }
            return await query.ToListAsync();
        }
        // function returns single base object by filtering on ID
        public async Task<TEntity> GetAsync(string id)
        {

            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
        // function returns single base object by filtering on id and including one navigation property
        public async Task<TEntity> GetAsync(string id, string include = null)
        {
            var query = DbSet.Include(include);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
        // function returns single base object by filtering on id and by including multiple navigation properties passed in as string array
        public async Task<TEntity> GetAsync(string id, string[] includes = null)
        {
            var query = DbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
        // function returns single base object by filtering  on expression
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await DbSet.SingleOrDefaultAsync(match);
        }
        // function returns collection of base object by filtering  on expression and including multiple navigation properties
        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, string[] includes = null)
        {
            var query = DbSet.AsQueryable();
            if (includes != null)
            {
                query = includes.Aggregate(query,
                  (current, include) => current.Include(include));
            }
            return await query.Where(match).ToListAsync();
        }
        // function ads entity to a DbSet and begins tracking
        public async Task AddAsync(TEntity t)
        {
            try
            {
                if (String.IsNullOrEmpty(t.Id))
                {
                    t.Id = Guid.NewGuid().ToString();
                }
                var xx = await DbSet.AddAsync(t);
            }
            catch (Exception ex)
            {
                //log before sending up the chain.
                throw; // sends exception up the chain.
            }
            finally
            {
                // do something before passing up the chain. 
            }

        }
        // adds multiple items to an entity set and begin tracking
        public async Task<TEntity[]> BatchAddAsync(TEntity[] entities)
        {
            await this._context.AddRangeAsync(entities);
            return entities;
        }
        // function checks existance of an entity to be updated using its id.
        // if it exists it replaces its current values with new incoming values.
        public async Task<TEntity> UpdateAsync(TEntity updated)
        {
            if (updated == null)
                return null;

            TEntity existing = await DbSet.FindAsync(updated.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
            }
            return existing;
        }
        // functions sets the value for delition
        public void DeleteAsync(TEntity t)
        {
            DbSet.Remove(t);
        }
        // function returns the count of and entity.
        public async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> match)
        {
            return await DbSet.AnyAsync(match);
        }
    }
```

## Step 5: Setup Unit of Work Contract
Create an interface ***IUnitOfWork*** in the ***Contract folder of the Infrastructure*** project. The contract will define the repository contracts of the entities and a method signature for committing changes to the database. The code is shown below 

```cs
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        IRepository<User> UserRepository { get; }
        Task<int> CommitAsync();
    }
```

## Step 6: Implement Unit of Work
Create a new class ***UnitOfWork*** in the ***Infrastructure project*** which is inherited from the UOW contract IUnitOfWork. We will **DI the repositories to create its instances and also the DbContext for the CommitAsync method**. The **CommitAsync method will simply commit all the changes at once** for all the operation performed in the repositories mentioned in the UnitOfWork class. The code for UOW is given below


```cs
    public class UnitOfWork : IUnitOfWork
    {
        //DI DbContext here will give it an instance of BBBankContext
        protected readonly DbContext _context;
        //UOW also acting as a wrapper on all the repositories .
        // So that all the repositories can be accessed from UOW in services layer
        public IAccountRepository AccountRepository { get; }
        public IRepository<Transaction> TransactionRepository { get; }
        public IRepository<User> UserRepository { get; }

        public UnitOfWork(DbContext context, IAccountRepository accountRepository,
                         IRepository<Transaction> transactionRepositoy,
                         IRepository<User> userRepositoy)
        {
            this.AccountRepository = accountRepository;
            this.TransactionRepository = transactionRepositoy;
            this.UserRepository = userRepositoy;
            this._context = context;
        }
        // and calling SaveChangesAsync on context will persist all the changes EF is tracking as one transaction.
        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
```

## Step 7: Setup Specialized Account Repository Contract
The specialized repository is a kind of unique repository which has all the common methods of the IRepository and also the methods that has a very specific functions for it. Create a new Class  ***IAccountRepository*** in the ***Contract folder of the Infrastructure*** project. It is inherited from the IRepository of account type. IAccountRepository class contains the prototypes for all the methods in the IRepository plus its own methods. We have defined **GetAllAccountsPaginated** as a specific method in this example which will return the paginated account list as a response. The code is given below

```cs
   public interface IAccountRepository : IRepository<Account>
    {
        Task<ICollection<Account>> GetAllAccountsPaginated(int pageIndex, int pageSize);
    }
```
## Step 8: Implement Specialized Account Repository
  Create a new class  ***AccountRepository*** in the ***Infrastructure*** project. It will contain the implementation of specialized repository pattern that is inherited from SQLRepository and also it will have everything in SQLRepository plus some special cases of data logics. We have used DbContext as a DI for its context. 
```cs
// Implementation of specialized repository pattern that is inherited from SQLRepository
// will have everything in SQLRepository plus some special cases of Datalogic 
public class AccountRepository : SQLRepository<Account>, IAccountRepository
{
    private DbContext _context;
    public AccountRepository(DbContext context)
        : base(context)
    {
        this._context = context;
    }

    public async Task<ICollection<Account>> GetAllAccountsPaginated(int pageIndex, int pageSize)
    {
        return await this.DbSet.Include(x => x.User)
            // first n number of records will be skpped based on pageSize and pageIndex
            // for example for pageIndex 2 of pageSize is 10 first 10 records will be skipped.
            .Skip((pageIndex) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
```
## Step 9: Change Account Service w.r.t the Account Repository and Unit of Work
Now we will change account service with respect to the account repository and Unit of Work design pattern by injecting the IUnitOfWork class. Now, we will use its instance for the repositories and any method we need. The code is given below.

```cs
 public class AccountService : IAccountsService
    {
        private readonly IMapper _mapper;
        // Instead of DIing BBContext directly and perferming Data operations in Service layer
        // we will inject UOW here that will have access to all the repositories and we dont have to injext indivisual repos.
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task OpenAccount(AccountRequestDTO accountRequest)
        {
            // If the user with the same User ID is already in teh system we simply set the userId forign Key of Account with it else 
            // first we create that user and then use it's ID.

            var user = await _unitOfWork.UserRepository.GetAsync(accountRequest.User.Id);
            if (user == null)
            {
                await _unitOfWork.UserRepository.AddAsync(accountRequest.User);
                accountRequest.UserId = accountRequest.User.Id;
            }
            else
            {
                accountRequest.UserId = user.Id;
            }

            var account = _mapper.Map<Account>(accountRequest);

            // Setting up ID of new incoming Account to be created.
            account.Id = Guid.NewGuid().ToString();
            // Once User ID forigen key and Account ID Primary Key is set we add the new accoun in Accounts.
            await this._unitOfWork.AccountRepository.AddAsync(account);
            // Once everything in place we make the Database call.
            await this._unitOfWork.CommitAsync();
        }
    }
```
## Step 10: Add Repositories and UOW as a Service in Web Builder
We will add the UOW and repositories in the `Program.cs` file web application builder as a service. **AddScoped** will keep *one instance of the type per http session*. We have DI of the contracts with their implementations in a service. Add the given lines in a file 
```cs
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

//Setting up DI of generic repos for UOW
builder.Services.AddScoped(typeof(IRepository<>), typeof(SQLRepository<>));
```

## **Conclusion**
We have implemented a generic repository of template type(TEntity) and unit of work design pattern to manage the repositories and our database context. We have also implemented specialized repository of specific type.  