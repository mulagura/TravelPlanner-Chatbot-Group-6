using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OrderBot
{
    public class Session
    {


        private enum State
        {
            
            WELCOMING, TO_LOCATION, FROM_LOCATION,TRAVEL_DATE,EVENTS,AVAILIBILITY, ACCOMODATION, FINAL_MESSAGE, EMAIL, UNKNOWN
                
        }


        private State nCur = State.WELCOMING;
        private Order oOrder;

        Dictionary<string, string> itemHash = new Dictionary<string, string>();

        public Session(string ssFrom)
        {
            this.oOrder = new Order();
            //this.oOrder.Phone = sPhone;
            this.itemHash.Add("1", "Plaza Get together");
            this.itemHash.Add("2", "Ontario Night club");
            this.itemHash.Add("3", "kitchner Star night");
           
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();

            if (!IsValidDateFormat(sInMessage.Trim(), "dd/MM/yyyy") && this.nCur == State.EVENTS)
            {
                this.nCur = State.TRAVEL_DATE;
                aMessages.Add("Date Format is Wrong Please Enter a Valid Date.");

            }
            

            if (!IsValidEmail(sInMessage.Trim()) && this.nCur == State.EMAIL)
            {
                this.nCur = State.FINAL_MESSAGE;
                aMessages.Add("Please Enter a Valid Email.");
            }

            if (!isHasmapHashThis(this.itemHash,sInMessage.Trim()) && this.nCur == State.AVAILIBILITY)
            {
                this.nCur = State.EVENTS;
                aMessages.Add("Please Enter a Valid Event.");
            }
            if (!IsValidAvailibilityFormat(sInMessage.Trim()) && this.nCur == State.ACCOMODATION)
            {
                this.nCur = State.AVAILIBILITY;
                aMessages.Add("Please enter the value in proper format.");
            }
            if (!(sInMessage.ToLower().Trim().Equals("y") || sInMessage.ToLower().Trim().Equals("n")) && this.nCur == State.FINAL_MESSAGE)
            {
                this.nCur = State.ACCOMODATION;
                aMessages.Add("Enter Valid Input.");
            }


            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to Travel Planner Chatbot");
                    aMessages.Add("Which location would you like to go (Kitchener/Waterloo)?");   
                    this.nCur = State.FROM_LOCATION;
                    break;
                case State.FROM_LOCATION:
                    this.oOrder.ToLocation = sInMessage;
                    aMessages.Add("From where you are going to start the travel?");
                    this.nCur = State.TRAVEL_DATE;
                    break;
                case State.TRAVEL_DATE:
                    this.oOrder.FromLocation = sInMessage;
                    aMessages.Add("Which date you want to travel?(DD/MM/YYYY)");
                    this.nCur = State.EVENTS;
                    break;
                case State.EVENTS:
                    this.oOrder.TravelDate = sInMessage;
                    aMessages.Add("Which event/place would you like to go on" + this.oOrder.TravelDate + " ?");
                    aMessages.Add("select options eg.1/2/3 (" +
                        "1. Plaza Get together, " +
                        "2. Ontario Night club, " +
                        "3. kitchner Star night)");
                    this.nCur = State.AVAILIBILITY;
                    break;
                case State.AVAILIBILITY:
                    this.oOrder.Event = sInMessage;
                    aMessages.Add("For Plaza Get together Passes are available, " +
                        "How many adults and kids you want to book? (A:2,K:3)");
                    
                    this.nCur = State.ACCOMODATION;
                    break;
                case State.ACCOMODATION:
                    this.oOrder.Save();
                    this.oOrder.Availibility = sInMessage;
                    aMessages.Add("Do you need an Accomodation? (Y/N)");
                   
                    this.nCur = State.FINAL_MESSAGE;
                    break;
                case State.FINAL_MESSAGE:
                    this.oOrder.Accomodation = sInMessage;
                    aMessages.Add("Thanks for your time, Here are some details for you.");
                    aMessages.Add("For "+ this.itemHash[this.oOrder.Event.Trim()] +", Adult pass is 200$ and kids pass is 50$");
                    aMessages.Add("If you what to confirm the bookig, please enter your e-mail address.");
                    aMessages.Add("Thank You !! We'll send the booking conformation mail.");
                    
                    
                    this.nCur = State.EMAIL;
                    break;
                case State.EMAIL:
                   
                    aMessages.Add("Booking Confirm.");
                    this.oOrder.Email = sInMessage;
                    this.oOrder.Save();
                   
                    break;

            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }



        bool IsValidDateFormat(string input, string format)
        {
            DateTime parsedDate;
            return DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
        }
        bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }
        bool isHasmapHashThis(Dictionary<string, string> itemHash, string v)
        {

            if (itemHash.ContainsKey(v))
            {
                return true;
            }

            return false;
        }
        static bool IsValidAvailibilityFormat(string input)
        {
            string pattern = @"^[A-Za-z]:\d+,[A-Za-z]:\d+$";
            return Regex.IsMatch(input, pattern);
        }

    }




}
