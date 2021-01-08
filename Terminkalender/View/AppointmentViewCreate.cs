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
    /// This class is responsible for displaying appointments that are to be created.
    /// </summary>
    internal class AppointmentViewCreate
    {
        #region Members

        private readonly AppointmentManager appointmentManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="appointmentManager">Manages all schedule-related tasks.</param>
        internal AppointmentViewCreate(AppointmentManager appointmentManager)
        {
            this.appointmentManager = appointmentManager;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Creates one or multiple appointments.
        /// </summary>
        internal void CreateNewAppointment()
        {
            // Appointment Type
            AppointmentType appointmentType = GetAppointmentType();
            if (appointmentType == AppointmentType.None)  
                return;

            // Date Format
            string dateFormat;
            if (appointmentType == AppointmentType.AllDay)
                dateFormat = "dd.MM.yyyy";
            else
                dateFormat = "dd.MM.yyyy HH:mm";

            // Recurrence Type
            RecurrenceType recurrenceType = GetRecurrenceType();
            if (recurrenceType == RecurrenceType.None)
                return;

            // Number Of Appointments
            int numberOfRecurrences = 1;
            if (recurrenceType == RecurrenceType.Weekly || recurrenceType == RecurrenceType.Yearly)
                numberOfRecurrences = GetNumberOfRecurrences();

            // Appointment Title
            string title = GetAppointmentTitle();

            // Appointment Start Date & End Date
            DateTime dateStart = GetAppointmentDateStart(dateFormat);
            DateTime dateEnd;
            if (appointmentType == AppointmentType.Duration)
                dateEnd = GetAppointmentDateEnd(dateStart, dateFormat);
            else
                dateEnd = new DateTime(dateStart.Year, dateStart.Month, dateStart.Day, 23, 59, 59, 999);

            // Create Appointment
            if (recurrenceType == RecurrenceType.Never)
                appointmentManager.CreateAppointment(title, dateStart, dateEnd);
            else
                appointmentManager.CreateMultipleAppointments(title, dateStart, dateEnd, recurrenceType, numberOfRecurrences);

            Console.WriteLine("\nDie Termin(e) wurden erfolgreich angelegt.");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the appointment type to be created.
        /// </summary>
        /// <returns>Returns the appointment type as an enum.</returns>
        private AppointmentType GetAppointmentType()
        {
            AppointmentType appointmentType;
            do
            {
                Console.WriteLine("\n1. Ganztägiger Termin");
                Console.WriteLine("2. Termin mit Dauer");
                Console.WriteLine("0. Abbruch");
                Console.Write("Geben Sie die entsprechende Nummer ein um fortzufahren: ");
                int input = AppointmentViewGeneral.GetUserInputInt();

                switch (input)
                {
                    case 1:
                        appointmentType = AppointmentType.AllDay;
                        break;
                    case 2:
                        appointmentType = AppointmentType.Duration;
                        break;
                    case 0:
                        appointmentType = AppointmentType.None;
                        AppointmentViewGeneral.PrintCanceledAction();
                        break;
                    default:
                        appointmentType = AppointmentType.Invalid;
                        AppointmentViewGeneral.PrintInvalidInput();
                        break;
                }
            }
            while (appointmentType == AppointmentType.Invalid);

            return appointmentType;
        }

        /// <summary>
        /// Gets the recurrence type to be created.
        /// </summary>
        /// <returns>Returns the recurrence type as an enum.</returns>
        private RecurrenceType GetRecurrenceType()
        {
            RecurrenceType recurrenceType;
            do
            {
                Console.WriteLine("\n1. Einmaliger Termin");
                Console.WriteLine("2. Wöchtenlicher Termin");
                Console.WriteLine("3. Jährlicher Termin");
                Console.WriteLine("0. Abbruch");
                Console.Write("Geben Sie die entsprechende Nummer ein um fortzufahren: ");
                int input = AppointmentViewGeneral.GetUserInputInt();

                switch (input)
                {
                    case 1:
                        recurrenceType = RecurrenceType.Never;
                        break;
                    case 2:
                        recurrenceType = RecurrenceType.Weekly;
                        break;
                    case 3:
                        recurrenceType = RecurrenceType.Yearly;
                        break;
                    case 0:
                        recurrenceType = RecurrenceType.None;
                        AppointmentViewGeneral.PrintCanceledAction();
                        break;
                    default:
                        recurrenceType = RecurrenceType.Invalid;
                        AppointmentViewGeneral.PrintInvalidInput();
                        break;
                }
            }
            while (recurrenceType == RecurrenceType.Invalid);

            return recurrenceType;
        }

        /// <summary>
        /// Gets how often the appointment should be created.
        /// </summary>
        /// <returns>The number of appointments as number.</returns>
        private int GetNumberOfRecurrences()
        {
            int input;
            do
            {
                Console.WriteLine("\nWie oft soll der Termin erstellt werden?");
                Console.Write("Geben Sie die entsprechende Nummer ein um fortzufahren: ");
                input = AppointmentViewGeneral.GetUserInputInt();
            }
            while (input <= 0);

            return input;
        }

        /// <summary>
        /// Gets the appointment title.
        /// </summary>
        /// <returns>Returns the title as string.</returns>
        private string GetAppointmentTitle()
        {
            Console.Write("\nWie lautet der Titel des Termins: ");
            string title = AppointmentViewGeneral.GetUserInputString();

            return title;
        }

        /// <summary>
        /// Gets the appointment start date.
        /// </summary>
        /// <param name="dateFormat">The date format to be used.</param>
        /// <returns>Returns the start date as DateTime.</returns>
        private DateTime GetAppointmentDateStart(string dateFormat)
        {
            DateTime? date;
            do
            {
                Console.Write($"\nGeben Sie das Startdatum im Format \"{dateFormat}\" ein um fortzufahren: ");
                date = AppointmentViewGeneral.GetUserInputDateTime(dateFormat);

                if (date == null)
                {
                    AppointmentViewGeneral.PrintInvalidInput();
                }
            }
            while (date == null);
            
            return date.Value;
        }

        /// <summary>
        /// Gets the appointment end date.
        /// </summary>
        /// <param name="dateStart">The appointment start date.</param>
        /// <param name="dateFormat">The date format to be used.</param>
        /// <returns>Returns the end date as DateTime.</returns>
        private DateTime GetAppointmentDateEnd(DateTime dateStart, string dateFormat)
        {
            DateTime? dateEnd;
            do
            {
                Console.Write($"Geben Sie das Enddatum im Format \"{dateFormat}\" ein um fortzufahren: ");
                dateEnd = AppointmentViewGeneral.GetUserInputDateTime(dateFormat);

                if (dateEnd == null)
                {
                    AppointmentViewGeneral.PrintInvalidInput();
                }
                else
                {
                    DateTime dateMaxLength = dateStart.AddYears(1);

                    if (dateEnd > dateMaxLength)
                    {
                        Console.WriteLine("\nDer Termin darf nicht länger als ein Jahr andauern.");
                        dateEnd = null;
                    }
                    else if (dateEnd < dateStart)
                    {
                        Console.WriteLine("\nDas Enddatum darf nicht in der Vergangenheit liegen.");
                        dateEnd = null;
                    }
                }
            }
            while (dateEnd == null);
            
            return dateEnd.Value;
        }

        #endregion
    }
}
