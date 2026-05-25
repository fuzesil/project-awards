namespace Awards
{
    using System;
    using Awards.Data;

    /// <summary>
    /// The class that contains the entry point of execution, the <see cref="Main"/> method.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "EISA Awards Auditing Application - by QKNWZ1";
            Console.WriteLine("** Welcome! **\nPlease wait, the initialisation process has begun.");
            AwardsDbContext eisa = new Data.AwardsDbContext();
            AuditWorker auditor = AuditWorker.Initialize(eisa);
            AdminRunner admin = AdminRunner.Initialize(eisa);

            ConsoleTools.ConsoleMenu adminMenu = new ConsoleTools.ConsoleMenu(args, 1)
                 .Add("Add a new brand to the 'Brands' table.", admin.AddNewBrand)
                 .Add("Add a new country to the 'Countries' table.", admin.AddNewCountry)
                 .Add("Add a new expert group to the 'ExpertGroups' table.", admin.AddNewEG)
                 .Add("Add a new member to the 'Members' table.", admin.AddNewMember)
                 .Add("Add a new product to the 'Products' table.", admin.AddNewProduct)
                 .Add("Remove a brand from the 'Brands' table.", admin.RemoveBrand)
                 .Add("Remove a country from the 'Countries' table.", admin.RemoveCountry)
                 .Add("Remove an expert group from the 'ExpertGroups' table.", admin.RemoveEG)
                 .Add("Remove a member from the 'Members' table.", admin.RemoveMember)
                 .Add("Remove a product from the 'Products' table.", admin.RemoveProduct)
                 .Add("Change the web home page of a selected Brand", admin.ChangeBrandHomePage)
                 .Add("Change the PPP/C of a selected Country", admin.ChangeCountryPPP)
                 .Add("Change the name of a selected ExpertGroup", admin.ChangeEGName)
                 .Add("Change the name of a selected Member", admin.ChangeMemberName)
                 .Add("Change the price of a selected product", admin.ChangeProductPrice)
                 .Add(" - Show brands and members that are located close to each other.", admin.ListBrandsAndMembersAtSameAddress)
                 .Add(" - Show brands and members that are located close to each other (ASYNC).", admin.ListBrandsAndMembersAtSameAdressAsync)
                 .Add("<< Main menu", ConsoleTools.ConsoleMenu.Close)
                 .Configure(config =>
                 {
                     config.Selector = "--> ";
                     config.EnableFilter = true;
                     config.EnableWriteTitle = true;
                     config.Title = "EISA Database Administrators' menu";
                 });

            ConsoleTools.ConsoleMenu internalauditMenu = new ConsoleTools.ConsoleMenu(args, 1)
                .Add("Print the 'Countries' table.", auditor.ListAllCountries)
                .Add("Fetch one record from the 'Countries' table.", auditor.FetchOneCountry)
                .Add("Print the 'ExpertGroups' table.", auditor.ListAllExpertgroupsInternally)
                .Add("Fetch one record from the 'ExpertGroups' table.", auditor.FetchOneEGInternally)
                .Add("Print the 'Members' table.", auditor.ListAllMembers)
                .Add("Fetch one record from the 'Members' table.", auditor.FetchOneMember)
                .Add(" - Show the avg. Purchase Price Parity / Capita & the above-average countries", auditor.AverageCountries)
                .Add(" - Show members that are/aren't located in a capital city (separately)", auditor.MembersInCapitals)
                .Add(" - Show members from the given country", auditor.CountMembersInCountry)
                .Add(" - Show the richest members (by its country) in each expert group", auditor.GetRichestMemberInExpertGroup)
                .Add(" - Show the richest members (by its country) in each expert group (ASYNC)", auditor.GetRichestMemberInExpertGroupAsync)
                .Add(" - Show members that are/aren't located in a capital city (separately) (ASYNC)", auditor.MembersInCapitalsAsync)
                .Add("<< Main menu", ConsoleTools.ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.EnableWriteTitle = true;
                    config.Title = "EISA Internal Audits' menu";
                });

            ConsoleTools.ConsoleMenu externalauditMenu = new ConsoleTools.ConsoleMenu(args, 1)
                .Add("Print the 'Brands' table.", auditor.ListAllBrands)
                .Add("Fetch one record from the 'Brands' table.", auditor.FetchOneBrand)
                .Add("Print the 'ExpertGroups' table.", auditor.ListAllExpertgroupsExternally)
                .Add("Fetch one record from the 'ExpertGroups' table.", auditor.FetchOneEGExternally)
                .Add("Print the 'Products' table.", auditor.ListAllProducts)
                .Add("Fetch one record from the 'Products' table.", auditor.FetchOneProduct)
                .Add(" - Show the most awarded brands & their (awarded) products", auditor.ListTopBrandsAndProducts)
                .Add(" - Show the most expensive product for each expert group", auditor.MaxPriceProductInEveryEG)
                .Add(" - Show the most awarded brands (ASYNC)", auditor.ListTopBrandsAsync)
                .Add(" - Show the most expensive product for each expert group (ASYNC)", auditor.GetMaxPriceProdInEveryEGAsync)
                .Add("<< Main menu", ConsoleTools.ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.EnableWriteTitle = true;
                    config.Title = "EISA External Audits' menu";
                });

            ConsoleTools.ConsoleMenu mainMenu = new ConsoleTools.ConsoleMenu(args, 0)
                .Add("Enter the Internal Audits' menu, tables: ExpertGroups, Countries, Members", internalauditMenu.Show)
                .Add("Enter the External Audits' menu, tables: ExpertGroups, Brands, Products", externalauditMenu.Show)
                .Add("Enter the Database Administrators' menu: Insert, Update, Remove in all tables", () =>
                {
                    if (Auth())
                    {
                        adminMenu.Show();
                    }
                })
                .Add("Exit menu", (thisMenu) =>
                {
                    if (AreYouSure())
                    {
                        thisMenu.CloseMenu();
                    }
                })
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableWriteTitle = true;
                    config.Title = "Main menu";
                });

            mainMenu.Show();
            eisa.Dispose();
        }

        private static bool AreYouSure()
        {
            Console.WriteLine("Proceed to Exit?\t[1] [Y]es [I]gen [O]ui [S]i [J]a\t(case insensitive)");
            System.Collections.Generic.List<char> replies = new System.Collections.Generic.List<char> { '1', 'Y', 'I', 'O', 'S', 'J' };
            char answer = Console.ReadKey().KeyChar;
            answer = char.ToUpperInvariant(answer);
            if (replies.Contains(answer))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n ** Thank you for using this application! **");
                System.Threading.Thread.Sleep(1000);
                Console.ResetColor();
                return true;
            }

            return false;
        }

        private static bool Auth()
        {
            Console.WriteLine("Please authenticate to access the Administrators' Menu.");
            Console.WriteLine(" (Input \"ADMIN\" without quotes) ");
            string key = Console.ReadLine();
            if (key == "ADMIN")
            {
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Authentication FAILED!");
            System.Threading.Thread.Sleep(1000);
            Console.ResetColor();
            return false;
        }
    }
}
