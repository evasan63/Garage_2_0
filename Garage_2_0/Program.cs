using System.Diagnostics.Metrics;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using static Garage_2_0.Utilities;

namespace Garage_2_0
{
    internal class Program
    {
        //***********************************************************************************
        // This program let you create a garage in which you can park and get vehicles of
        // different types (Car/Bus/Mc/Boat/Airplane). It is also possible to list all 
        // vehicles or a specific type of vehicles in the garage.
        //***********************************************************************************
        static void Main(string[] args)
        {

            Garage garage = null;
            //List <Garage> garage = null;
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("***     Välkommen till Evas Garage      **");
                Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
                Console.WriteLine("1. Skapa ett nytt garage");
                Console.WriteLine("2. Parkera ett fordon i garaget");
                Console.WriteLine("3. Hämta ut ett fordon ur garaget");
                Console.WriteLine("4. Lista alla fordon i garaget");
                Console.WriteLine("5. Lista specifika fordon i garaget");
                Console.WriteLine("6. Parkera lista av fordon i garaget");
                Console.WriteLine("0. Avsluta");
                Console.Write("Välj alternativ: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": // Create a new garage
                        garage = CreateGarage();
                        if (garage != null)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Nytt garage med totalt {garage.TotalNumberOfParkingLots} platser, " +
                                              $"varav {garage.NumberOfElectricParkingLots} laddplatser " +
                                              $"skapat!");
                            Console.ResetColor();

                            ParkVehiclesFromFile(garage);
                        }
                        break;

                    case "2": // Park a vehicle into the garage
                        ParkVehicle(garage);
                        break;

                    case "3": // Get a vehicle from the garage

                        GetVehicle(garage);
                        break;

                    case "4": // List all vehicles in the garage
                        ListAllVehicles(garage);
                        break;

                    case "5": // Search for a specific vehicle in the garage
                        ListSpecificVehicles(garage);
                        break;

                    case "6": // Park many vehicles into the garage
                        ParkVehiclesFromFile(garage);
                        break;

                    case "0": // Exit the program
                        running = false;
                        break;

