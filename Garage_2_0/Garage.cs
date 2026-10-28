using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using static Garage_2_0.Utilities;

namespace Garage_2_0
{
    public class Garage
    {
        private int garageId = 0;
        private int totalNumberOfParkingLots = 0;
        private int numberOfElectricParkingLots = 0;
        private int currentVehicleCount;
        private int currentElectricVehicleCount;
        private int currentOccupiedSpots;
        private int currentOccupiedElectricSpots;
        private int pricePerHour;
        private List<Vehicle> vehicles = new List<Vehicle>();

        public Garage(int numberOfSpots, int numberOfElectricSpots, int pricePerHour)
        {
            garageId++;
            TotalNumberOfParkingLots = numberOfSpots;
            NumberOfElectricParkingLots = numberOfElectricSpots;
            PricePerHour = pricePerHour;
            CurrentVehicleCount = 0;
            CurrentElectricVehicleCount = 0;
            CurrentOccupiedSpots = 0;
            CurrentOccupiedElectricSpots = 0;
        }

        //**********************************************************
        // Getter for GarageId
        //**********************************************************
        public int GarageId
        {
            get { return garageId; }
            set { garageId = value; }
        }
        //**********************************************************
        // Getter and setter for TotalNumberOfParkingLots
        //**********************************************************
        public int TotalNumberOfParkingLots
        {
            get { return totalNumberOfParkingLots; }
            set { totalNumberOfParkingLots = value; }
        }       

        //**********************************************************
        // Getter and setter for NumberOfElectricParkingLots
        //**********************************************************
        public int NumberOfElectricParkingLots
        {
            get { return numberOfElectricParkingLots; }
            set { numberOfElectricParkingLots = value; }
        }

        //**********************************************************
        // Getter and setter for PricePerHour
        //**********************************************************
        public int PricePerHour
        {
            get { return pricePerHour; }
            set { pricePerHour = value; }
        }

        //**********************************************************
        // Getter and setter for CurrentVehicleCount
        //**********************************************************
        public int CurrentVehicleCount
        { 
            get { return currentVehicleCount; }
            set { currentVehicleCount = value; }
        }

        //**********************************************************
        // Getter and setter for CurrentVehicleCount
        //**********************************************************
        public int CurrentElectricVehicleCount
        {
            get { return currentElectricVehicleCount; }
            set { currentElectricVehicleCount = value; }
        }

        //**********************************************************
        // Getter and setter for CurrentOccupiedSpots
        //**********************************************************
        public int CurrentOccupiedSpots
        {
            get { return currentOccupiedSpots; }
            set { currentOccupiedSpots = value; }
        }

        //**********************************************************
        // Getter and setter for CurrentOccupiedElectricSpots
        //**********************************************************
        public int CurrentOccupiedElectricSpots
        {
            get { return currentOccupiedElectricSpots; }
            set { currentOccupiedElectricSpots = value; }
        }

        //*************************************************************************************
        // This method is used to park a vehicle of specified type in the garage. It   
        // also increments the currentVehicleCount, currentElectricVehicleCount and 
        // currentOccupiedSpots based on the type of vehicle being parked.        
        //**************************************************************************************
        public void AddVehicle(string vehicleType,
                               string regNumber,
                               string brand, 
                               string inputfuelType, 
                               string color, 
                               int numOfSeats = 0,
                               int numOfWheels = 0 )
        {
            try
            {
                Utilities.FuelType fuelType = (Utilities.FuelType)Enum.Parse(typeof(Utilities.FuelType), inputfuelType, true);

                switch (vehicleType.ToLower())
                {
                    case "bil":  // Implementation for parking a car in the garage
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots)
                        {
                            if (fuelType == FuelType.El || fuelType == FuelType.Hybrid)
                            {
                                if (NumberOfElectricParkingLots > CurrentOccupiedElectricSpots)
                                {
                                    CurrentOccupiedElectricSpots++;
                                }
                                else
                                {
                                    PrintWarningMessage("Sorry, inga lediga laddplatser!");
                                    return;
                                }
                            }

                            Car car = new Car (regNumber, brand, fuelType, color, numOfSeats);
                            vehicles.Add(car);

                            PrintSuccessfulMessage($"Bil med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots++;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry Garage is full, cannot add more cars!");
                            return;
                        }
                        break;

                    case "mc":  // Implementation for parking a motorcycle in the garage
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots)
                        {
                            Motorcycle motorcycle = new Motorcycle ( regNumber, brand, fuelType, color);
                            vehicles.Add(motorcycle);

                            PrintSuccessfulMessage($"Motorcycel med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots++;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry Garage is full, cannot add more motorcycles!");
                            return;
                        }
                        break;

                    case "buss":  // Implementation for parking a bus in the garage
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 2)
                        {
                            Bus bus = new Bus ( regNumber, brand, fuelType, color, numOfSeats, numOfWheels);
                            vehicles.Add(bus);

                            PrintSuccessfulMessage($"Buss med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 2;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry Garage is full, cannot add more buses.");
                            return;
                        }
                        break;

