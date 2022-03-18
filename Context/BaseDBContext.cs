using Armut.Model;

public class BaseDBContext : DbContext
{
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<Gender>? Genders { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<UserRole>? UserRoles { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<SubCategory>? SubCategories { get; set; }
    public DbSet<Activity>? Activities { get; set; }
    public DbSet<ActivityTimeTable>? ActivityTimeTables { get; set; }

    //public DbSet<PriceTable>? PriceTables { get; set; }
    public DbSet<Ranking>? Rankings { get; set; }
    public DbSet<WishedActivity>? WishedActivites { get; set; }

    //public DbSet<AttendedActivity>? AttendedActivities { get; set; }
    //public DbSet<UpComingActivity>? UpComingActivities { get; set; }
    public DbSet<UserActivityTimeTable>? UserActivityTimeTables { get; set; }


    public DbSet<Country>? Countries { get; set; }
    public DbSet<City>? Cities { get; set; }
    public DbSet<State>? States { get; set; }
    public DbSet<District>? Districts { get; set; }
    public DbSet<Neighbourhood>? Neighbourhoods { get; set; }
    public DbSet<Address>? Addresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
        dbContextOptionsBuilder.UseMySql("server=localhost;database=armut;user=root;port=3306;password=toortoor", serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email);
            entity.Property(e => e.Password);
            entity.Property(e => e.Visibility);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username);
            entity.Property(e => e.FirstName);
            entity.Property(e => e.LastName);
            entity.Property(e => e.Age);
            entity.Property(e => e.PhoneNumber);
            entity.Property(e => e.ProfilePhoto);
            entity.HasOne(e=>e.Account).WithOne().HasForeignKey("User","AccountId");
            entity.HasOne(e=>e.Gender).WithMany(e=>e.Users).HasForeignKey(e=>e.GenderId);
            entity.HasMany(e=>e.Roles).WithMany(e=>e.Users).UsingEntity(j=>j.ToTable("User_Role")); //sor hangisi
            entity.HasMany(e=>e.RecordedActivities).WithMany(e=>e.AllAttendantUsers).UsingEntity(j=>j.ToTable("User_Activity_Records"));
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
        });

        modelBuilder.Entity<UserRole>().HasKey(x =>new { x.UserId, x.RoleId });
        modelBuilder.Entity<UserRole>().HasOne<User>(x => x.User).WithMany(x => x.UserRoles)
        .HasForeignKey(x => x.UserId);
        modelBuilder.Entity<UserRole>().HasOne<Role>(x => x.Role).WithMany(x => x.UserRoles)
        .HasForeignKey(x => x.RoleId);


        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type);
        });

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.Description);
            entity.Property(e => e.Image);
            entity.Property(e => e.Visibility);
            entity.Property(e => e.CreatedTime);
            entity.Property(e => e.Price);
            entity.Property(e => e.Point);
            entity.HasOne(e => e.SubCategory).WithMany(e => e.Activities).HasForeignKey(e => e.SubCategoryId);
            entity.HasOne(e => e.Address).WithMany(e => e.Activities).HasForeignKey(e => e.AddressId);
            entity.HasOne(e=>e.OwnerUser).WithMany(e => e.CreatedActivities).HasForeignKey(e => e.OwnerUserId); 
        });

        modelBuilder.Entity<ActivityTimeTable>(entity => //sor4 many:many ara tablosu olcak mı --bir activity birden fazla time table sahip ama bir timetableIn birden fazla activity olabilir mi????
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartDate);
            entity.Property(e => e.EndDate);
            entity.Property(e => e.Quota);
            entity.Property(e => e.NumberOfAttendants);
            entity.Property(e => e.IsUpComing);
             entity.Property(e => e.Visibility);
            /*
            entity.HasMany(e => e.AttendantUsers).WithMany(e => e.Activities).UsingEntity<UserActivityTimeTable>(
                   x => x.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId),
                   x => x.HasOne(x => x.ActivityTimeTable).WithMany().HasForeignKey(x => x.ActivityTimeTableId)
            );
            */
            entity.HasOne(e=>e.Activity).WithMany(e=>e.TimeTable).HasForeignKey(e=>e.ActivityId);
        });

        //bir userın birden fazla timetableı olabilir ve bir timetableın da birden fazla katılımcı user'ı olabilir
        //sor1 bunun için de normal bir şekilde servis-repository yazıp sorgu yapabilir miyim???
        modelBuilder.Entity<UserActivityTimeTable>().HasKey(x => new { x.UserId, x.ActivityTimeTableId });
        modelBuilder.Entity<UserActivityTimeTable>().HasOne<User>(x => x.User).WithMany(x => x.UserActivityTimeTables)
        .HasForeignKey(x => x.UserId);
        modelBuilder.Entity<UserActivityTimeTable>().HasOne<ActivityTimeTable>(x => x.ActivityTimeTable).WithMany(x => x.AttendantUsers)
        .HasForeignKey(x => x.ActivityTimeTableId);
        modelBuilder.Entity<UserActivityTimeTable>().Property(e => e.IsUpComing);

        //bir user birden fazla activiyeyi isteyebilir ve bir aktivite birden fazla user tarafından istenebilir??
        //sor2 
        modelBuilder.Entity<WishedActivity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User).WithMany(e => e.WishedActivities).HasForeignKey(e => e.UserId);
            entity.HasOne(e => e.Activity).WithMany(e => e.WishingUsers).HasForeignKey(e=>e.ActivityId);
        });

        //bir user birden fazla aktiviteye puan verebilir ve bir aktivite birden fazla puan alabilir??
        //sor3
        modelBuilder.Entity<Ranking>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Value);
            entity.HasOne(e=>e.Activity).WithMany(e=>e.Ranking).HasForeignKey(e=>e.ActivityId);
            entity.HasOne(e=>e.RatingUser).WithMany(e=>e.GivenRankings).HasForeignKey(e=>e.RatingUserId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.Visibility);
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.Visibility);
            entity.HasOne(e=>e.Category).WithMany(e=>e.SubCategories).HasForeignKey(e=>e.CategoryId);
        });




