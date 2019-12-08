using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CarInfo
{
    //************************************
    //Title: CarShopper
    //Application Type: Console
    //Description: To aid the user in car shopping
    //by comparing makes, models, and prices
    //Author: Sortzi, Sammi
    //Date Created: 11/30/2019
    //Last Modified: 12/8/2019
    //************************************

    class Program
    {
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //
            // initialize list
            //
            //List<Car> cars = InitializeCarList();

            //
            // read cars from file
            //
            List<Car> cars = ReadFromDataFile();

            //
            // displays
            //
            DisplayWelcomeScreen();
            DisplayMenuScreen(cars);
            DisplayClosingScreen();
        }

        #region METHODS

        /// <summary>
        /// initializing cars
        /// </summary>
        /// <returns>list of cars</returns>
        
        static List<Car> InitializeCarList()
        {
            //
            // list of cars
            //
            List<Car> cars = new List<Car>()
            {
                new Car()
                {
                    Make = "BMW",
                    Model = "330i xDrive",
                    Countries = Car.Country.germany,
                    Price = 42750
                },

                new Car()
                {
                    Make = "Audi",
                    Model = "A4",
                    Countries = Car.Country.germany,
                    Price = 44700
                }
            };

            Console.WriteLine(cars[0]);

            return cars;
        }

        static void DisplayMenuScreen(List<Car> cars)
        {
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get menu choice
                //
                Console.WriteLine("a) List All Cars in Database");
                Console.WriteLine("b) View Cars Detail and Specs");
                Console.WriteLine("c) Add Car to Database");
                Console.WriteLine("d) Delete Cars from Database");
                Console.WriteLine("e) Update Car Specs in Database");
                Console.WriteLine("f) Save Data to File");
                Console.WriteLine("g) Filter Cars Countries");
                Console.WriteLine("q) Quit Application");
                Console.Write("Enter Menu Choice===>>>>>:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayAllCars(cars);
                        break;

                    case "b":
                        DisplayViewCarSpecs(cars);
                        break;

                    case "c":
                        DisplayAddCar(cars);
                        break;

                    case "d":
                        DisplayDeleteCar(cars);
                        break;

                    case "e":
                        DisplayUpdateCar(cars);
                        break;

                    case "f":
                        DisplayWriteToDataFile(cars);
                        break;

                    case "g":
                        DisplayFilterByCountry(cars);
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please Enter a Valid Menu Choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        static void DisplayAllCars(List<Car> cars)
        {
            DisplayScreenHeader("Cars in Database");

            Console.WriteLine("\t********************");
            foreach (Car car in cars)
            {
                CarInfo(car);
                Console.WriteLine();
                Console.WriteLine("\t********************");
            }

            DisplayContinuePrompt();
        }

        static void DisplayViewCarSpecs(List<Car> cars)
        {
            DisplayScreenHeader("Car Specs and Details");

            //
            // display car makes
            //
            Console.WriteLine("\tCar Makes");
            Console.WriteLine("\t----------");
            foreach (Car car in cars)
            {
                Console.WriteLine("\t" + car.Make);
            }

            //
            // make choice
            //
            Console.WriteLine();
            Console.Write("\tEnter Car Make for more Details and Specs====>>>>>:");
            string carMake = Console.ReadLine();

            //
            // get car objects
            //
            Car selectedCar = null;
            foreach (Car car in cars)
            {
                if (car.Make == carMake)
                {
                    selectedCar = car;
                    break;
                }
            }

            //
            // display car details and specs
            //
            Console.WriteLine();
            Console.WriteLine("\t********************");
            CarInfo(selectedCar);
            Console.WriteLine("\t********************");

            DisplayContinuePrompt();
        }

        static void DisplayAddCar(List<Car> cars)
        {
            Car newCar = new Car();

            DisplayScreenHeader("Add Car and Specs");

            //
            // add car values
            //
            Console.Write("\tMake: ");
            newCar.Make = Console.ReadLine();
            Console.Write("\tModel: ");
            newCar.Model = Console.ReadLine();
            Console.Write("\tCountry: ");
            Enum.TryParse(Console.ReadLine(), out Car.Country country);
            newCar.Countries = country;
            Console.Write("\tPrice: ");
            int.TryParse(Console.ReadLine(), out int price);
            newCar.Price = price;

            //
            // echo new car's specs
            //
            Console.WriteLine("\tNew Car's Specs and Details");
            CarInfo(newCar);
            DisplayContinuePrompt();

            cars.Add(newCar);
        }

        static void DisplayDeleteCar(List<Car> cars)
        {
            DisplayScreenHeader("Deleting Cars from Database");

            //
            // Display Car Makes
            //
            Console.WriteLine("\tCar Makes");
            Console.WriteLine("\t----------");
            foreach (Car car in cars)
            {
                Console.WriteLine("\t" + car.Make);
            }

            //
            // get Car Make choice
            //
            Console.WriteLine();
            Console.WriteLine("\tEnter Car Make===>>>>:");
            string carMake = Console.ReadLine();

            //
            // get car objects
            //
            Car selectedCar = null;
            foreach (Car car in cars)
            {
                if (car.Make == carMake)
                {
                    selectedCar = car;
                    break;
                }
            }

            //
            // delete cars
            //
            if (selectedCar !=null)
            {
                cars.Remove(selectedCar);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedCar.Make} deleted");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{carMake} not found.");
            }

            DisplayContinuePrompt();
        }

        static void DisplayUpdateCar(List<Car> cars)
        {
            bool validResponse = false;
            Car selectedCar = null;

            do
            {
                DisplayScreenHeader("Update Car Specs and Details");

                //
                // Display all Car Makes
                //
                Console.WriteLine("\tCar Makes");
                Console.WriteLine("\t----------");
                foreach (Car car in cars)
                {
                    Console.WriteLine("\t" + car.Make);
                }

                //
                // get car make choice
                //
                Console.WriteLine();
                Console.Write("\tEnter Car Make===>>>>:");
                string carMake = Console.ReadLine();

                //
                // get car objects
                //

                foreach (Car car in cars)
                {
                    if (car.Make == carMake)
                    {
                        selectedCar = car;
                        validResponse = true;
                        break;
                    }
                }

                //
                // feedback for wrong car make
                //
                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select a valid car make.");
                    DisplayContinuePrompt();
                }
            } while (!validResponse);

            //
            // update car info
            //
            string userResponse;
            Console.WriteLine("\tReady to update. Press enter to keep current specs.");
            Console.Write($"\tCurrent Make: {selectedCar.Make} New Make: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                selectedCar.Make = userResponse;
            }

            Console.Write($"\tCurrent Model: {selectedCar.Model} New Model: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                selectedCar.Model = userResponse;
            }

            Console.Write($"\tCurrent Country: {selectedCar.Countries} New Country: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                Enum.TryParse(userResponse, out Car.Country country);
                selectedCar.Countries = country;
            }

            Console.Write($"\tCurrent Price: {selectedCar.Price} New Price: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                int.TryParse(userResponse, out int price);
                selectedCar.Price = price;
            }

            DisplayContinuePrompt();
        }

        static void WriteToDataFile(List<Car> cars)
        {
            string[] carsString = new string[cars.Count];

            //
            // array of car strings
            //
            for (int index = 0; index < cars.Count; index++)
            {
                string carString =
                    cars[index].Make + "," +
                    cars[index].Model + "," +
                    cars[index].Countries + "," +
                    cars[index].Price;

                carsString[index] = carString;
            }

            File.WriteAllLines("Data\\Data.txt", carsString);
        }

        static List<Car> ReadFromDataFile()
        {
            List<Car> cars = new List<Car>();

             //
             //read all lines in the file
             //
            string[] carsString = File.ReadAllLines("Data\\Data.txt");

            //
            // create objects and add to list
            //
            foreach (string carString in carsString)
            {
                //
                // get properties
                //
                string[] carProperties = carString.Split(',');

                //
                // create car
                //
                Car newCar = new Car();

                newCar.Make = carProperties[0];

                newCar.Model = carProperties[1];

                Enum.TryParse(carProperties[2], out Car.Country country);
                newCar.Countries = country;

                int.TryParse(carProperties[3], out int price);
                newCar.Price = price;

                //
                // add new cars to list
                //
                cars.Add(newCar);
            }
            return cars;
        }

        static void DisplayWriteToDataFile(List<Car> cars)
        {
            DisplayScreenHeader("\tWrite To Data File");
            Console.WriteLine("\t********************");

            //
            // warn user prompt
            //
            DisplayContinuePrompt();

            WriteToDataFile(cars);


            //
            // process I/O exceptions
            //
            Console.WriteLine();
            Console.WriteLine("\t*********************");
            Console.WriteLine();
            Console.WriteLine("\tList saved to data file.");

            DisplayContinuePrompt();
        }

        //static void DisplayFilterByMake(List<Car> cars)
        //{
            
        //    List<Car> filteredCars = new List<Car>();
        //    Console.WriteLine("Select Make: ");
        //    string selectedMake = Console.ReadLine();
        //    foreach  (Car car1 in cars)
        //    {
        //        if (selectedMake == Car.)
        //        {
        //            filteredCars.Add(cars);
        //        }
        //    }

        //    //
        //    // display new list
        //    //
        //    Console.WriteLine($"Cars Made By {selectedMake}.");
        //    Console.WriteLine("\t********************");
        //    foreach (Car car1 in filteredCars)
        //    {
        //        CarInfo();
        //        Console.WriteLine();
        //        Console.WriteLine("\t**********************");
        //    }

        //    DisplayContinuePrompt();
        //}

        static void DisplayFilterByCountry(List<Car> cars)
        {
            string menuChoice;
            bool quitApplication = false;
            do
            {
                DisplayScreenHeader("Filter Menu");

                //
                // get menu choice
                //
                Console.WriteLine("a) All German Cars");
                Console.WriteLine("b) All US Cars");
                Console.WriteLine("c) All UK Cars");
                Console.WriteLine("d) All Italian Cars");
                Console.WriteLine("e) All Swedish Cars");
                Console.WriteLine("f) All Japanese Cars");
                Console.WriteLine("q) Main Menu");
                Console.Write("Enter Menu Choice===>>>>>:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayFilterGerman(cars);
                        break;

                    case "b":
                        DisplayFilterUS(cars);
                        break;

                    case "c":
                        DisplayFilterUK(cars);
                        break;

                    case "d":
                        DisplayFilterItaly(cars);
                        break;

                    case "e":
                        DisplayFilterSweden(cars);
                        break;

                    case "f":
                        DisplayFilterJapan(cars);
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please Enter a Valid Menu Choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        static void DisplayFilterGerman(List<Car> cars)
        {
            List<Car> filteredCars = new List<Car>();
            Car.Country selectedCountry = Car.Country.germany;

            DisplayScreenHeader("German Made Cars");

            filteredCars = cars.Where(m => m.Countries == selectedCountry).ToList();
            filteredCars = filteredCars.OrderBy(m => m.Make).ToList();

            //
            // display new filtered list
            //
            Console.WriteLine($"Cars From {selectedCountry}");
            Console.WriteLine("\t********************");
            foreach (Car car in filteredCars)
            {
                CarInfo(car);
                Console.WriteLine();
                Console.WriteLine("\t********************");
            }

            DisplayContinuePrompt();
        }

        static void DisplayFilterUS(List<Car> cars)
        {
            List<Car> filteredCars = new List<Car>();
            Car.Country selectedCountry = Car.Country.USA;

            DisplayScreenHeader("United States Made Cars");

            filteredCars = cars.Where(m => m.Countries == selectedCountry).ToList();
            filteredCars = filteredCars.OrderBy(m => m.Make).ToList();

            //
            // display new filtered list
            //
            Console.WriteLine($"Cars From {selectedCountry}");
            Console.WriteLine("\t********************");
            foreach (Car car in filteredCars)
            {
                CarInfo(car);
                Console.WriteLine();
                Console.WriteLine("\t********************");
            }

            DisplayContinuePrompt();
        }

        static void DisplayFilterUK(List<Car> cars)
        {
            List<Car> filteredCars = new List<Car>();
            Car.Country selectedCountry = Car.Country.UK;

            DisplayScreenHeader("United Kingdom Made Cars");

            filteredCars = cars.Where(m => m.Countries == selectedCountry).ToList();
            filteredCars = filteredCars.OrderBy(m => m.Make).ToList();

            //
            // display new filtered list
            //
            Console.WriteLine($"Cars From {selectedCountry}");
            Console.WriteLine("\t********************");
            foreach (Car car in filteredCars)
            {
                CarInfo(car);
                Console.WriteLine();
                Console.WriteLine("\t********************");
            }

            DisplayContinuePrompt();
        }

        static void DisplayFilterItaly(List<Car> cars)
        {
            List<Car> filteredCars = new List<Car>();
            Car.Country selectedCountry = Car.Country.italy;

            DisplayScreenHeader("Italian Made Cars");

            filteredCars = cars.Where(m => m.Countries == selectedCountry).ToList();
            filteredCars = filteredCars.OrderBy(m => m.Make).ToList();

            //
            // display new filtered list
            //
            Console.WriteLine($"Cars From {selectedCountry}");
            Console.WriteLine("\t********************");
            foreach (Car car in filteredCars)
            {
                CarInfo(car);
                Console.WriteLine();
                Console.WriteLine("\t********************");
            }

            DisplayContinuePrompt();
        }

        static void DisplayFilterSweden(List<Car> cars)
        {
            List<Car> filteredCars = new List<Car>();
            Car.Country selectedCountry = Car.Country.sweden;

            DisplayScreenHeader("Swedish Made Cars");

            filteredCars = cars.Where(m => m.Countries == selectedCountry).ToList();
            filteredCars = filteredCars.OrderBy(m => m.Make).ToList();

            //
            // display new filtered list
            //
            Console.WriteLine($"Cars From {selectedCountry}");
            Console.WriteLine("\t********************");
            foreach (Car car in filteredCars)
            {
                CarInfo(car);
                Console.WriteLine();
                Console.WriteLine("\t********************");
            }

            DisplayContinuePrompt();
        }

        static void DisplayFilterJapan(List<Car> cars)
        {
            List<Car> filteredCars = new List<Car>();
            Car.Country selectedCountry = Car.Country.japan;

            DisplayScreenHeader("Japanese Made Cars");

            filteredCars = cars.Where(m => m.Countries == selectedCountry).ToList();
            filteredCars = filteredCars.OrderBy(m => m.Make).ToList();

            //
            // display new filtered list
            //
            Console.WriteLine($"Cars From {selectedCountry}");
            Console.WriteLine("\t********************");
            foreach (Car car in filteredCars)
            {
                CarInfo(car);
                Console.WriteLine();
                Console.WriteLine("\t********************");
            }

            DisplayContinuePrompt();
        }
        #endregion

        #region CONSOLE HELPER METHODS

        /// <summary>
        /// display all car properties
        /// </summary>
        /// <param name="car">monster object</param>
        static void CarInfo(Car car)
        {
            Console.WriteLine($"\tMake: {car.Make}");
            Console.WriteLine($"\tModel: {car.Model}");
            Console.WriteLine($"\tCountry: {car.Countries}");
            Console.WriteLine($"\tPrice: {car.Price}");
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Car Shopper");
            Console.WriteLine();
            Console.WriteLine("This application was designed to help aid consumers");
            Console.WriteLine("with car shopping by listing make, model, country of origin");
            Console.WriteLine("and base model prices with an added filter for country of origin.");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Car Shopper!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.Write("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}