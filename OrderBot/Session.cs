using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            
            WELCOMING, TO_LOCATION, FROM_LOCATION,TRAVEL_DATE,EVENTS,AVAILIBILITY, ACCOMODATION, FINAL_MESSAGE, EMAIL
                
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session()
        {
            this.oOrder = new Order();
            //this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to Travel Planner Chatbot");
                    aMessages.Add("Which location would you like to go (Kitchener/Waterloo)?");
                    
                    this.nCur = State.FROM_LOCATION;
                    break;
                //case State.TO_LOCATION:
                //    aMessages.Add("Which location would you like to go (Kitchener/Waterloo)?");
                //    this.oOrder.ToLocation = sInMessage;
                //    this.nCur = State.FROM_LOCATION;
                //    break;
                case State.FROM_LOCATION:
                    this.oOrder.ToLocation = sInMessage;
                    aMessages.Add("From where your going to start the travel?");
                    this.nCur = State.TRAVEL_DATE;
                    break;
                case State.TRAVEL_DATE:
                    this.oOrder.FromLocation = sInMessage;
                    aMessages.Add("Which date you want to travel?(DD/MM/YYYY)");
                    this.nCur = State.EVENTS;
                    break;
                case State.EVENTS:
                    this.oOrder.TravelDate = sInMessage;
                    aMessages.Add("Which event/place you like to go on" + this.oOrder.TravelDate + " ?");
                    aMessages.Add("select options eg.1/2/3 (1. Plaza Get together, 2. Ontario Night club, 3. kitchner Star night)");
                    
                   
                    this.nCur = State.AVAILIBILITY;
                    break;
                case State.AVAILIBILITY:

                    aMessages.Add("For Plaza Get together Passes are available, How many adults and kids you want to book? (A:2,K:3)");
                    
                    this.nCur = State.ACCOMODATION;
                    break;
                case State.ACCOMODATION:
                    this.oOrder.Availibility = sInMessage;
                    aMessages.Add("Are you need a Accomodation? (Y/N)");
                   
                    this.nCur = State.FINAL_MESSAGE;
                    break;
                case State.FINAL_MESSAGE:
                    this.oOrder.Accomodation = sInMessage;
                    aMessages.Add("Thanks your for your time, Here are some details for you.");
                    aMessages.Add("For Plaza Get together event Adult pass is 200$ and kids pass is 50$, you have to pay 550$ on the event gate.");
                    aMessages.Add("If you what to confirm the bookig please enter your e-mail address.");
                    aMessages.Add("We'll sent you the booking conformation mail.");
                    
                    
                    this.nCur = State.EMAIL;
                    break;
                case State.EMAIL:
                    aMessages.Add("Booking Confirm.");
                    this.oOrder.Email = sInMessage;

                    //this.nCur = State.FINAL_MESSAGE;
                    break;


                    //case State.SIZE:
                    //    this.oOrder.Size = sInMessage;
                    //    this.oOrder.Save();
                    //    aMessages.Add("What protein would you like on this  " + this.oOrder.Size + " Shawarama?");
                    //    this.nCur = State.PROTEIN;
                    //    break;
                    //case State.PROTEIN:
                    //    string sProtein = sInMessage;
                    //    aMessages.Add("What toppings would you like on this (1. pickles 2. Tzaki) " + this.oOrder.Size + " " + sProtein + " Shawarama?");
                    //    break;


            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
