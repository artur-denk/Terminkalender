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
    /// This class is responsible for displaying appointments that are to be saved or loaded.
    /// </summary>
    internal class AppointmentViewStorage
    {
        private readonly AppointmentManager appointmentManager;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="appointmentManager">Manages all schedule-related tasks.</param>
        internal AppointmentViewStorage(AppointmentManager appointmentManager)
        {
            this.appointmentManager = appointmentManager;
        }

        /// <summary>
        /// Saves all created appointments.
        /// </summary>
        internal void SaveAppointments()
        {
            bool saveSuccessful = appointmentManager.SaveAppointments();

            if (saveSuccessful)
                Console.WriteLine("\nDie Termine wurden erfolgreich gespeichert.");
            else
                Console.WriteLine("\nDie Termine konnten nicht gespeichert.");
        }
    }
}
