using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;

namespace OrderBot.tests
{
    public class OrderBotTest
    {

        

        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM orders
    ";
                commandUpdate.ExecuteNonQuery();

            }
        }

        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("123");
            String sInput = oSession.OnMessage("hello")[0];
            Console.WriteLine(sInput);
            Assert.Contains("Welcome", sInput);
            //Assert.True(sInput.Equals("Welcome"));
        }
        [Fact]
        public void TestWelcomPerformance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("123");
            String sInput = oSession.OnMessage("Kitchener")[0];
            Console.WriteLine(sInput);
            //Assert.Contains("travel", sInput.ToLower());
            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }
        [Fact]
        public void TestFromDate()
        {
            Session oSession = new Session("123");
            oSession.OnMessage("hello");
            oSession.OnMessage("Kitchener");
            oSession.OnMessage("Waterloo");
            String sInput = oSession.OnMessage("02/12/2023")[0];
            Console.WriteLine(sInput);
           
            Assert.Contains("02/12/2023", sInput);
        }
        [Fact]
        public void TestTravelBooking()
        {

            Session oSession = new Session("123");
            oSession.OnMessage("hello");
            oSession.OnMessage("Kitchener");
            oSession.OnMessage("Waterloo");
            oSession.OnMessage("1");
            oSession.OnMessage("A:2,K:3");
            oSession.OnMessage("Y");
            oSession.OnMessage("joes@gamil.com");
            String sInput = oSession.OnMessage("Thanks")[0];
            Console.WriteLine(sInput);

            Assert.Contains("Booking Confirm", sInput);
        }

        [Fact]
        public void TestPerfomanceOfWholeProcess()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("123");
            oSession.OnMessage("hello");
            oSession.OnMessage("Kitchener");
            oSession.OnMessage("Waterloo");
            oSession.OnMessage("1");
            oSession.OnMessage("A:2,K:3");
            oSession.OnMessage("Y");
            oSession.OnMessage("joes@gamil.com");
            String sInput = oSession.OnMessage("Thanks")[0];
            Console.WriteLine(sInput);

            //Assert.Contains("Booking Confirm", sInput);
            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 10000);
        }

    }
}
