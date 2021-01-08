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
using System.Collections.Generic;
using System.Linq;
using Terminkalender.Controller;
using Terminkalender.Model.Appointments;

namespace Terminkalender.View
{
    /// <summary>
    /// This class is responsible for displaying appointments that are to be displayed.
    /// </summary>
    internal class AppointmentViewDisplay
    {
        private readonly AppointmentManager appointmentManager;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="appointmentManager">Manages all schedule-related tasks.</param>
        internal AppointmentViewDisplay(AppointmentManager appointmentManager)
        {
            this.appointmentManager = appointmentManager;
        }

        /// <summary>
        /// Shows the appointments of today.
        /// </summary>
        internal void ShowAppointmentsOfToday()
        {
            var appointments = appointmentManager.GetOngoingAppointments(rangeInDays: 1);

            if (appointments.Any())
                ShowAppointments(appointments);
            else
                Console.WriteLine("\nEs wurden für heute keine Termine gefunden.");
        }

        /// <summary>
        /// Shows the appointments for the next seven days.
        /// </summary>
        internal void ShowAppointmentsOfNextSevenDays()
        {
            var appointments = appointmentManager.GetOngoingAppointments(rangeInDays: 7);

            if (appointments.Any())
                ShowAppointments(appointments);
            else
                Console.WriteLine("\nEs wurden für die nächsten sieben Tage keine Termine gefunden.");
        }

        /// <summary>
        /// Show all appointments.
        /// </summary>
        internal void ShowAllAppointments()
        {
            var appointments = appointmentManager.Appointments;

            if (appointments.Any())
                ShowAppointments(appointments);
            else
                Console.WriteLine("\nEs wurden keine Termine gefunden.");
        }

        /// <summary>
        /// Shows the specified appopintments.
        /// </summary>
        /// <param name="appointments">The list of appointments.</param>
        private void ShowAppointments(List<Appointment> appointments)
        {
            Console.WriteLine("\nÜbersicht der Termine:");

            int counter = 0;
            foreach (var appointment in appointments)
            {
                Console.WriteLine($"Nr. {++counter} - Titel: {appointment.Title}, Startdatum: {appointment.DateStart}, Enddatum: {appointment.DateEnd}");
            }
        }
    }
}