/*
        modelBuilder.Entity<PriceTable>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount);
            entity.Property(e => e.Discount);
            entity.HasOne(e=>e.Activity).WithMany(e=>e.PriceTable).HasForeignKey(e=>e.ActivityId);
        }); 
        aktivtytimetable entitye ekle:
        entity.HasOne(e=>e.Price).WithOne(e=>e.ActivityTimeTable).HasForeignKey("ActivityTimeTable","PriceTableId");
*/



        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.OpenAddress1);
            entity.Property(e => e.OpenAddress2);
            entity.Property(e => e.Visibility);
            entity.HasOne(e=>e.Country).WithMany(e=>e.Addresses).HasForeignKey(e=>e.CountryId);
            entity.HasOne(e=>e.State).WithMany(e=>e.Addresses).HasForeignKey(e=>e.StateId);
            entity.HasOne(e=>e.City).WithMany(e=>e.Addresses).HasForeignKey(e=>e.CityId);
            entity.HasOne(e=>e.District).WithMany(e=>e.Addresses).HasForeignKey(e=>e.DistrictId);
            entity.HasOne(e=>e.Neighbourhood).WithMany(e=>e.Addresses).HasForeignKey(e=>e.NeighbourhoodId);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.HasStates);
            entity.Property(e => e.Visibility);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.Visibility);
            entity.HasOne(e=>e.Country).WithMany(e=>e.States).HasForeignKey(e=>e.CountryId);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.Visibility);
            entity.HasOne(e=>e!.State).WithMany(e=>e!.Cities).HasForeignKey(e=>e.StateId);
            entity.HasOne(e=>e.Country).WithMany(e=>e.Cities).HasForeignKey(e=>e.CountryId);
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.Visibility);
            entity.HasOne(e=>e.City).WithMany(e=>e.Districts).HasForeignKey(e=>e.CityId);
        });

        modelBuilder.Entity<Neighbourhood>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name);
            entity.Property(e => e.Visibility);
            entity.HasOne(e=>e.District).WithMany(e=>e.Neighbourhoods).HasForeignKey(e=>e.DistrictId);
        });




        AddDataToAccount(modelBuilder);
        AddDataToUser(modelBuilder);
        AddDataToGender(modelBuilder);
        AddDataToRole(modelBuilder);
        AddDataToUserRole(modelBuilder);
        AddDataToActivity(modelBuilder);
        AddDataToActivityTimeTable(modelBuilder);
        AddDataToCategory(modelBuilder);
        AddDataToSubCategory(modelBuilder);
        AddDataToAddress(modelBuilder);
    }

    void AddDataToAccount(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasData(
                 new Account
                 {
                     Id = 1,
                     Email = "melek@gmail.com",
                     Password = "1234567",
                     Visibility = true
                 },
                 new Account
                 {
                     Id = 2,
                     Email = "selcen@gmail.com",
                     Password = "1235467",
                     Visibility = true
                 }
             );
    }
    void AddDataToUser(ModelBuilder modelBuilder)
    {

    }
    void AddDataToGender(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Gender>().HasData(
                 new Gender
                 {
                     Id = 1,
                     Type = "Female"
                 },
                 new Gender
                 {
                     Id = 2,
                     Type = "Male"
                 },
                 new Gender
                 {
                     Id = 3,
                     Type = "Undefined"
                 }
             );
    }
    void AddDataToRole(ModelBuilder modelBuilder)
    {
        
         modelBuilder.Entity<Role>().HasData(
                 new Role
                 {
                     Id = 1,
                     Name = "User"
                 },
                 new Role
                 {
                     Id = 2,
                     Name = "Admin"
                 }
         );
    }
    void AddDataToUserRole(ModelBuilder modelBuilder)
    {

    }
    void AddDataToActivity(ModelBuilder modelBuilder)
    {
        
    }
    void AddDataToActivityTimeTable(ModelBuilder modelBuilder)
    {
        
    }
    void AddDataToCategory(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Spor",
                    Visibility = true
                },
                new Category
                {
                    Id = 2,
                    Name = "Dans",
                    Visibility = true
                }
        );
    }
    void AddDataToSubCategory(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory
                {
                    Id = 1,
                    Name = "Kick Box",
                    CategoryId = 1,
                    Visibility = true
                },
                new SubCategory
                {
                    Id = 2,
                    Name = "Masa tenisi",
                    CategoryId = 1,
                    Visibility = true
                },
                new SubCategory
                {
                    Id = 3,
                    Name = "Zumba dansı",
                    CategoryId = 2,
                    Visibility = true
                },
                new SubCategory
                {
                    Id = 4,
                    Name = "Cha Cha dansı",
                    CategoryId = 2,
                    Visibility = true
                }
        );
    }
    void AddDataToAddress(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Turkey",
                    Visibility = true,
                    HasStates = false
                },
                new Country
                {
                    Id = 2,
                    Name = "USA",
                    Visibility = true,
                    HasStates = true
                }
        );
         modelBuilder.Entity<State>().HasData(
                new State
                {
                    Id = 1,
                    Name = "California",
                    Visibility = true,
                    CountryId = 2
                },
                new State
                {
                    Id = 2,
                    Name = "Teksas",
                    Visibility = true,
                    CountryId = 2
                }
        );
         modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    Name = "Istanbul",
                    Visibility = true,
                    CountryId = 1
                },
                new City
                {
                    Id = 2,
                    Name = "Ankara",
                    Visibility = true,
                    CountryId = 1
                },
                new City
                {
                    Id = 3,
                    Name = "New Orleans",
                    Visibility = true,
                    CountryId = 2,
                    StateId = 1
                }
        );
        modelBuilder.Entity<District>().HasData(
                new District
                {
                    Id = 1,
                    Name = "Umraniye",
                    Visibility = true,
                    CityId = 1
                },
                new District
                {
                    Id = 2,
                    Name = "Kartal",
                    Visibility = true,
                    CityId = 1
                },
                new District
                {
                    Id = 3,
                    Name = "Ulus",
                    Visibility = true,
                    CityId = 2
                },
                new District
                {
                    Id = 4,
                    Name = "Eagle",
                    Visibility = true,
                    CityId = 3
                }
        );
         modelBuilder.Entity<Neighbourhood>().HasData(
                new Neighbourhood
                {
                    Id = 1,
                    Name = "Atakent",
                    Visibility = true,
                    DistrictId = 1
                },
                new Neighbourhood
                {
                    Id = 2,
                    Name = "KartalinMahallesi1",
                    DistrictId = 2,
                    Visibility = true,
                },
                new Neighbourhood
                {
                    Id = 3,
                    Name = "UlusunMahallesi1",
                    Visibility = true,
                    DistrictId = 3
                },
                new Neighbourhood
                {
                    Id = 4,
                    Name = "EagleinMahallesi1",
                    Visibility = true,
                    DistrictId = 4
                }
        );
        modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    Name = "Evim",
                    DistrictId = 1,
                    NeighbourhoodId=1,
                    CountryId=1,
                    CityId=1,
                    Visibility = true,
                    OpenAddress1="emrah sk. no:64"
                },
                new Address
                {
                    Id = 2,
                    Name = "Isyerim",
                    DistrictId = 2,
                    CountryId=1,
                    CityId=1,
                    NeighbourhoodId=2,
                    Visibility = true,
                    OpenAddress1="cicek sk. no:14"
                },
                new Address
                {
                    Id = 3,
                    Name = "Adresim",
                    DistrictId = 3,
                    CountryId=1,
                    CityId=2,
                    NeighbourhoodId=3,
                    OpenAddress1="yıldıran sk. no:6 , kat : 3, daire : 5",
                    Visibility = true,
                    OpenAddress2="okulun arkasi marketin önü"
                },
                new Address
                {
                    Id = 4,
                    Name = "My Address",
                    DistrictId = 4,
                    CountryId=2,
                    CityId=3,
                    NeighbourhoodId=4,
                    StateId = 1,
                    Visibility = true,
                    OpenAddress1="even street no:4 "
                }
        );
    }

}