                    default:
                        PrintWarningMessage("Ogiltigt val, försök igen!");
                        break;
                }
            }
        }

        //**************************************************************
        // Method to create a new garage with user-defined total number
        // of parking spots and electric spots
        //**************************************************************
        static public Garage CreateGarage()
        {
            int totalNumberOfSpots = 0;
            int numberOfElectricSpots = 0;
            int pricePerHour = 0;


            try
            {
                Console.WriteLine("Hur många parkeringsplatser totalt ska garaget ha?");
                if (!int.TryParse(Console.ReadLine(), out totalNumberOfSpots))
                {
                    throw new Exception("Ogiltigt antal parkeringsplatser");
                }

                Console.WriteLine("Hur många av parkeringsplatserna ska vara laddplatser?");
                if (!int.TryParse(Console.ReadLine(), out numberOfElectricSpots))
                {
                    throw new Exception("Ogiltigt antal laddplatser");
                }
                else if (numberOfElectricSpots > totalNumberOfSpots)
                {
                    throw new Exception("Antal laddplatser kan inte vara större än totala antalet platser");
                }

                Console.WriteLine("Ange pris per timme ( 0 - 100 kr), de 2 första timmarna är gratis?");
                if (!int.TryParse(Console.ReadLine(), out pricePerHour))
                {
                    throw new Exception("Ogiltigt pris!");
                }
                else if (pricePerHour > 100 || pricePerHour < 0)
                {
                    throw new Exception("Ogiltigt pris, skall vara mellan ( 0 - 100 kr )!");
                }

                // Creating a new garage
                return new Garage(totalNumberOfSpots, numberOfElectricSpots, pricePerHour);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine($"Error creating garage: {ex.Message}");
                Console.ResetColor();

                return null;
            }
        }

        //**************************************************************
        // Method to park a vehicle in the garage
        //**************************************************************
        static public void ParkVehicle(Garage garage)
        {
            try
            {
                if (garage == null)
                {
                    PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                    return;
                }

                // Ask for vehicle type and check given type
                Console.WriteLine("Ange typ av fordon (Bil, Mc, Buss, Båt eller Flygplan):");
                string vehicleType = Console.ReadLine().ToLower();
                if (!IsValidVehicleType(vehicleType))
                { 
                    PrintWarningMessage("Ogiltigt fordonstyp, försök igen!");

                    Console.WriteLine("Ange typ av fordon (Bil, Mc, Buss, Båt eller Flygplan):");
                    vehicleType = Console.ReadLine().ToLower();
                    if (!IsValidVehicleType(vehicleType))
                    {
                        PrintErrorMessage("Ogiltigt fordonstyp!");
                        return;
                    }
                }

                // Ask for registration number and check given reg number
                Console.WriteLine("Ange registreringsnummer på fordonet:");
                string regNumber = Console.ReadLine();
                if (!IsValidRegistrationNumber(regNumber))
                {
                    PrintWarningMessage("Ogiltigt registeringsnummer, försök igen!");

                    Console.WriteLine("Ange registreringsnummer på fordonet:");
                    regNumber = Console.ReadLine();
                    if (!IsValidRegistrationNumber(regNumber))
                    {
                        PrintErrorMessage("Ogiltigt registeringsnummer!");
                        return;
                    }
                }
                else if (garage.IsVehicleWithRegNumInGarage(regNumber))
                {
                    PrintErrorMessage("Bil med samma reg nummer finns redan i garaget!");
                }

                // Ask for vehichle brand
                Console.WriteLine("Ange vilken tillverkare (Audi, BMW, Cupra, Ford, Volvo, etc.):");
                string brand = Console.ReadLine();

                // Promt user for fule type
                Console.WriteLine("Ange type av bränsle (1:Petrol, 2:Diesel, 3:Electric, 4:Hybrid):");
                string fuelType = Console.ReadLine();

                // Promt user for vehichle color
                Console.WriteLine("Ange färg på fordonet:");
                string color = Console.ReadLine();

                // Ask for number of seats and number of wheels depending on vehichle type
                if (vehicleType.ToLower() == "bil" || vehicleType.ToLower() == "båt")
                {
                    Console.WriteLine("Ange antal säten på fordonet:");
                    int numOfSeats = int.Parse(Console.ReadLine());

                    garage.AddVehicle(vehicleType, regNumber, brand, fuelType, color, numOfSeats);
                }
                else if (vehicleType.ToLower() == "buss" || vehicleType.ToLower() == "flygplan")
                {
                    Console.WriteLine("Ange antal säten på fordonet:");
                    int numOfSeats = int.Parse(Console.ReadLine());

                    Console.WriteLine("Ange antal hjul på fordonet:");
                    int numOfWheels = int.Parse(Console.ReadLine());

                    garage.AddVehicle(vehicleType, regNumber, brand, fuelType, color, numOfSeats, numOfWheels );
                }
                else
                {
                    garage.AddVehicle(vehicleType, regNumber, brand, fuelType, color);
                } 
            }
            catch (IOException ex)
            {
                Console.WriteLine($"IO error while getting vehicle: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Format error while getting vehicle: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Format error while getting vehicle: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting vehicle: {ex.Message}");
            }
        }

        //**************************************************************
        // Method to get a vehicle from the garage
        //**************************************************************
        static public void GetVehicle(Garage garage)
        {
            try
            {
                if (garage == null)
                {
                    PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                    return;
                }
                Console.WriteLine("Ange registreringsnummer på fordonet som ska hämtas ut:");

                string regNumber = Console.ReadLine();
                garage.GetVehicle( regNumber );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting vehicle: {ex.Message}");
            }
        }

        //**************************************************************
        // List all vehicles parked in the garage
        //**************************************************************
        static public void ListAllVehicles(Garage garage)
        {
            try
            {
                if (garage == null)
                {
                    PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                    return;
                }

                garage.ListAllVehicles();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting vehicle: {ex.Message}");
            }
        }

        //**************************************************************
        // List spcific vehicles parked in the garage
        //**************************************************************
        static public void ListSpecificVehicles(Garage garage)
        {
            if (garage == null)
            {
                PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                return;
            }

            garage.ListSpecificVehicles();
        }

        //**************************************************************
        // Park many vehicles in the garage
        //**************************************************************
        static public void ParkVehiclesFromFile(Garage garage)
        {
            if (garage == null)
            {
                PrintWarningMessage("Tyvärr finns inget garage tillgängligt nu!");
                return;
            }
            int antal;
            string fordon;

            Console.WriteLine("Vill du parkera ett antal fordon direkt, ange antal (0 - 20):");
            int.TryParse(Console.ReadLine(), out antal);

            //string[] allaFordon = File.ReadAllLines(@"C:\Users\evasa\source\repos\Garage_2_0\Garage_2_0\fordon.txt");
            if (antal > 0 && antal < 20)
            {
                using (StreamReader reader = new StreamReader(@"C:\Users\evasa\source\repos\Garage_2_0\Garage_2_0\fordon.txt"))
                {
                    for (int i = 0; i <= antal; i++)
                    {
                        fordon = reader.ReadLine();

                        if (fordon == null)
                            break;

                        var cmdArgs = fordon.Split();


                        if (cmdArgs.Length > 4 && cmdArgs.Length < 8)
                        {
                            switch (cmdArgs[0])
                            {
                                case "bil":
                                    garage.AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], int.Parse(cmdArgs[5]));
                                    break;

                                case "mc":
                                    garage.AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4]);
                                    break;

                                case "buss":
                                    garage.AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], int.Parse(cmdArgs[5]), int.Parse(cmdArgs[6]));
                                    break;

                                case "båt":
                                    garage.AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], int.Parse(cmdArgs[5]));
                                    break;

                                case "flygplan":
                                    garage.AddVehicle(cmdArgs[0], cmdArgs[1], cmdArgs[2], cmdArgs[3], cmdArgs[4], int.Parse(cmdArgs[5]));
                                    break;

                                default:
                                    Console.WriteLine("Okänd fordons type!");
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
