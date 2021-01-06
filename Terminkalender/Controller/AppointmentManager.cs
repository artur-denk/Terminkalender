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
using Terminkalender.Model.Appointments;
using Terminkalender.Model.Data;

namespace Terminkalender.Controller
{
    /// <summary>
    /// This class manages the appointments.
    /// </summary>
    internal class AppointmentManager
    {
        #region Properties

        /// <summary>
        /// Gets the list of all appointments.
        /// </summary>
        internal List<Appointment> Appointments { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        internal AppointmentManager()
        {
            LoadAppointments();
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Creates a single appointment.
        /// </summary>
        /// <param name="title">The title of the appointment.</param>
        /// <param name="dateStart">The start date of the appointment.</param>
        /// <param name="dateEnd">The end date of the appointment.</param>
        internal void CreateAppointment(string title, DateTime dateStart, DateTime dateEnd)
        {
            string id = CreateUniqueAppointmentId();

            Appointment appointment = new Appointment(id, title, dateStart, dateEnd);
            Appointments.Add(appointment);
        }

        /// <summary>
        /// Creates multiple appointments.
        /// </summary>
        /// <param name="title">The title of the appointment.</param>
        /// <param name="dateStart">The start date of the appointment.</param>
        /// <param name="dateEnd">The end date of the appointment.</param>
        /// <param name="recurrenceType">The recurrence type of the appointment.</param>
        /// <param name="numberOfRecurrences">The number of recurrences.</param>
        internal void CreateMultipleAppointments(string title, DateTime dateStart, DateTime dateEnd, RecurrenceType recurrenceType, int numberOfRecurrences)
        {
            string id = CreateUniqueAppointmentId();

            List<Appointment> list = new List<Appointment>();
            if (recurrenceType == RecurrenceType.Weekly)
            {
                for (int i = 0; i < numberOfRecurrences; i++)
                {
                    list.Add(new Appointment(id, title, dateStart, dateEnd));
                    dateStart = dateStart.AddDays(7.0);
                    dateEnd = dateEnd.AddDays(7.0);
                }
            }
            else if (recurrenceType == RecurrenceType.Yearly)
            {
                for (int i = 0; i < numberOfRecurrences; i++)
                {
                    list.Add(new Appointment(id, title, dateStart, dateEnd));
                    dateStart = dateStart.AddYears(1);
                    dateEnd = dateEnd.AddYears(1);
                }
            }

            Appointments.AddRange(list);
        }

        /// <summary>
        /// Gets the list of appointments with the same appointment ID.
        /// </summary>
        /// <param name="index">The appointment list index.</param>
        /// <returns>The list of appointments with the same appointment ID.</returns>
        internal List<Appointment> GetAppointmentsOfSameId(int index)
        {
            Appointment appointment = Appointments.ElementAt(index);
            List<Appointment> list = Appointments.Where(a => a.ID == appointment.ID && a.DateStart >= appointment.DateStart).ToList();

            return list;
        }

        /// <summary>
        /// Gets a list of ongoing appointments.
        /// </summary>
        /// <param name="rangeInDays">The range in days for all relevant appointments.</param>
        /// <returns>The list of matching appointments.</returns>
        internal List<Appointment> GetOngoingAppointments(int rangeInDays)
        {
            DateTime dateNow = DateTime.Now;
            DateTime startOfDay = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);
            DateTime endOfDay = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day);
            endOfDay = endOfDay.AddDays(rangeInDays);

            List<Appointment> list = Appointments.Where(a =>
                (a.DateStart >= startOfDay && a.DateEnd < endOfDay) ||
                (a.DateStart >= startOfDay && a.DateStart < endOfDay) ||
                (a.DateEnd >= startOfDay && a.DateEnd < endOfDay) ||
                (a.DateStart <= startOfDay && a.DateEnd > endOfDay)
            ).ToList();

            return list;
        }

        /// <summary>
        /// Deletes the appointment at the specified index.
        /// </summary>
        /// <param name="index">The appointment index.</param>
        internal void DeleteAppointment(int index)
        {
            Appointments.RemoveAt(index);
        }

        /// <summary>
        /// Deletes the specified appointment.
        /// </summary>
        /// <param name="index">The index position of the appointment</param>
        internal void DeleteSpecifiedAndFutureAppointments(int index)
        {
            Appointment appointment = Appointments.ElementAt(index);
            Appointments.RemoveAll(a => a.ID == appointment.ID && a.DateStart >= appointment.DateStart);
        }

        /// <summary>
        /// Deletes all appointments of the specified date.
        /// </summary>
        /// <param name="date">The specified date to delete.</param>
        internal void DeleteAppointmentsOfSpecifiedDate(DateTime date)
        {
            DateTime startOfDay = new DateTime(date.Year, date.Month, date.Day);
            DateTime endOfDay = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);

            Appointments.RemoveAll(a => a.DateStart >= startOfDay && a.DateStart <= endOfDay);
        }

        /// <summary>
        /// Deletes all appointments.
        /// </summary>
        internal void DeleteAllAppointments()
        {
            Appointments.Clear();
        }

        /// <summary>
        /// Gets whether list has any appointments with the specified date.
        /// </summary>
        /// <param name="startDate">The start date of the specified date.</param>
        /// <returns>True if any appointments exist; otherwise false.</returns>
        internal bool HasAppointmentsOnSpecifiedDate(DateTime startDate)
        {
            List<Appointment> list = Appointments.Where(a =>
               startDate.Year == a.DateStart.Year &&
               startDate.Month == a.DateStart.Month &&
               startDate.Day == a.DateStart.Day
            ).ToList();

            return list.Any();
        }

        /// <summary>
        /// Saves all appointments.
        /// </summary>
        /// <returns>True if saving was successful; otherwise false.</returns>
        internal bool SaveAppointments()
        {
            Storage storage = new Storage();
            bool saveSuccessful = storage.SaveAppointments(Appointments);

            return saveSuccessful;
        }

        /// <summary>
        /// Loads all appointments from the XML file.
        /// </summary>
        internal void LoadAppointments()
        {
            Storage storage = new Storage();
            Appointments = storage.LoadAppointments();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates a unique id for one or a group of appointments. 
        /// </summary>
        /// <returns>The unique id.</returns>
        private string CreateUniqueAppointmentId()
        {
            long ticks = DateTime.Now.Ticks;
            string guid = Guid.NewGuid().ToString();
            string appointmentId = $"{ticks}-{guid}";

            return appointmentId;
        }

        #endregion
    }
}
