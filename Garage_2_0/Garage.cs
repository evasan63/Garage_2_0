using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

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

                // Verify if a vehicle with same reg number alreay exists in the garage
                for (int i = 0; i < CurrentVehicleCount; i++)
                {
                    if ( regNumber.Replace(" ", "").ToUpperInvariant() == vehicles[i].RegistrationNumber)
                    {
                        Console.WriteLine("Ett fordon med samma register nummer finns redan parkerad i garaget!");
                        return;
                    }
                }

                switch (vehicleType.ToLower())
                {
                    case "bil":  // Implementation for parking a car in the garage
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots)
                        {
                             if (NumberOfElectricParkingLots > CurrentOccupiedElectricSpots)
                             {
                                 CurrentOccupiedElectricSpots++;
                             }
                             else
                             {
                                 Console.WriteLine("Sorry, inga lediga laddplatser!");
                                 return;
                             }

                            Car car = new Car (regNumber, brand, fuelType, color, numOfSeats);
                            vehicles.Add(car);

                            //Console.WriteLine();
                            Console.WriteLine($"Bil med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");
                            Console.WriteLine();

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots++;
                        }
                        else
                        {
                            Console.WriteLine("Sorry Garage is full, cannot add more cars!");
                            return;
                        }
                        break;

                    case "mc":  // Implementation for parking a motorcycle in the garage
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots)
                        {
                            Motorcycle motorcycle = new Motorcycle ( regNumber, brand, fuelType, color);
                            vehicles.Add(motorcycle);

                            //Console.WriteLine();
                            Console.WriteLine($"Motorcycel med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");
                            Console.WriteLine();

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots++;
                        }
                        else
                        {
                            Console.WriteLine("Sorry Garage is full, cannot add more motorcycles!");
                            return;
                        }
                        break;

                    case "buss":  // Implementation for parking a bus in the garage
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 2)
                        {
                            Bus bus = new Bus ( regNumber, brand, fuelType, color, numOfWheels, numOfSeats);
                            vehicles.Add(bus);

                            //Console.WriteLine();
                            Console.WriteLine($"Buss med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");
                            Console.WriteLine();

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 2;
                        }
                        else
                        {
                            Console.WriteLine("Sorry Garage is full, cannot add more buses.");
                            return;
                        }
                        break;

                    case "båt":  // Implementation for parking a boat in the garage  
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 2)
                        {
                            Boat boat = new Boat(regNumber, brand, fuelType, color, numOfSeats);
                            vehicles.Add(boat);

                            //Console.WriteLine();
                            Console.WriteLine($"Båt med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");
                            Console.WriteLine();

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 2; ;
                        }
                        else
                        {
                            Console.WriteLine("Sorry Garage is full, cannot add more boats!");
                            return;
                        }
                        break;
                    case "flygplan":  // Implementation for parking an airplane in the garage
                        if (CurrentOccupiedSpots < totalNumberOfParkingLots - 3)
                        {
                            Airplane airplane = new Airplane(regNumber, brand, fuelType, color, numOfSeats);
                            vehicles.Add(airplane);

                            //Console.WriteLine();
                            Console.WriteLine($"Flygplan med registrerings nummer {vehicles[CurrentVehicleCount].RegistrationNumber} parkerad!");
                            Console.WriteLine();

                            CurrentVehicleCount++;
                            CurrentOccupiedSpots = CurrentOccupiedSpots + 3;
                        }
                        else
                        {
                            Console.WriteLine("Sorry, Garage is full, not enough space for an airplanes!");
                            return;
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid vehicle type. Please enter a valid vehicle type.");
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while parking a vehicle: {ex.Message}");
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
                    Console.WriteLine("Garage is empty. No vehicles to retrieve.");
                    return;
                }
                for (int i = 0; i < CurrentVehicleCount; i++)
                {
                    if (vehicles[i].RegistrationNumber.Equals(regNumber.Replace(" ", "").ToUpperInvariant(), 
                        StringComparison.OrdinalIgnoreCase))
                    {
                        Random random = new Random();
                        int hoursParked = random.Next(1, 24);

                        Console.WriteLine();
                        Console.WriteLine($"Fordon med registrerings nummer {vehicles[i].RegistrationNumber} hämtad!");
                        Console.WriteLine($"Kvitto: {vehicles[i].RegistrationNumber} parkerad {hoursParked} timmar, " +
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
                Console.WriteLine($"No vehicle found with registration number: {regNumber}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving specifiedvehicle: {ex.Message}");
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
                    Console.WriteLine("No vehicles in the garage.");
                    return;
                }
                else
                {
                    Utilities.PrintHeading();

                    for (int i = 0; i < CurrentVehicleCount; i++)
                    {
                        Console.WriteLine(vehicles[i].ToString());
                    }

                    Console.WriteLine("-------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing vehicles: {ex.Message}");
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
                    Console.WriteLine("No vehicles in the garage.");
                    return;
                }
                else if (CurrentVehicleCount == 0)
                {
                    Console.WriteLine("No vehicles in the garage.");
                    return;
                }
                else
                {
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
                                    Console.WriteLine($"Fordons typen finns inte");
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
                            Console.WriteLine("Ogiltigt val, försök igen!");
                            break;
                    }
                    Console.WriteLine("-------------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing vehicles: {ex.Message}");
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

