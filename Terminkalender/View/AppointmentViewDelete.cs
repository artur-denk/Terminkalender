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
using System.Linq;
using Terminkalender.Controller;

namespace Terminkalender.View
{
    /// <summary>
    /// This class is responsible for displaying appointments that are to be deleted.
    /// </summary>
    internal class AppointmentViewDelete
    {
        #region Members

        private const string CONFIRMATION_STRING = "Y";

        private readonly AppointmentManager appointmentManager;
        private readonly AppointmentViewDisplay appointmentViewDisplay;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="appointmentManager">Manages all schedule-related tasks.</param>\
        /// <param name="appointmentViewDisplay">Manages displaying the appointments.</param>
        internal AppointmentViewDelete(AppointmentManager appointmentManager, AppointmentViewDisplay appointmentViewDisplay)
        {
            this.appointmentManager = appointmentManager;
            this.appointmentViewDisplay = appointmentViewDisplay;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Deletes the appointments by index.
        /// </summary>
        internal void DeleteAppointmentByIndex()
        {
            if (appointmentManager.Appointments.Any())
            {
                appointmentViewDisplay.ShowAllAppointments();

                Console.Write($"\nGeben Sie die Terminnummer ein um fortzufahren: ");
                int index = AppointmentViewGeneral.GetUserInputInt();

                int appointmentsCount = appointmentManager.Appointments.Count;
                if (index > 0 && index <= appointmentsCount)
                    RemoveAppointmentByIndex(--index);
                else
                    Console.WriteLine("Die eingegebene Terminnummer existiert nicht.");
            }
            else
            {
                Console.WriteLine("\nEs wurden keine Termine gefunden.");
            }
        }

        /// <summary>
        /// Deletes the appointment(s) by date.
        /// </summary>
        internal void DeleteAppointmentByDate()
        {
            Console.Write($"\nGeben Sie das Startdatum im Format \"dd.MM.yyyy\" ein um fortzufahren: ");
            DateTime? date = AppointmentViewGeneral.GetUserInputDateTime("dd.MM.yyyy");

            if (date != null)
                RemoveAppointmentByDate(date.Value);
            else
                AppointmentViewGeneral.PrintInvalidInput();
        }

        /// <summary>
        /// Deletes all appointments.
        /// </summary>
        internal void DeleteAllAppointments()
        {
            if (appointmentManager.Appointments.Any())
            {
                Console.WriteLine($"\nMöchten Sie wirklich alle Termine löschen?");
                Console.Write($"Geben Sie \"{CONFIRMATION_STRING}\" ein um die Löschung zu bestätigen: ");
                string input = AppointmentViewGeneral.GetUserInputString();

                if (input == CONFIRMATION_STRING)
                {
                    appointmentManager.DeleteAllAppointments();
                    Console.WriteLine("\nEs wurden alle Termine gelöscht.");
                }
                else
                {
                    Console.WriteLine("\nDer Löschvorgang wurde abgebrochen.");
                }
            }
            else
            {
                Console.WriteLine("\nEs wurden keine Termine gefunden.");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Deletes the specified appointment.
        /// </summary>
        /// <param name="index">The specified index in the appointment list.</param>
        private void RemoveAppointmentByIndex(int index)
        {
            var appointments = appointmentManager.GetAppointmentsOfSameId(index);

            if (appointments.Count > 1)
            {
                Console.WriteLine($"\nEs wurden zukünftige Termine für diesen Termin gefunden. Möchten Sie diese auch löschen?");
                Console.Write($"Geben Sie \"{CONFIRMATION_STRING}\" ein um alle zukünftige Termine zu löschen: ");
                string input = AppointmentViewGeneral.GetUserInputString();

                if (input == CONFIRMATION_STRING)
                {
                    appointmentManager.DeleteSpecifiedAndFutureAppointments(index);
                    Console.WriteLine($"\nDer Termin und alle zukünftigen Termine wurden gelöscht.");
                }
                else
                {
                    appointmentManager.DeleteAppointment(index);
                    Console.WriteLine($"/nDer angegebene Termin wurde gelöscht.");
                }
            }
            else
            {
                Console.WriteLine($"\nSoll der Termin wirklich gelöscht werden?");
                Console.Write($"Geben Sie \"{CONFIRMATION_STRING}\" ein um die Löschung zu bestätigen: ");
                string input = AppointmentViewGeneral.GetUserInputString();

                if (input == CONFIRMATION_STRING)
                {
                    appointmentManager.DeleteAppointment(index);
                    Console.WriteLine("\nDer angegebene Termin wurde gelöscht.");
                }
                else
                {
                    Console.WriteLine("\nDer Löschvorgang wurde abgebrochen.");
                }
            }
        }

        /// <summary>
        /// Deletes all appointments of the specified date.
        /// </summary>
        /// <param name="date">The specified date.</param>
        private void RemoveAppointmentByDate(DateTime date)
        {
            bool hasAppointments = appointmentManager.HasAppointmentsOnSpecifiedDate(date);

            if (hasAppointments)
            {
                Console.WriteLine($"\nEs wurden ein oder mehrere Termine gefunden.");
                Console.Write($"Geben Sie \"{CONFIRMATION_STRING}\" ein um die Löschung zu bestätigen: ");
                string input = AppointmentViewGeneral.GetUserInputString();

                if (input == CONFIRMATION_STRING)
                {
                    appointmentManager.DeleteAppointmentsOfSpecifiedDate(date);
                    Console.WriteLine("\nDie Termine wurden gelöscht.");
                }
                else
                {
                    Console.WriteLine("\nDer Löschvorgang wurde abgebrochen.");
                }
            }
            else
            {
                Console.WriteLine("\nEs wurden keine Termine gefunden.");
            }
        }

        #endregion
    }
}
