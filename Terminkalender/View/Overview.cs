/*
    Copyright (c) 2021 Artur Denk
    
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using System;
using Terminkalender.Controller;

namespace Terminkalender.View
{
    /// <summary>
    /// This class serves as a user interface for the user.
    /// </summary>
    internal class Overview
    {
        private readonly AppointmentManager appointmentManager;
        private readonly AppointmentViewCreate appointmentViewCreate;
        private readonly AppointmentViewDisplay appointmentViewDisplay;
        private readonly AppointmentViewDelete appointmentViewDelete;
        private readonly AppointmentViewStorage appointmentViewStorage;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        internal Overview()
        {
            appointmentManager = new AppointmentManager();
            appointmentViewCreate = new AppointmentViewCreate(appointmentManager);
            appointmentViewDisplay = new AppointmentViewDisplay(appointmentManager);
            appointmentViewDelete = new AppointmentViewDelete(appointmentManager, appointmentViewDisplay);
            appointmentViewStorage = new AppointmentViewStorage(appointmentManager);

            MainMenu();
        }

        /// <summary>
        /// The main menu of the software.
        /// </summary>
        private void MainMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n##################################################\n");
                Console.WriteLine("1. Termin erstellen");
                Console.WriteLine("2. Übersicht der Termine des aktuellen Tages");
                Console.WriteLine("3. Übersicht der Termine der nächsten sieben Tage");
                Console.WriteLine("4. Übersicht aller Termine");
                Console.WriteLine("5. Termin löschen");
                Console.WriteLine("6. Alle Termine eines Tages löschen");
                Console.WriteLine("7. Alle Termine löschen");
                Console.WriteLine("8. Termine speichern");
                Console.WriteLine("0. Programm beenden");
                Console.Write("Geben Sie die entsprechende Nummer ein um fortzufahren: ");
                int input = AppointmentViewGeneral.GetUserInputInt();

                switch (input)
                {
                    case 1:
                        appointmentViewCreate.CreateNewAppointment();
                        break;
                    case 2:
                        appointmentViewDisplay.ShowAppointmentsOfToday();
                        break;
                    case 3:
                        appointmentViewDisplay.ShowAppointmentsOfNextSevenDays();
                        break;
                    case 4:
                        appointmentViewDisplay.ShowAllAppointments();
                        break;
                    case 5:
                        appointmentViewDelete.DeleteAppointmentByIndex();
                        break;
                    case 6:
                        appointmentViewDelete.DeleteAppointmentByDate();
                        break;
                    case 7:
                        appointmentViewDelete.DeleteAllAppointments();
                        break;
                    case 8:
                        appointmentViewStorage.SaveAppointments();
                        break;
                    case 0:
                        isRunning = false;
                        break;
                    default:
                        AppointmentViewGeneral.PrintInvalidInput();
                        break;
                }
            }
        }
    }
}
