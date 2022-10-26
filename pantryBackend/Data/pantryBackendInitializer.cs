using pantryBackend.Models;

namespace pantryBackend.Data
{
    public class pantryBackendInitializer
    {
        public static void Initialize1(pantryDbContext pantryItemscontext)
        {
            pantryItemscontext.Database.EnsureCreated();
            if (pantryItemscontext.pantryItems.Any())
                return;
            pantryItems[] pantryItems = new[]
            {
            new pantryItems{iName="pasta",weight=56,tCalories=82,image="https://photos.google.com/photo/AF1QipNqmQ3sRxSj0ceFOvnyZfv_wmAjO0DrX5cnFOPA"},
            new pantryItems{iName="Rice",weight=195,tCalories=757,image="https://photos.google.com/photo/AF1QipP-P005JKHS-7YTLEyGyO5hQFcRGDqk4ORuXEIZ"},
            new pantryItems{iName="Couscous",weight=173,tCalories=650,image="https://photos.google.com/photo/AF1QipPC9kz3SC6Z--VLOVxE0WepgqNQ0Vs9ZZHA_2nS"},
            new pantryItems{iName="Corn Oil",weight=15,tCalories=120,image="https://photos.google.com/photo/AF1QipMHSCn0woZVn6yH7AxFHkNtH_D3UY_ucdevWTej"}

        };
            foreach (var items in pantryItems)
            {
                pantryItemscontext.pantryItems.Add(items);
            }
            pantryItemscontext.SaveChanges();
        }
        public static void Initialize2(pantryDbContext recipecontext)
        {
            recipecontext.Database.EnsureCreated();
            if (recipecontext.recipe.Any())
                return;
            recipes[] recipes = new[]
            {
            new recipes{rName="rec1",rImage="https://photos.google.com/photo/AF1QipNqmQ3sRxSj0ceFOvnyZfv_wmAjO0DrX5cnFOPA",rSteps="1,2,3"},
            new recipes{rName="rec2",rImage="https://photos.google.com/photo/AF1QipP-P005JKHS-7YTLEyGyO5hQFcRGDqk4ORuXEIZ,",rSteps="1,2,3"},
            new recipes{rName="rec3",rImage="https://photos.google.com/photo/AF1QipPC9kz3SC6Z--VLOVxE0WepgqNQ0Vs9ZZHA_2nS",rSteps="1,2,3"},
            new recipes{rName="rec 4",rImage="https://photos.google.com/photo/AF1QipMHSCn0woZVn6yH7AxFHkNtH_D3UY_ucdevWTej", rSteps = "1,2,3"},

        };
            foreach (var recipe in recipes)
            {
                recipecontext.recipe.Add(recipe);
            }
            recipecontext.SaveChanges();
        }
        public static void Initialize3(pantryDbContext usercontext)
        {
            usercontext.Database.EnsureCreated();
            if (usercontext.users.Any())
                return;
            Users[] users = new[]
            {
            new Users{firstName="kokou", password="pass1"},
            new Users{firstName="julia", password="pass2"},
            new Users{firstName="klenam", password="pass3"},

        };
            foreach (var user in users)
            {
                usercontext.users.Add(user);
            }
            usercontext.SaveChanges();
        }
    }
}
