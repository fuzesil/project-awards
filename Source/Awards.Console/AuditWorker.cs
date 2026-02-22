namespace Awards
{
    using System;
    using Awards.Logic;
    using Awards.Repository;

    /// <summary>
    /// This class enables the use of Audit Logic operations.
    /// </summary>
    public sealed class AuditWorker
    {
        private static AuditWorker singletonProgW;
        private readonly ExternalLogic externalAudit;
        private readonly InternalLogic internalAudit;

        private AuditWorker(Microsoft.EntityFrameworkCore.DbContext context)
        {
            var brands = new BrandRepository(context);
            var countries = new CountryRepository(context);
            var expertgroups = new ExpertGroupRepository(context);
            var members = new MemberRepository(context);
            var products = new ProductRepository(context);

            this.externalAudit = new ExternalAuditLogic(1, brands, products, expertgroups);
            this.internalAudit = new InternalAuditLogic(1, members, countries, expertgroups);
        }

        /// <summary>
        /// Gets the singleton instance of the class and calls the constructor.
        /// </summary>
        /// <param name="context">Database context needed to initialise logic classes.</param>
        /// <returns>A <see cref="AuditWorker"/> instance.</returns>
        public static AuditWorker Initialize(Microsoft.EntityFrameworkCore.DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (singletonProgW == null)
            {
                singletonProgW = new AuditWorker(context);
            }

            return singletonProgW;
        }

        /// <summary>
        /// Prints a list of members that are/aren't located in a capital city (separately) and the numbers of each.
        /// </summary>
        public void MembersInCapitals()
        {
            this.internalAudit.ListMembersInCapitalCity(true).PrintToConsole("Members IN a capital city");
            this.internalAudit.ListMembersInCapitalCity(false).PrintToConsole("Members OUT OF a capital city");
        }

        /// <summary>
        /// Asynchronously prints a list of members that are/aren't located in a capital city (separately) and the numbers of each.
        /// </summary>
        public void MembersInCapitalsAsync()
        {
            this.internalAudit.ListMembersInCapitalCityAsync(true).Result.PrintToConsole("Members IN a capital city");
            this.internalAudit.ListMembersInCapitalCityAsync(false).Result.PrintToConsole("Members OUT OF a capital city");
        }

        /// <summary>
        /// Prints the average Purchase Price Parity / Capita and the above-average countries.
        /// </summary>
        public void AverageCountries()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("The average of PPP/C is ");
            Console.ResetColor();
            Console.WriteLine(this.internalAudit.GetAveragePPP());
            this.internalAudit.ListCountriesAboveAveragePPP(out int count).PrintToConsole($"{count} countries with above-average PPP/C");
        }

        /// <summary>
        /// Prints the top 'N' most awarded brands and their (awarded) products.
        /// </summary>
        public void ListTopBrandsAndProducts()
        {
            Console.WriteLine("What should be the rank 'N' as in 'the top N brands' ?");
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int topn))
            {
                this.externalAudit.TopN = topn;
                this.externalAudit.ListTopBrands().PrintToConsole("(Name of brand, Count of awards)");
                this.externalAudit.GetProductsByBrandId(out int count).PrintToConsole($"{count} awarded products from top brands");
            }
            else
            {
                Console.WriteLine("Rank parsing failed!\t(Press a key.)");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Asynchronously prints the top 'N' most awarded brands.
        /// </summary>
        public void ListTopBrandsAsync()
        {
            Console.WriteLine("What should be the rank 'N' as in 'the top N brands' ?");
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int topn))
            {
                this.externalAudit.TopN = topn;
                this.externalAudit.ListTopBrandsAsync().Result.PrintToConsole("(Name of brand, Count of awards)");
            }
            else
            {
                Console.WriteLine("Rank parsing failed!\t(Press a key.)");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Prints the 'Brands' table.
        /// </summary>
        public void ListAllBrands()
        {
            this.externalAudit.ListAllBrands(out int count).PrintToConsole($"{count} records in 'Countries' table");
        }

        /// <summary>
        /// Print the 'Countries' table.
        /// </summary>
        public void ListAllCountries()
        {
            this.internalAudit.ListAllCountries(out int count).PrintToConsole($"{count} records in 'Countries' table");
        }

        /// <summary>
        /// Print the 'ExpertGrops' table.
        /// </summary>
        public void ListAllExpertgroupsInternally()
        {
            this.internalAudit.ListAllExpertgroups(out int count).PrintToConsole($"{count} records in 'ExpertGroups' table");
        }

        /// <summary>
        /// Print the 'ExpertGrops' table.
        /// </summary>
        public void ListAllExpertgroupsExternally()
        {
            this.externalAudit.ListAllExpertgroups(out int count).PrintToConsole($"{count} records in 'ExpertGroups' table");
        }

        /// <summary>
        /// Print the 'Members' table.
        /// </summary>
        public void ListAllMembers()
        {
            this.internalAudit.ListAllMembers(out int count).PrintToConsole($"{count} records in 'Members' table");
        }

        /// <summary>
        /// Prints the 'Products' table.
        /// </summary>
        public void ListAllProducts()
        {
            this.externalAudit.ListAllProducts(out int count).PrintToConsole($"{count} records in 'Members' table");
        }

        /// <summary>
        /// Fetches one record from the 'Brands' table.
        /// </summary>
        public void FetchOneBrand()
        {
            Console.WriteLine("\nEnter ID or Name :");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    Console.WriteLine(this.externalAudit.GetOneBrand(id));
                }
                else
                {
                    Console.WriteLine(this.externalAudit.GetOneBrand(-1, userInput));
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Fetches one record from the 'Countries' table.
        /// </summary>
        public void FetchOneCountry()
        {
            Console.WriteLine("\nEnter ID or Name :");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    Console.WriteLine(this.internalAudit.GetOneCountry(id));
                }
                else
                {
                    Console.WriteLine(this.internalAudit.GetOneCountry(-1, userInput));
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Fetches one record from the 'ExpertGroups' table.
        /// </summary>
        public void FetchOneEGExternally()
        {
            Console.WriteLine("\nEnter ID or Name :");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    Console.WriteLine(this.externalAudit.GetOneExpertGroup(id));
                }
                else
                {
                    Console.WriteLine(this.externalAudit.GetOneExpertGroup(-1, userInput));
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Fetches one record from the 'ExpertGroups' table.
        /// </summary>
        public void FetchOneEGInternally()
        {
            Console.WriteLine("\nEnter ID or Name :");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    Console.WriteLine(this.internalAudit.GetOneExpertGroup(id));
                }
                else
                {
                    Console.WriteLine(this.internalAudit.GetOneExpertGroup(-1, userInput));
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Fetches one record from the 'Members' table.
        /// </summary>
        public void FetchOneMember()
        {
            Console.WriteLine("\nEnter ID or Name :");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    Console.WriteLine(this.internalAudit.GetOneMember(id));
                }
                else
                {
                    Console.WriteLine(this.internalAudit.GetOneMember(-1, userInput));
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Fetches one record from the 'Products' table.
        /// </summary>
        public void FetchOneProduct()
        {
            Console.WriteLine("\nEnter ID or Name :");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    Console.WriteLine(this.externalAudit.GetOneProduct(id));
                }
                else
                {
                    Console.WriteLine(this.externalAudit.GetOneProduct(-1, userInput));
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Prints the list of members in a given country.
        /// </summary>
        public void CountMembersInCountry()
        {
            Console.WriteLine("Input the ID or Name of the country of interest:");
            Console.WriteLine("\nEnter ID or Name :");
            string userInput = Console.ReadLine();
            try
            {
                bool isInt = int.TryParse(userInput, out int id);
                if (isInt)
                {
                    this.internalAudit.CountMembersInCountry(id, out int count).PrintToConsole($"{count} members in {this.internalAudit.GetOneCountry(id).Name}");
                }
                else
                {
                    var temp = this.internalAudit.GetOneCountry(-1, userInput);
                    this.internalAudit.CountMembersInCountry(temp.CountryID, out int count).PrintToConsole($"{count} members in {temp.Name}");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Prints the list of members in a given expert group.
        /// </summary>
        public void GetRichestMemberInExpertGroup()
        {
            this.internalAudit.GetRichestMemberInExpertGroup().PrintToConsole($"Richest members in expert group");
        }

        /// <summary>
        /// Asynchronously prints the list of members in a given expert group.
        /// </summary>
        public void GetRichestMemberInExpertGroupAsync()
        {
            this.internalAudit.GetRichestMemberInExpertGroupAsync().Result.PrintToConsole($"Richest members in expert group");
        }

        /// <summary>
        /// Prints the most expensive product for each expert group.
        /// </summary>
        public void MaxPriceProductInEveryEG()
        {
            this.externalAudit.GetMaxPriceProdInEveryEG().PrintToConsole("Max Price in Each Category");
        }

        /// <summary>
        /// Asynchronously prints the most expensive product for each expert group.
        /// </summary>
        public void GetMaxPriceProdInEveryEGAsync()
        {
            this.externalAudit.GetMaxPriceProdInEveryEGAsync().Result.PrintToConsole("Max Price in Each Category");
        }
    }
}