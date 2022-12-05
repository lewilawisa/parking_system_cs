using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Terminal.Gui;

namespace parking_system_cs
{
    class Vehicle
    {
        public string slot_number { get; set; }
        public string plate_number { get; set; }
        public bool is_even_plate { get; set; }
        public string colour { get; set; }
        public string type { get; set; }
        public DateTime check_in { get; set; }
    }

    class Program
    {
        static int tableWidth = 73;
        private int init_parking_lot = 0;
        private int init_hour_car = 5000;
        private int init_hour_motor = 2000;

        List<Vehicle> VehicleList;

        static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }

        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        public static void Choose()
        {
            //Console.Clear();
            Console.WriteLine("Console Parking System in C#\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Choose an option from the following list:\n");
            Console.WriteLine("\t1|create_parking_lot - Create Parking Lot");
            Console.WriteLine("\t2|park - Park Vehicle");
            Console.WriteLine("\t3|leave - Leave Parking Lot");
            Console.WriteLine("\t4|status - Parking Lot Status");
            Console.WriteLine("\t5|type_of_vehicles - Check TypeOfVehicle");
            Console.WriteLine("\t6|registration_numbers_for_vehicles_with_ood_plate - Check RegistrationNumbersForVehicleWithOddPLate");
            Console.WriteLine("\t7|registration_numbers_for_vehicles_with_event_plate - Check RegistrationNumbersForVehicleWithEvenPLate");
            Console.WriteLine("\t8|registration_numbers_for_vehicles_with_colour - Check SlotNumberForVehicleWithColour");
            Console.WriteLine("\t9|slot_numbers_for_vehicles_with_colour - Check SlotNumberForRegistrationNumber");
            Console.WriteLine("\t0|exit - Exit Parking System");
            Console.Write("Your option? ");
        }

        public void Create(int i)
        {
            //VehicleList = new List<Vehicle>(i);
            //for (int x = 0; x < i; x++)
            //{
            //    VehicleList.Add(new Vehicle { slot_number = x.ToString() }) ;
            //}
            //Console.Write("Created a parking lot with " + VehicleList.Count()+" slots \n");
        }

