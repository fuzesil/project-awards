namespace Awards
{
    using System;
    using Awards.Logic;
    using Awards.Repository;

    /// <summary>
    /// Acts as a bridge between <see cref="Logic.AdminLogic"/> and <see cref="System.Console"/>.
    /// </summary>
    public class AdminRunner
    {
        private static AdminRunner singletonAR;
        private readonly AdminLogic adminLogic;

        private AdminRunner(Microsoft.EntityFrameworkCore.DbContext context)
        {
            this.adminLogic = new AdminLogic(
                new BrandRepository(context),
                new CountryRepository(context),
                new ExpertGroupRepository(context),
                new MemberRepository(context),
                new ProductRepository(context));
        }

        /// <summary>
        /// Initialises the singleton instance of the <see cref="AdminRunner"/> helper class.
        /// </summary>
        /// <param name="context">Database context needed to initialise logic classes.</param>
        /// <returns>The only one instance of this class.</returns>
        public static AdminRunner Initialize(Microsoft.EntityFrameworkCore.DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (singletonAR == null)
            {
                singletonAR = new AdminRunner(context);
            }

            return singletonAR;
        }

        /// <summary>
        /// Prints Brands and Members at similar places.
        /// </summary>
        public void ListBrandsAndMembersAtSameAddress()
        {
            this.adminLogic.ListBrandsAndMembersAtSameAdress().PrintToConsole("Brands and Members in close proximity");
        }

        /// <summary>
        /// Asynchronously prints Brands and Members at similar places.
        /// </summary>
        public void ListBrandsAndMembersAtSameAdressAsync()
        {
            this.adminLogic.ListBrandsAndMembersAtSameAdressAsync().Result.PrintToConsole("Brands and Members in close proximity");
        }

        /// <summary>
        /// Adds a new brand to the 'Brands' table.
        /// </summary>
        public void AddNewBrand()
        {
            string[] input = { "name", "country", "address", "hompage" };
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine("Input the required data for " + input[i]);
                input[i] = Console.ReadLine();
            }

            this.adminLogic.InsertBrand(input[0], input[1], input[2], input[3]);
        }

        /// <summary>
        /// Adds a new country to the 'Countries' table.
        /// </summary>
        public void AddNewCountry()
        {
            string[] input = { "name", "capital city", "calling code", "PPP/C in USD" };
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine("Input the required data for " + input[i]);
                input[i] = Console.ReadLine();
            }

            this.adminLogic.InsertCountry(
                input[0],
                input[1],
                int.Parse(input[2], System.Globalization.NumberFormatInfo.InvariantInfo),
                int.Parse(input[3], System.Globalization.NumberFormatInfo.InvariantInfo));
        }

        /// <summary>
        /// Adds a new expert group to the 'ExpertGroups' table.
        /// </summary>
        public void AddNewEG()
        {
            string name = "name";
            Console.WriteLine("Input the required data for " + name);
            name = Console.ReadLine();
            this.adminLogic.InsertEG(name);
        }

        /// <summary>
        /// Adds a new member to the 'Members' table.
        /// </summary>
        public void AddNewMember()
        {
            string[] input = { "name", "website", "publisher", "editor", "phone number without +/00/country code", "country", "office", "EGname" };
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine("Input the required data for " + input[i]);
                input[i] = Console.ReadLine();
            }

            this.adminLogic.InsertMember(input[0], input[1], input[2], input[3], input[4], input[5], input[6], input[7]);
        }

        /// <summary>
        /// Adds a new product to the 'Products' table.
        /// </summary>
        public void AddNewProduct()
        {
            string[] input = { "name", "brand name", "price in USD", "estimated lifetime in Years, rounded", "date of launch YYYY-MM-DD", "name of the expert group", "category" };
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine("Input the required data for " + input[i]);
                input[i] = Console.ReadLine();
            }

            this.adminLogic.InsertProduct(
                input[0],
                input[1],
                int.Parse(input[2], System.Globalization.NumberFormatInfo.InvariantInfo),
                int.Parse(input[3], System.Globalization.NumberFormatInfo.InvariantInfo),
                DateTime.Parse(input[4], System.Globalization.NumberFormatInfo.InvariantInfo),
                input[5],
                input[6]);
        }

        /// <summary>
        /// Remove a brand from the 'Brands' table.
        /// </summary>
        public void RemoveBrand()
        {
            Console.WriteLine("Input the ID of the brand to be removed.");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    this.adminLogic.RemoveBrand(id);
                }
                else
                {
                    Console.WriteLine("ID cannot be parsed.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Remove a country from the 'Countries' table.
        /// </summary>
        public void RemoveCountry()
        {
            Console.WriteLine("Input the ID of the country to be removed.");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    this.adminLogic.RemoveCountry(id);
                }
                else
                {
                    Console.WriteLine("ID cannot be parsed.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Removes an expert group from the 'ExpertGroups' table.
        /// </summary>
        public void RemoveEG()
        {
            Console.WriteLine("Input the ID of the expert group to be removed.");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    this.adminLogic.RemoveEG(id);
                }
                else
                {
                    Console.WriteLine("ID cannot be parsed.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Remove a member from the 'Members' table.
        /// </summary>
        public void RemoveMember()
        {
            Console.WriteLine("Input the ID of the member to be removed.");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    this.adminLogic.RemoveMember(id);
                }
                else
                {
                    Console.WriteLine("ID cannot be parsed.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Removes a product from the 'Products' table.
        /// </summary>
        public void RemoveProduct()
        {
            Console.WriteLine("Input the ID of the product to be removd.");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    this.adminLogic.RemoveProduct(id);
                }
                else
                {
                    Console.WriteLine("ID cannot be parsed.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Updates the name of the selected ExpertGroup record.
        /// </summary>
        public void ChangeEGName()
        {
            Console.WriteLine("\nEnter ID :");
            string userInput = Console.ReadLine();
            try
            {
                if (int.TryParse(userInput, out int id))
                {
                    Console.WriteLine("\nEnter new Name :");
                    userInput = Console.ReadLine();
                    this.adminLogic.ChangeEGName(id, userInput);
                }
                else
                {
                    Console.WriteLine("ID parsing failed!\t(Press a key.)");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Updates the web home page of the selected Brand record.
        /// </summary>
        public void ChangeBrandHomePage()
        {
            Console.WriteLine("\nEnter ID :");
            string userInput = Console.ReadLine();
            try
            {
                if (int.TryParse(userInput, out int id))
                {
                    Console.WriteLine("\nEnter new web Home Page :");
                    string userParam = Console.ReadLine();
                    this.adminLogic.ChangeBrandHomePage(id, userParam);
                }
                else
                {
                    Console.WriteLine("ID parsing failed!\t(Press a key.)");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Updates the PPP/C for the selected Country record.
        /// </summary>
        public void ChangeCountryPPP()
        {
            Console.WriteLine("\nEnter ID :");
            string userInput = Console.ReadLine();
            try
            {
                if (int.TryParse(userInput, out int id))
                {
                    Console.WriteLine("\nEnter new Price :");
                    userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out int newPPP))
                    {
                        this.adminLogic.ChangeCountryPPP(id, newPPP);
                    }
                    else
                    {
                        Console.WriteLine("New price parsing failed!\t(Press a key.)");
                    }
                }
                else
                {
                    Console.WriteLine("ID parsing failed!\t(Press a key.)");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Updates the new name of the selected Member record.
        /// </summary>
        public void ChangeMemberName()
        {
            Console.WriteLine("\nEnter ID :");
            string userInput = Console.ReadLine();
            try
            {
                if (int.TryParse(userInput, out int id))
                {
                    Console.WriteLine("\nEnter new Name :");
                    userInput = Console.ReadLine();
                    this.adminLogic.ChangeMemberName(id, userInput);
                }
                else
                {
                    Console.WriteLine("ID parsing failed!\t(Press a key.)");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Updates the price of a given product.
        /// </summary>
        public void ChangeProductPrice()
        {
            Console.WriteLine("\nEnter ID :");
            string userInput = Console.ReadLine();
            try
            {
                if (int.TryParse(userInput, out int id))
                {
                    Console.WriteLine("\nEnter new Price :");
                    userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out int newPrice))
                    {
                        this.adminLogic.ChangeProductPrice(id, newPrice);
                    }
                    else
                    {
                        Console.WriteLine("New price parsing failed!\t(Press a key.)");
                    }
                }
                else
                {
                    Console.WriteLine("ID parsing failed!\t(Press a key.)");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