                    case "båt":  // Implementation for parking a boat in the garage  
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 2)
                        {
                            Boat boat = new Boat(regNumber, brand, fuelType, color, numOfSeats);
                            vehicles.Add(boat);

                            PrintSuccessfulMessage($"Båt med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 2; ;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry Garage is full, cannot add more boats!");
                            return;
                        }
                        break;
                    case "flygplan":  // Implementation for parking an airplane in the garage
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 3)
                        {
                            Airplane airplane = new Airplane(regNumber, brand, fuelType, color, numOfSeats);
                            vehicles.Add(airplane);

                            PrintSuccessfulMessage($"Flygplan med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 3;
                        }
                        else
                        {
                            PrintErrorMessage("Sorry, Garage is full, not enough space for an airplanes!");
                            return;
                        }
                        break;

                    default:
                        PrintErrorMessage("Invalid vehicle type. Please enter a valid vehicle type.");
                        return;
                }
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error while parking a vehicle: {ex.Message}");
                return;
            }    
        }

        //*******************************************************************************************
        // This method is used to retrieve a vehicle from the garage. It takes a string parameter
        // representing the registration number of the vehicle to be retrieved.
        //*******************************************************************************************
        public void GetVehicle(string regNumber)
        {
            // Implementation for retrieving a vehicle
            try
            {
                if (CurrentVehicleCount == 0)
                {
                    PrintWarningMessage("Garage is empty. No vehicles to retrieve.");
                    return;
                }
                for (int i = 0; i < CurrentVehicleCount; i++)
                {
                    if (vehicles[i].RegistrationNumber.Equals(regNumber.Replace(" ", "").ToUpperInvariant(), 
                        StringComparison.OrdinalIgnoreCase))
                    {
                        Random random = new Random();
                        int hoursParked = random.Next(1, 24);

                        PrintSuccessfulMessage($"Fordon med registrerings nummer {vehicles[i].RegistrationNumber} hämtad!");
                        PrintSuccessfulMessage($"Kvitto: {vehicles[i].RegistrationNumber} parkerad {hoursParked} timmar, " +
                            $"avgift {Utilities.CalculateParkingFee(hoursParked, pricePerHour)} kr!");

                        // Shift the remaining vehicles to fill the gap
                        for (int j = i; j < CurrentVehicleCount - 1; j++)
                        {
                            vehicles[j] = vehicles[j + 1];
                        }
                        vehicles[CurrentVehicleCount - 1] = null; // Clear the last spot
                        CurrentVehicleCount--;
                        return;
                    }
                }
                PrintErrorMessage($"No vehicle found with registration number: {regNumber}");

            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error retrieving specifiedvehicle: {ex.Message}");
            }
        }

        //**********************************************************************
        // This method is used to list all vehicles parked in the garage.
        //**********************************************************************
        public void ListAllVehicles()
        {
            try
            {
                if (vehicles == null || CurrentVehicleCount == 0)
                {
                    PrintWarningMessage("No vehicles in the garage.");
                    return;
                }
                else
                {
                    PrintHeading();

                    for (int i = 0; i < CurrentVehicleCount; i++)
                    {
                        Console.WriteLine(vehicles[i].ToString());
                    }

                    Console.WriteLine("-------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error listing vehicles: {ex.Message}");
            }   
        }

        //**********************************************************************
        // This method is used to list vehicles with certain type/brand/color etc
        // parked in the garage.
        //**********************************************************************
        public void ListSpecificVehicles()
        {
            // Implementation for searching specific vehicles
            try
            {
                if (vehicles == null || vehicles.Count  == 0)
                {
                    PrintWarningMessage("No vehicles in the garage.");
                    return;
                }
                else if (CurrentVehicleCount == 0)
                {
                    PrintWarningMessage("No vehicles in the garage.");
                    return;
                }
                else
                {
                    Console.WriteLine("1:Här kan du lista fordon i garaget efter typ, märke, bränsleTyp eller färg, tryck enter   ");
                    Console.WriteLine("1:Lista fordon i garaget efter typ");
                    Console.WriteLine("2:Lista fordon i garaget efter märke");
                    Console.WriteLine("3:Lista fordon i garaget efter färg");

                    Console.Write("Välj alternativ: ");

                    switch (Console.ReadLine()?.Trim())
                    {
                        case "1": // List vehicles in the garage by type
                            Console.WriteLine("Ange vilken typ av fordon du vill lista (Bil, MC, Buss, Båt, Flygplan):");
                            
                            string vehicleType = Console.ReadLine()?.Trim().ToLower();

                            Utilities.PrintHeading();

                            for (int i = 0; i < CurrentVehicleCount; i++)
                            {
                                if (vehicles[i] is Car)
                                {
                                    if (vehicleType.ToLower() == "bil")
                                    {
                                        Console.WriteLine(vehicles[i].ToString());
                                    }
                                }
                                else if (vehicles[i] is Motorcycle)
                                {
                                    if (vehicleType.ToLower() == "mc")
                                    {
                                        Console.WriteLine(vehicles[i].ToString());
                                    }
                                }
                                else if (vehicles[i] is Bus)
                                {
                                    if (vehicleType.ToLower() == "buss")
                                    {
                                        Console.WriteLine(vehicles[i].ToString());
                                    }
                                }
                                else if (vehicles[i] is Boat)
                                {
                                    if (vehicleType.ToLower() == "båt")
                                    {
                                        Console.WriteLine(vehicles[i].ToString());
                                    }
                                }
                                else if (vehicles[i] is Airplane)
                                {
                                    if (vehicleType.ToLower() == "flygplan")
                                    {
                                        Console.WriteLine(vehicles[i].ToString());
                                    }
                                }
                                else
                                {
                                    PrintWarningMessage($"Fordons typen finns inte");
                                }
                            }
                            break;

                        case "2": // List vehicles in the garage by brand
                            Console.WriteLine("Ange vilken märke av fordon du vill lista:");
                            string brand = Console.ReadLine()?.Trim().ToLower();

                            Utilities.PrintHeading();

                            for (int i = 0; i < CurrentVehicleCount; i++)
                            {
                                if (brand == vehicles[i].Brand.ToLower())
                                {
                                   Console.WriteLine(vehicles[i].ToString());
                                }
                            }
                            break;

                        case "3": // List vehicles in the garage by color
                            Console.WriteLine("Ange vilken färg på fordon du vill lista:");
                            string color = Console.ReadLine()?.Trim().ToLower();

                            Utilities.PrintHeading();

                            for (int i = 0; i < CurrentVehicleCount; i++)
                            {
                                if (color == vehicles[i].Color.ToLower())
                                {
                                    Console.WriteLine(vehicles[i].ToString());
                                }
                            }
                            break;

                        default:
                            PrintWarningMessage("Ogiltigt val, försök igen!");
                            break;
                    }
                    Console.WriteLine("-------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                PrintErrorMessage($"Error listing vehicles: {ex.Message}");
            }
        }

        //***************************************************************************
        // This method checks if vehichle with given reg number exists in the garage
        //***************************************************************************
        public bool IsVehicleWithRegNumInGarage(string regNumber)
        {
            bool IsFound = false;

            // Verify if a vehicle with same reg number alreay exists in the garage
            for (int i = 0; i < CurrentVehicleCount; i++)
            {
                if (regNumber.Replace(" ", "").ToUpperInvariant() == vehicles[i].RegistrationNumber)
                {
                    IsFound = true;
                }
            }

            return IsFound;
        }
    }

}