        public int FindMaxSlot(List<Vehicle> v)
        {
            if (v.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            int maxSlot = int.MinValue;
            foreach (Vehicle type in v)
            {
                if (int.Parse(type.slot_number) > maxSlot)
                {
                    maxSlot= int.Parse(type.slot_number);
                }
            }
            return maxSlot;
        }

        public void Park(string s, List<Vehicle> v, int max)
        {
            string search = s.ToUpper();
            //Console.Write("Park: "+ search + " max("+max+")\n");
            int next_slot = 0;
            int current_data = v.Count();
            int empty_slot = 0;
            string plate_number_check = "";
            bool duplicate_plate_number = false;
            int maxValue = 0;
            if (current_data > 0)
            {
                maxValue = FindMaxSlot(v);
                next_slot = FindMaxSlot(v)+1;
            }
            else
            {
                next_slot = 1;
            }
            int[] check_data = new int[maxValue];
            for (int i = 1; i <= check_data.Length; i++)
            {

                check_data[i-1] = i;
                //Console.WriteLine(check_data[i-1]);

                    bool result = v.Find(x => int.Parse(x.slot_number) == i) == null ? true:false ;
                if (result)
                {
                    if (empty_slot < 1)
                    {
                         empty_slot = i;
                    }

                }
            }

            //Console.WriteLine("empty_slot: " + empty_slot + "\n");

            int car = 0;
            int motor = 0;
            string plate_number = "";
            bool is_even_plate = false;
            string[] colour_type;
            string colour = "";
            string type = "";
            DateTime check_in = DateTime.Now;

            string pattern_plate_number_check = "[0-9]{4}[-][a-zA-Z]{2,3}";
            string pattern_car = "^[a-zA-Z]{1,2}[-][0-9]{4}[-][a-zA-Z]{2,3} [a-zA-Z]{1,} [cC][aA][rR]$|^[a-zA-Z]{1,2}[-][0-9]{4}[-][a-zA-Z]{2,3} [a-zA-Z]{1,} [mM][oO][bB][iI][lL]$|^[a-zA-Z]{1,2}[-][0-9]{4}[-][a-zA-Z]{2,3} [a-zA-Z]{1,} [cC][aA][rR][ ]{1,}$|^[a-zA-Z]{1,2}[-][0-9]{4}[-][a-zA-Z]{2,3} [a-zA-Z]{1,} [mM][oO][bB][iI][lL][ ]{1,}$";
            string pattern_motor = "^[a-zA-Z]{1,2}[-][0-9]{4}[-][a-zA-Z]{2,3} [a-zA-Z]{1,} [mM][oO][tT][oO][rR][cC][yY][cC][lL][eE]$|^[a-zA-Z]{1,2}[-][0-9]{4}[-][a-zA-Z]{2,3} [a-zA-Z]{1,} [mM][oO][tT][oO][rR][cC][yY][cC][lL][eE][ ]{1,}$|^[a-zA-Z]{1,2}[-][0-9]{4}[-][a-zA-Z]{2,3} [a-zA-Z]{1,} [mM][oO][tT][oO][rR]$|^[a-zA-Z]{1,2}[-][0-9]{4}[-][a-zA-Z]{2,3} [a-zA-Z]{1,} [mM][oO][tT][oO][rR][ ]{1,}$";
            string pattern_is_even_plate = "[0-9]{4}";
            string pattern_colour_type = "[a-zA-Z]{1,}[ ][mM][oO][tT][oO][rR]$|[a-zA-Z]{1,}[ ][mM][oO][tT][oO][rR][ ]{1,}$|[a-zA-Z]{1,}[ ][mM][oO][tT][oO][rR][cC][yY][cC][lL][eE][ ]{1,}$|[a-zA-Z]{1,}[ ][mM][oO][tT][oO][rR][cC][yY][cC][lL][eE]$|[a-zA-Z]{1,}[ ][mM][oO][bB][iI][lL]|[a-zA-Z]{1,}[ ][mM][oO][bB][iI][lL][ ]$|[a-zA-Z]{1,}[ ][cC][aA][rR]$|[a-zA-Z]{1,}[ ][cC][aA][rR][ ]$";

            Regex rg_plate_number_check = new Regex(pattern_plate_number_check);
            Regex rg_car = new Regex(pattern_car);
            Regex rg_motor = new Regex(pattern_motor);
            Regex rg_is_even_plate = new Regex(pattern_is_even_plate);
            Regex rg_colour_type = new Regex(pattern_colour_type);

            //rg_plate_number_check
            MatchCollection matchedPlateNumberCheck = rg_plate_number_check.Matches(search);
            for (int count_plate_number_check = 0; count_plate_number_check < matchedPlateNumberCheck.Count; count_plate_number_check++)
            {
                //Console.WriteLine(matchedPlateNumberCheck[count_plate_number_check].Value);
                plate_number_check = matchedPlateNumberCheck[count_plate_number_check].Value;
            }

            foreach (var val in v)
            {
                if (val.plate_number.ToUpper().Equals(plate_number_check))
                {
                    duplicate_plate_number = true;
                }
            }

            //rg_car
            MatchCollection matchedCar = rg_car.Matches(search);
            for (int count_car = 0; count_car < matchedCar.Count; count_car++)
            {
                //Console.WriteLine(matchedCar[count_car].Value);
                car++;
            }

            //rg_motor
            MatchCollection matchedMotor = rg_motor.Matches(search);
            for (int count_motor = 0; count_motor < matchedMotor.Count; count_motor++)
            {
                //Console.WriteLine(matchedMotor[count_motor].Value);
                motor++;
            }

            if (next_slot > max)
            {
                if (empty_slot <1)
                {
Console.Write("Sorry, parking lot is full\n");
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    next_slot = empty_slot;

                    //rg_plate_number
                    MatchCollection matchedPlateNumber = rg_plate_number_check.Matches(search);
                    for (int count_plate_number = 0; count_plate_number < matchedPlateNumber.Count; count_plate_number++)
                    {
                        //Console.WriteLine(matchedPlateNumber[count_plate_number].Value);
                        plate_number = matchedPlateNumber[count_plate_number].Value;
                    }

                    //rg_is_even_plate
                    MatchCollection matchedIsEvenPlate = rg_is_even_plate.Matches(search);
                    for (int count_is_even_plate = 0; count_is_even_plate < matchedIsEvenPlate.Count; count_is_even_plate++)
                    {
                        //Console.WriteLine(matchedIsEvenPlate[count_is_even_plate].Value);
                        is_even_plate = int.Parse(matchedIsEvenPlate[count_is_even_plate].Value) % 2 == 0 ? true : false;
                    }

                    //rg_colour_type
                    MatchCollection matchedColourType = rg_colour_type.Matches(search);
                    for (int count_colour_type = 0; count_colour_type < matchedColourType.Count; count_colour_type++)
                    {
                        //Console.WriteLine(matchedColourType[count_colour_type].Value);
                        colour_type = matchedColourType[count_colour_type].Value.Split(' ');
                        colour = colour_type[0].ToUpper();
                        type = colour_type[1].ToUpper() == "MOBIL" ? "CAR" : colour_type[1].ToUpper() == "CAR" ? "CAR" : "MOTOR";
                    }

                    v.Add(new Vehicle { slot_number = next_slot.ToString(), plate_number = plate_number.ToUpper(), is_even_plate = is_even_plate, colour = colour, type = type, check_in = check_in });
                    Console.Write("Allocated slot number: " + next_slot + " slots\n");
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
                
            }
            else if(duplicate_plate_number)
            {
                Console.Write("Sorry, duplicate input Plate Number!\n");
                Console.Write("Press Enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
            else if (motor<1 && car<1)
            {
                Console.Write("Please input a valid Plate Number, ex. B-1233-XYZ Putih Mobil\n");
                Console.Write("Press Enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {

            //rg_plate_number
            MatchCollection matchedPlateNumber = rg_plate_number_check.Matches(search);
            for (int count_plate_number = 0; count_plate_number < matchedPlateNumber.Count; count_plate_number++)
            {
                //Console.WriteLine(matchedPlateNumber[count_plate_number].Value);
                plate_number = matchedPlateNumber[count_plate_number].Value;
            }

            //rg_is_even_plate
            MatchCollection matchedIsEvenPlate = rg_is_even_plate.Matches(search);
            for (int count_is_even_plate = 0; count_is_even_plate < matchedIsEvenPlate.Count; count_is_even_plate++)
            {
                //Console.WriteLine(matchedIsEvenPlate[count_is_even_plate].Value);
                is_even_plate = int.Parse(matchedIsEvenPlate[count_is_even_plate].Value)%2==0?true:false;
            }

            //rg_colour_type
            MatchCollection matchedColourType = rg_colour_type.Matches(search);
            for (int count_colour_type = 0; count_colour_type < matchedColourType.Count; count_colour_type++)
            {
                //Console.WriteLine(matchedColourType[count_colour_type].Value);
                colour_type = matchedColourType[count_colour_type].Value.Split(' ');
                colour = colour_type[0].ToUpper();
                type = colour_type[1].ToUpper()=="MOBIL"?"CAR": colour_type[1].ToUpper() == "CAR"?"CAR":"MOTOR";
            }

                v.Add(new Vehicle { slot_number = next_slot.ToString(), plate_number = plate_number.ToUpper(), is_even_plate = is_even_plate, colour = colour, type = type, check_in = check_in });
            Console.Write("Allocated slot number: " + next_slot + " slots\n");
                Console.Write("Press Enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }
        public void Leave(string i, List<Vehicle> v, int car, int motor)
        {
            bool check_slot = false;
            DateTime check_in;
            DateTime check_out = DateTime.Now;
            TimeSpan duration_seconds;
            TimeSpan duration;
            int hour=0;
            int minute=0;
            String str_duration="";
            String type = "";
            int payment = 0;
            foreach (var val in v)
            {
                if (val.slot_number.Equals(i))
                {
                    check_slot = true;
                    check_in = val.check_in;
                    type = val.type.ToUpper();
                    duration_seconds = check_out-check_in;
                    duration = TimeSpan.FromSeconds(duration_seconds.TotalSeconds);
                    hour = duration.Hours>0? duration.Hours: 1;
                    minute = duration.Minutes;
                    str_duration = duration.ToString(@"hh\:mm\:ss");
                }
            }

            if (check_slot)
            {
                if (minute > 0)
                {
                    hour = hour + 1;
                }
                if (type == "CAR")
                {
                    payment = hour * car;
                }
                else
                {
                    payment = hour * motor;
                }
                Console.Write("Duration: " + str_duration + " \n");
                Console.Write("Parking Fee: " + payment + " \n");
                Console.Write("Press Enter to continue");
                Console.ReadLine();
                var itemToRemove = v.Single(r => r.slot_number == i);
                v.Remove(itemToRemove);
                Console.Write("Slot number " + i + " is free\n");
                Console.Write("Press Enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.Write("Slot number not found!\n");
                Console.Write("Press Enter to continue");
                Console.ReadLine();
                Console.Clear();
            }
        }
        public void Status(int max, int car, int motor, List<Vehicle> v)
        {
            Console.Write("Parking Lot with "+ max+" slots!\n");
            Console.Write("Car Parking Fee: " + car + "/hour\n");
            Console.Write("Motor Parking Fee: " + motor + "/hour\n");
            PrintLine();
            PrintRow("Slot", "No.", "Type", "R. No Colour");
            PrintLine();
            foreach (var val in v)
            {
                PrintRow(val.slot_number, val.plate_number, val.type, val.colour);
            }
            PrintLine();
            Console.Write("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public void TypeOfVehicle(string s, List<Vehicle> v)
        {
            string search = s.ToUpper();
            int c=0; 
            foreach (var val in v)
            {
                if (val.type.ToUpper().Equals(search))
                {
                    c++;
                }
            }
            Console.WriteLine(c + "\n");
            Console.Write("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
        public void RegistrationNumbersForVehicleWithOddPLate(List<Vehicle> v)
        {
            string str = "";
            foreach (var val in v)
            {
                if (!val.is_even_plate)
                {
                    if (str.Length < 1)
                    {
                        str = val.plate_number;
                    }
                    else
                    {
                        str += "," + val.plate_number;
                    }

                }
            }
            Console.WriteLine(str + "\n");
            Console.Write("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
        public void RegistrationNumbersForVehicleWithEvenPLate(List<Vehicle> v)
        {
            string str = "";
            foreach (var val in v)
            {
                if (val.is_even_plate)
                {
                    if (str.Length < 1)
                    {
                        str = val.plate_number;
                    }
                    else
                    {
                        str += "," + val.plate_number;
                    }

                }
            }
            if (str.Length < 1)
            {
                str = "0";
            }
            Console.WriteLine(str + "\n");
            Console.Write("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public void SlotNumberForVehicleWithColour(string s, List<Vehicle> v)
        {
            string search = s.ToUpper();
            string str = "";
            foreach (var val in v)
            {
                if (val.colour.ToUpper().Equals(search))
                {
                    if (str.Length < 1)
                    {
                        str = val.slot_number;
                    }
                    else
                    {
                        str += ","+val.slot_number;
                    }
                   
                }
            }
            if (str.Length < 1)
            {
                str = "0";
            }
            Console.WriteLine(str + "\n");
            Console.Write("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public void SlotNumberForRegistrationNumber(string s, List<Vehicle> v)
        {
            string search = s.ToUpper();
            string str="";
            foreach (var val in v)
            {
                if (val.plate_number.ToUpper().Equals(search))
                {
                    if (str.Length < 1)
                    {
                        str = val.slot_number;
                    }
                    else
                    {
                        str += "," + val.slot_number;
                    }
                }
            }
            Console.WriteLine(str + "\n");
            Console.Write("Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.VehicleList = new List<Vehicle>();
            bool endApp = false;
            while (!endApp)
            {
                string input = "";
                Choose();
                string op = Console.ReadLine();

                double checkNum = 0;
                //while (!double.TryParse(op, out checkNum))
                //{
                //   Console.Write("This is not valid input. Please input (0-9): ");
                //   op = Console.ReadLine();
                //}

                switch (op)
                {
                    case "1":
                    case "create_parking_lot":
                        // Create Parking Lot
                        Console.Write("Input Create Parking Lot, and then press Enter: ");
                        input = Console.ReadLine();
                        while (!double.TryParse(input, out checkNum))
                        {
                            Console.Write("This is not valid input. Please input numeric and then press Enter: ");
                            input = Console.ReadLine();
                        }

                        //p.Create(int.Parse(input));
                        p.init_parking_lot = int.Parse(input);
                        Console.Write("Created a parking lot with " + p.init_parking_lot + " slots \n");
                        Console.Write("Press Enter to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                    case "park":
                        // Park Vehicle
                        if (p.init_parking_lot<1)
                        {
                            Console.Write("Please Create Parking Lot!\n");
                            Console.Write("Press Enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Write("Input Plate Number, and then press Enter: ");
                            input = Console.ReadLine();
                            p.Park(input, p.VehicleList, p.init_parking_lot);
                        }
                        break;
                    case "3":
                    case "leave":
                        // Leave Parking Lot
                        if (p.init_parking_lot < 1)
                        {
                            Console.Write("Please Create Parking Lot!\n");
                            Console.Write("Press Enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Write("Input Slot Number, and then press Enter: ");
                            input = Console.ReadLine();
                            p.Leave(input, p.VehicleList, p.init_hour_car,p.init_hour_motor);
                        }
                        break;
                    case "4":
                    case "status":
                        // Parking Lot Status
                        if (p.init_parking_lot < 1)
                        {
                            Console.Write("Please Create Parking Lot!\n");
                            Console.Write("Press Enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            p.Status(p.init_parking_lot,p.init_hour_car,p.init_hour_motor, p.VehicleList);
                        }
                        break;
                    case "5":
                    case "type_of_vehicles":
                        // Check TypeOfVehicle
                        if (p.init_parking_lot < 1)
                        {
                            Console.Write("Please Create Parking Lot!\n");
                            Console.Write("Press Enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Write("Input Type Of Vehicle, and then press Enter: ");
                            input = Console.ReadLine();
                            p.TypeOfVehicle(input, p.VehicleList);
                        }
                        break;
                    case "6":
                    case "registration_numbers_for_vehicles_with_ood_plate":
                        // Check RegistrationNumbersForVehicleWithOddPLate
                        if (p.init_parking_lot < 1)
                        {
                            Console.Write("Please Create Parking Lot!\n");
                            Console.Write("Press Enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            p.RegistrationNumbersForVehicleWithOddPLate(p.VehicleList);
                        }
                        break;
                    case "7":
                    case "registration_numbers_for_vehicles_with_event_plate":
                        // Check RegistrationNumbersForVehicleWithEvenPLate
                        if (p.init_parking_lot < 1)
                        {
                            Console.Write("Please Create Parking Lot!\n");
                            Console.Write("Press Enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            p.RegistrationNumbersForVehicleWithEvenPLate(p.VehicleList);
                        }
                        break;
                    case "8":
                    case "registration_numbers_for_vehicles_with_colour":
                        // Check SlotNumberForVehicleWithColour
                        if (p.init_parking_lot < 1)
                        {
                            Console.Write("Please Create Parking Lot!\n");
                            Console.Write("Press Enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Write("Input SlotNumberForVehicleWithColour, and then press Enter: ");
                            input = Console.ReadLine();
                            p.SlotNumberForVehicleWithColour(input, p.VehicleList);
                        }
                        break;
                    case "9":
                    case "slot_numbers_for_vehicles_with_colour":
                        // Check SlotNumberForRegistrationNumber
                        if (p.init_parking_lot < 1)
                        {
                            Console.Write("Please Create Parking Lot!\n");
                            Console.Write("Press Enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Write("Input SlotNumberForRegistrationNumber, and then press Enter: ");
                            input = Console.ReadLine();
                            p.SlotNumberForRegistrationNumber(input, p.VehicleList);
                        }
                        break;
                    case "0":
                    case "exit":
                        // Exit Parking System
                        endApp = true;
                        Console.WriteLine("------------------------");
                        Console.Write("Thank you for using Parking System! have a nice day!\n");
                        Console.Write("Press Enter to exit");
                        Console.ReadLine();
                        endApp = true;
                        break;
                    default:
                        // choose
                        Console.Write("This is not valid input!\n");
                        Console.Write("Press Enter to continue\n");
                        Console.ReadLine();
                        Console.Clear();
                        Choose();
                        Console.Clear();

                        break;
                }
            }
            return;
        }
    }
}
